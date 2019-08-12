using System;

namespace MagicCube
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[][] s =
            {
                new[] {4, 9, 2},
                new[] {3, 5, 7},
                new[] {8, 1, 5}
            };

//            s = new []
//            {
//                new[] {8, 3, 4},
//                new[] {1, 5, 9},
//                new[] {6, 7, 2}
//            };

            Console.WriteLine(formingMagicSquare(s));
        }

        // Complete the formingMagicSquare function below.
        static int formingMagicSquare(int[][] s)
        {
            bool[] digits = new bool[9];
            int[][] mins = new int[3][]
            {
                new int[3],
                new int[3],
                new int[3],
            };

            if (isMagic(s)) return 0;

            return form(s, mins, digits);
        }

        private static int G = 0;

        static int form(int[][] s, int[][] mins, bool[] digits, int min = -1, int depth=1)
        {
            if (isEmpty(digits))
            {
                if (isMagic(mins))
                {
                    var delta = compare(s, mins);
                    min = delta < min ? delta : min;
                }

                return min;
            }

            if (min == -1) min = int.MaxValue;

            depth++;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mins[i][j] != 0) continue;

                    for (int k = 0; k < 9; k++)
                    {
                        if (!digits[k])
                        {
                            digits[k] = true;
                            mins[i][j] = k + 1;
                            min = form(s, mins, digits, min, depth);
                            mins[i][j] = 0;
                            digits[k] = false;
                        }
                    }
                }
            }

            return min;
        }

        static int compare(int[][] arr1, int[][] arr2)
        {
            int sum = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sum += Math.Abs(arr1[i][j] - arr2[i][j]);
                }
            }

            return sum;
        }

        static bool isEmpty(int[][] arr)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i][j] != 9) return false;
                }
            }

            return true;
        }

        static bool isEmpty(bool[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i]) return false;
            }

            return true;
        }

        static bool isMagic(int[][] s)
        {
            int sum = 0;
            int prevSum = 0;
            for (int i = 0; i < 3; i++)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += s[i][j];
                }

                if (i != 0 && sum != prevSum) return false;
                prevSum = sum;
            }

            for (int i = 0; i < 3; i++)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += s[j][i];
                }

                if (sum != prevSum) return false;
                prevSum = sum;
            }


            sum = 0;
            for (int i = 0; i < 3; i++)
            {
                sum += s[i][i];
            }

            if (sum != prevSum) return false;
            sum = 0;
            for (int i = 0; i < 3; i++)
            {
                sum += s[i][3 - i - 1];
            }

            if (sum != prevSum) return false;

            return true;
        }
    }
}
