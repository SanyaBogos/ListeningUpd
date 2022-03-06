using Listening.Core.ViewModels.Steg;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server;
using Listening.Server.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Services
{
    public class StegPictureService : IStegPictureService
    {
        private readonly string _stegPicturePath;
        private readonly int _stegTTL;
        private readonly IFileService _fileService;
        private readonly IStegDataOperationsService _stegDataOperationsService;
        private readonly IFunctionService _functionService;

        public StegPictureService(IStegDataOperationsService stegDataOperationsService,
            IFunctionService functionService,
            IConfiguration configuration,
            IFileService fileService,
            IWebHostEnvironment env)
        {
            _stegPicturePath = $"{env.WebRootPath}{configuration["Data:FileStorage:Steg:Picture"]}";
            _stegTTL = Convert.ToInt32(configuration["Data:StegTimeToLive"]);

            _fileService = fileService;
            _stegDataOperationsService = stegDataOperationsService;
            _functionService = functionService;
        }

        public string SimpleInjectMessage(StegSettingsDto settings)
        {
            var fullPath = $"{_stegPicturePath}{settings.FileName}";

            using (var imageData = Image.Load<Rgba32>(fullPath))
            {
                if (settings.Message.Length > _stegDataOperationsService.GetMaxLength(imageData.Width, imageData.Height, settings.Colors.Length))
                    throw new StegException(GlobalConstats.STEG_TOO_HUGE_MESSAGE);

                var bytesWithLength = _stegDataOperationsService.StringToBytesWithLength(settings.Message);
                var bits = new Queue<bool>(_stegDataOperationsService.NumbersToBits(bytesWithLength));
                var startIndexEnh = settings.StartIndex;
                var stepEnh = settings.Step;

                if (settings.Mode == 's')
                {
                    var iH = 0;
                    var iW = 0;

                    if (startIndexEnh > imageData.Width)
                        iH = Math.DivRem(startIndexEnh, imageData.Width, out iW);
                    else
                        iW = startIndexEnh;

                    do
                    {
                        ChangePixel(settings, imageData, bits, iH, iW);

                        var div = Math.DivRem(iW + stepEnh, imageData.Width, out iW);
                        iH += div;

                    } while (bits.Count > 0);
                }
                else
                {
                    var pictureSize = new System.Drawing.Size(imageData.Width, imageData.Height);
                    var pointCounts = DivideRoundingUp(bits.Count, settings.Colors.Length);
                    var func = new FuncEnhancedDto
                    {
                        Description = settings.Func.Description,
                        StartIndex = settings.StartIndex,
                        Step = settings.Step
                    };
                    var pffParams = new PointsFromFuncParams
                    {
                        FuncDto = func,
                        PictureSize = pictureSize,
                        Length = pointCounts
                    };
                    var points = _functionService.GetPointsFromFunction(pffParams).Array;

                    for (int i = 0; i < pointCounts; i++)
                        ChangePixel(settings, imageData, bits, points[i, 1], points[i, 0]);
                }

                var resultPath = fullPath.GetUpdatedFileNameFromPath("-res");
                imageData.SaveAsPng(resultPath);
                _fileService.SetAutoClearResource(resultPath, Core.FileContentType.StegPict, _stegTTL);
                return resultPath;
            }
        }

        private void ChangePixel(StegSettingsDto settings, Image<Rgba32> imageData, Queue<bool> bits, int iH, int iW)
        {
            var newColor = imageData[iW, iH];

            for (int j = 0; j < settings.Colors.Length; j++)
            {
                switch (settings.Colors[j])
                {
                    case 0: newColor.R = (byte)GetUpdatedLSB(imageData[iW, iH].R, bits.Dequeue()); break;
                    case 1: newColor.G = (byte)GetUpdatedLSB(imageData[iW, iH].G, bits.Dequeue()); break;
                    case 2: newColor.B = (byte)GetUpdatedLSB(imageData[iW, iH].B, bits.Dequeue()); break;
                    case 3: newColor.A = (byte)GetUpdatedLSB(imageData[iW, iH].A, bits.Dequeue()); break;
                    default:
                        break;
                }
            }

            imageData[iW, iH] = newColor;
        }

        public string SimpleEjectMessage(StegSettingsDto settings)
        {
            var fullPath = $"{_stegPicturePath}{settings.FileName}";

            using (var imageData = Image.Load<Rgba32>(fullPath))
            {

                var resultBits = new List<bool>();
                var bufferLength = _stegDataOperationsService.GetBufferLength();
                var lengthBits = new List<bool>();
                var startIndex = settings.StartIndex;
                var step = settings.Step;

                if (settings.Mode == 's')
                {
                    var iH = 0;
                    var iW = 0;

                    if (startIndex > imageData.Width)
                        iH = Math.DivRem(startIndex, imageData.Width, out iW);
                    else
                        iW = startIndex;

                    while (lengthBits.Count < bufferLength)
                        BuildLSBListSimple(imageData, settings, lengthBits, step, ref iH, ref iW);

                    var messageLength = _stegDataOperationsService.BitsToNumber(lengthBits.ToArray())[0];

                    if (messageLength > _stegDataOperationsService.GetMaxLength(imageData.Width, imageData.Height, settings.Colors.Length))
                        throw new StegException(GlobalConstats.STEG_EJECT_INCREASE_LENGTH);

                    while (resultBits.Count < messageLength * bufferLength)
                        BuildLSBListSimple(imageData, settings, resultBits, step, ref iH, ref iW);
                }
                else
                {
                    var pictureSize = new System.Drawing.Size(imageData.Width, imageData.Height);
                    var pointCounts = DivideRoundingUp(bufferLength, settings.Colors.Length);
                    var func = new FuncEnhancedDto
                    {
                        Description = settings.Func.Description,
                        StartIndex = settings.StartIndex,
                        Step = settings.Step
                    };
                    var pffParams = new PointsFromFuncParams
                    {
                        FuncDto = func,
                        PictureSize = pictureSize,
                        Length = pointCounts
                    };
                    var pointsAll = _functionService.GetPointsFromFunction(pffParams);
                    var points = pointsAll.Array;

                    for (int i = 0; i < pointCounts; i++)
                        BuildLSBList(imageData, settings, lengthBits, points[i, 1], points[i, 0]);

                    var messageLength = _stegDataOperationsService.BitsToNumber(lengthBits.ToArray())[0];

                    if (messageLength > _stegDataOperationsService.GetMaxLength(imageData.Width, imageData.Height, settings.Colors.Length))
                        throw new StegException(GlobalConstats.STEG_EJECT_INCREASE_LENGTH);

                    pffParams.FuncDto.StartIndex = pointsAll.Index;
                    pointCounts = pffParams.Length = DivideRoundingUp(messageLength * bufferLength, settings.Colors.Length);
                    var otherPoints = _functionService.GetPointsFromFunction(pffParams).Array;

                    for (int i = 0; i < pointCounts; i++)
                        BuildLSBList(imageData, settings, resultBits, otherPoints[i, 1], otherPoints[i, 0]);
                }

                var numbers = _stegDataOperationsService.BitsToNumber(resultBits.ToArray());
                var message = _stegDataOperationsService.NumbersToString(numbers);
                return message;
            }

        }

        private void BuildLSBListSimple(Image<Rgba32> imageData, StegSettingsDto settings, List<bool> lengthBits, int step, ref int iH, ref int iW)
        {
            BuildLSBList(imageData, settings, lengthBits, iH, iW);

            var div = Math.DivRem(iW + step, imageData.Width, out iW);
            iH += div;
        }

        private void BuildLSBList(Image<Rgba32> imageData, StegSettingsDto settings, List<bool> lengthBits, int iH, int iW)
        {
            for (int j = 0; j < settings.Colors.Length; j++)
            {
                switch (settings.Colors[j])
                {
                    case 0: lengthBits.Add(GetLSB(imageData[iW, iH].R)); break;
                    case 1: lengthBits.Add(GetLSB(imageData[iW, iH].G)); break;
                    case 2: lengthBits.Add(GetLSB(imageData[iW, iH].B)); break;
                    case 3: lengthBits.Add(GetLSB(imageData[iW, iH].A)); break;
                    default:
                        break;
                }
            }
        }

        private int DivideRoundingUp(int x, int y)
        {
            int quotient = Math.DivRem(x, y, out int remainder);
            return remainder == 0 ? quotient : quotient + 1;
        }

        private int GetUpdatedLSB(byte val, bool bit)
        {
            if (bit)
                return val | 1;
            else
                return val & ~0 << 1;
        }

        private bool GetLSB(byte val)
        {
            if ((val & 1) == 0)
                return false;
            else
                return true;
        }
    }
}
