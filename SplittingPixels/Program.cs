using System;

namespace SplittingPixels
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var color = "000000001111111100000110";

            Console.WriteLine(FindClosest(color));
        }

        public static string FindClosest(string color)
        {
            var r = ToByte(color.Substring(0, 8));
            var g = ToByte(color.Substring(8, 8));
            var b = ToByte(color.Substring(16, 8));

            var inC = new []{r, g, b};

            var colors = new[]
            {
                new byte[] {0, 0, 0},
                new byte[] {255, 255, 255},
                new byte[] {255, 0, 0},
                new byte[] {0, 255, 0},
                new byte[] {0, 0, 255}
            };

            var minI = 0;
            var minDist = Math.Sqrt(3) * 255;
            var precision = 0.000001;

            for (var i = 0; i < colors.Length; i++)
            {
                var c = colors[i];
                var dist = Dist(c, inC);

                if (dist < minDist)
                {
                    if (Math.Abs(dist - minDist) < precision)
                    {
                        minDist = -1;
                        minI = -1;
                        break;
                    }
                    else
                    {
                        minDist = dist;
                        minI = i;
                    }
                }
            }

            switch (minI)
            {
                case 0:
                    return "black";
                case 1:
                    return "white";
                case 2:
                    return "red";
                case 3:
                    return "green";
                case 4:
                    return "blue";
                default:
                    return "ambiguous";
            }
        }

        private static double Dist(byte[] c1, byte[] c2)
        {
            return Math.Sqrt(Math.Pow(c1[0] - c2[0], 2) + Math.Pow(c1[1] - c2[1], 2) + Math.Pow(c1[2] - c2[2], 2));
        }

        private static byte ToByte(string s)
        {
            byte b = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '1')
                {
                    b += (byte)Math.Pow(2, s.Length - i - 1);
                }
            }

            return b;
        }
    }
}
