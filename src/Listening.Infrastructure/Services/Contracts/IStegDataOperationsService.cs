using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IStegDataOperationsService
    {
        int[] StringToBytes(string str);
        int[] StringToBytesWithLength(string str);
        bool[] NumberToBits(int num);
        int[] BitsToNumber(bool[] bits);
        string NumbersToString(int[] numbers);
        bool[] NumbersToBits(int[] nums);
        int GetMaxLength(int width, int height, int colorsCount);
        byte GetBufferLength();
    }
}
