using Listening.Core.ViewModels.Steg;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server;
using NCalc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Listening.Infrastructure.Services
{
    public class FunctionService : IFunctionService
    {
        public PointsFromFuncResultDto GetPointsFromFunction(PointsFromFuncParams pffParams)
        {
            var justFunction = GetPreparedFunction(pffParams.FuncDto.Description);
            var expr = new Expression(justFunction);

            expr.EvaluateParameter += delegate (string name, ParameterArgs args)
            {
                if (name == "Pi")
                    args.Result = Math.PI;
            };

            var pointsCount = 0;
            var index = pffParams.FuncDto.StartIndex;
            var result = new int[pffParams.Length, 2];

            do
            {
                if (index < 0 || index > pffParams.PictureSize.Height) goto NextStep;

                expr.Parameters["x"] = index;
                var y = Convert.ToInt32(expr.Evaluate());

                if (y < 0 || y > pffParams.PictureSize.Height || !IsPointAvailable(result, pointsCount, index, y))
                    goto NextStep;

                result[pointsCount, 0] = index;
                result[pointsCount++, 1] = y;

            NextStep: index += pffParams.FuncDto.Step;
            } while (pointsCount < pffParams.Length && index < pffParams.PictureSize.Width);

            if (!pffParams.IsEdit && pointsCount < pffParams.Length)
                throw new StegException(GlobalConstats.STEG_TOO_HUGE_MESSAGE);

            var res = new PointsFromFuncResultDto
            {
                Array = result,
                Index = index
            };

            return res;
        }

        private string GetPreparedFunction(string func)
        {
            var result = func.Replace("x", "[x]")
                .Replace("abs", "Abs")
                .Replace("acos", "Acos")
                .Replace("arccos", "Acos")
                .Replace("asin", "Asin")
                .Replace("arcsin", "Asin")
                .Replace("atan", "Atan")
                .Replace("arctan", "Atan")
                .Replace("arctg", "Atan")
                .Replace("ceiling", "Ceiling")
                .Replace("cos", "Cos")
                .Replace("exp", "Exp")
                .Replace("floor", "Floor")
                .Replace("log", "Log")
                .Replace("log10", "Log10")
                .Replace("pow", "Pow")
                .Replace("sign", "Sign")
                .Replace("sin", "Sin")
                .Replace("sqrt", "Sqrt")
                .Replace("tan", "Tan")
                .Replace("tg", "Tan")
                .Replace("pi", "Pi")
                .Replace("PI", "Pi")
                .Replace("truncate", "Truncate");

            return result;
        }

        private bool IsPointAvailable(int[,] result, int pointsCount, int index, int y)
        {
            for (int i = 0; i < pointsCount; i++)
                if (result[i, 0] == index && result[i, 1] == y)
                    return false;

            return true;
        }
    }
}
