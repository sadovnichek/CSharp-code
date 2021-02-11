using System;
using System.Collections.Generic;

namespace Program
{
	class Program
    {
        public delegate int FindFunction(int[] array, int value);

        static int LinearSearch(int[] array, int value)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                    return i;
            }
            return -1;
        }

        static int BinarySearch(int[] array, int value)
        {
            int left = 0;
            int right = array.Length - 1;
            while(left < right)
            {
                int middle = (right + left) / 2;
                if (array[middle] > value)
                    right = middle - 1;
                else if (array[middle] < value)
                    left = middle + 1;
                else
                    return middle;
            }
            if (array[left] == value)
                return left;
            return -1;
        }
        // with func
        public static int FuncSearcher(int[] array, int value, Func<int[], int, int> function)
        {
            return function(array, value);
        }
        // with delegate
        public static int DelegateSearcher(int[] array, int value, FindFunction function)
        {
            return function(array, value);
        }

        static void Main()
        {
            var x = new int[5];
            Random random = new Random();
            for(int i = 0; i < 5; i++)
            {
                x[i] = random.Next(10);
            }
            Array.Sort(x);
            var index = FuncSearcher(x, x[2], BinarySearch);
		}
    }
}
