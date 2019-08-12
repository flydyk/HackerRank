using System;
using System.Collections.Generic;

namespace MagicStrings
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(BuildStr(100));
        }

        class State
        {
            public long n;
            public string set;

            private int _i;

            public bool HasNext()
            {
                return _i < set.Length;
            }

            public char Next()
            {
                return set[_i++];
            }
        }

        private static int BuildStr(long n)
        {
            int number = 0;
            var state = new State {n = n, set = "aeiou"};

            var stack = new Stack<State>();
            stack.Push(state);

            while (stack.Count > 0)
            {
                state = stack.Peek();
                if (state.n <= 0)
                {
                    stack.Pop();
                    number++;
                    continue;
                }

                if (!state.HasNext())
                {
                    stack.Pop();
                    continue;
                }

                var nextChar = state.Next();

                switch (nextChar)
                {
                    case 'a':
                    {
                        stack.Push(new State {n = state.n - 1, set = "e"});
                        break;
                    }

                    case 'e':
                    {
                        stack.Push(new State {n = state.n - 1, set = "ai"});
                        break;
                    }

                    case 'i':
                    {
                        stack.Push(new State {n = state.n - 1, set = "aeou"});
                        break;
                    }

                    case 'o':
                    {
                        stack.Push(new State {n = state.n - 1, set = "iu"});
                        break;
                    }

                    case 'u':
                    {
                        stack.Push(new State {n = state.n - 1, set = "a"});
                        break;
                    }
                }
            }

            return number;
        }
    }
}
