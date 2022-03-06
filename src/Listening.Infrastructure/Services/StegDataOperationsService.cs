using Listening.Infrastructure.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Services
{
    public class StegDataOperationsService : IStegDataOperationsService
    {
        private readonly byte _bufferLength;

        public StegDataOperationsService(IConfiguration configuration)
        {
            _bufferLength = Convert.ToByte(configuration["Data:StegBufferLength"]);
        }

        public int[] StringToBytes(string str)
        {
            var data = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
                data[i] = (Char.ConvertToUtf32(str, i));

            return data;
        }

        public int[] StringToBytesWithLength(string str)
        {
            var data = new int[str.Length + 1];
            data[0] = str.Length;

            for (int i = 0; i < str.Length; i++)
                data[i + 1] = (Char.ConvertToUtf32(str, i));

            return data;
        }

        public bool[] NumberToBits(int num)
        {
            var arr = new bool[_bufferLength];

            for (var i = 0; i < _bufferLength; i++)
                arr[i] = Convert.ToBoolean((num >> i) & 1);

            return arr;
        }

        public int[] BitsToNumber(bool[] bits)
        {
            var numbers = new int[bits.Length / _bufferLength];

            for (int i = 0, n = 0; i < bits.Length; i += _bufferLength, n++)
                for (int j = 0; j < _bufferLength; j++)
                    if (bits[i + j])
                        numbers[n] += 1 << j;

            return numbers;
        }

        public string NumbersToString(int[] numbers)
        {
            var chars = new char[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
                chars[i] = Char.ConvertFromUtf32(numbers[i])[0];

            return new string(chars);
        }

        public bool[] NumbersToBits(int[] nums)
        {
            var bits = new bool[nums.Length * _bufferLength];

            for (int i = 0, k = 0; i < nums.Length; i++, k += _bufferLength)
            {
                var partBits = NumberToBits(nums[i]);
                Array.Copy(partBits, 0, bits, k, _bufferLength);
            }

            return bits;
        }

        public int GetMaxLength(int width, int height, int colorsCount)
        {
            var maxLength = (int)Math.Round((double)width * height * colorsCount / _bufferLength) - 1;
            return maxLength;
        }

        public byte GetBufferLength()
        {
            return _bufferLength;
        }
    }
}
