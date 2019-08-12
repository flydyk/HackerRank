using System;
using System.Collections.Generic;

namespace MarsColonists
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var f = new[] {7000, 7000, 12000, 13000, 6900};
            var m = new[] {6910, 7010, 7000, 12000, 18000, 15000, 10450};

            var sorted = SortIntersect(f, m);

            Console.Write(string.Join(" ", sorted));
        }

        public static List<int> SortIntersect(int[] f, int[] m)
        {
            var dict = new Dictionary<int, int>();
            var output = new List<int>();

            for (int i = 0; i < f.Length; i++)
            {
                var j = dict.TryGetValue(f[i], out var lastMatch) ? lastMatch + 1 : 0;
                for (; j < m.Length; j++)
                {
                    if (f[i] == m[j])
                    {
                        var added = false;

                        for (int k = 0; k < output.Count; k++)
                        {
                            if (output[k] < f[i])
                            {
                                output.Insert(k, f[i]);
                                added = true;
                                break;
                            }
                        }

                        if (!added)
                        {
                            output.Add(f[i]);
                        }

                        dict.Add(f[i], j);
                    }
                }
            }

            return output;
        }
    }
}
