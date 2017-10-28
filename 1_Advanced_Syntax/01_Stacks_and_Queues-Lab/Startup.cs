namespace _01_Stacks_and_Queues_Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            // Working with Stacks
            //E1_ReverseString();
            //E2_SimpleCalculator();
            //E3_DecimalToBinaryConverter();
            //E4_MatchingBrackets();

            // Working with Queues
            //E5_HotPotato();
            //E6_MathPotato();
        }

        public static void E6_MathPotato()
        {
            string[] childrenArr = Console.ReadLine().Split(' ').ToArray();
            int num = int.Parse(Console.ReadLine());

            Queue<string> queueChildren = new Queue<string>(childrenArr);

            int cycle = 1;
            List<int> primeNums = new List<int>();
            while (queueChildren.Count != 1)
            {
                for (int i = 1; i < num; i++)
                {
                    queueChildren.Enqueue(queueChildren.Dequeue());
                }
                if (IsPrime(cycle, primeNums))
                {
                    Console.WriteLine($"Prime {queueChildren.Peek()}");
                }
                else
                {
                    Console.WriteLine($"Removed {queueChildren.Dequeue()}");
                }
                cycle++;
            }

            Console.WriteLine($"Last is {queueChildren.Dequeue()}");
        }

        private static bool IsPrime(int number,List<int> primeNums)
        {
            if (number == 1)
                return false;

            foreach (int primeNum in primeNums)
            {
                if (number % primeNum == 0)
                {
                    return false;
                }
            }

            primeNums.Add(number);
            return true;
        }

        public static void E5_HotPotato()
        {
            string[] childrenArr = Console.ReadLine().Split(' ').ToArray();
            int num = int.Parse(Console.ReadLine());

            Queue<string> queueChildren = new Queue<string>(childrenArr);
            while (queueChildren.Count != 1)
            {
                for (int i = 1; i < num; i++)
                {
                    queueChildren.Enqueue(queueChildren.Dequeue());
                }
                Console.WriteLine($"Removed {queueChildren.Dequeue()}");
            }
            Console.WriteLine($"Last is {queueChildren.Dequeue()}");
        }

        public static void E4_MatchingBrackets()
        {
            string input = Console.ReadLine();

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char symbol = input[i];

                if (symbol == '(')
                {
                    stack.Push(i);
                }
                else if (symbol == ')')
                {
                    int startIdx = stack.Pop();
                    Console.WriteLine(input.Substring(startIdx, i - startIdx + 1));
                }
            }
        }

        public static void E3_DecimalToBinaryConverter()
        {
            int num = int.Parse(Console.ReadLine());

            //Console.WriteLine(Convert.ToString(num, 2));

            if (num == 0)
            {
                Console.WriteLine(0);
                return;
            }

            Stack<int> stack = new Stack<int>();

            while (num != 0)
            {
                stack.Push(num % 2);
                num /= 2;
            }

            while (stack.Count != 0)
            {
                Console.Write(stack.Pop());
            }
            Console.WriteLine();
        }

        public static void E2_SimpleCalculator()
        {
            Stack<string> stack = new Stack<string>();

            foreach (string symbol in Console.ReadLine().Split(' ').Reverse())
            {
                stack.Push(symbol);
            }

            int sum = int.Parse(stack.Pop());
            while (stack.Count != 0)
            {
                string oper = stack.Pop();
                int num = int.Parse(stack.Pop());

                switch (oper)
                {
                    case "+": sum += num; break;
                    case "-": sum -= num; break;
                }
            }

            Console.WriteLine(sum);
        }

        public static void E1_ReverseString()
        {
            Stack<char> stack = new Stack<char>();

            foreach (char letter in Console.ReadLine())
            {
                stack.Push(letter);
            }

            //Console.WriteLine(string.Join("", stack));

            while (stack.Count != 0)
            {
                Console.Write(stack.Pop());
            }
            Console.WriteLine();
        }
    }
}