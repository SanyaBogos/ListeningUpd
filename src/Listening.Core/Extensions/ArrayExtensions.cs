using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static void Swap<T>(this T[] array, int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= array.Length)
                throw new Exception("Incorrect swap index A");

            if (indexB < 0 || indexB >= array.Length)
                throw new Exception("Incorrect swap index B");

            T temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
        }

        //public static int FindIndex<T>(this T[] array, Func<>)
        //{
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (array[i])
        //        {

        //        }

        //    }

        //    return 0;
        //}
    }
}
