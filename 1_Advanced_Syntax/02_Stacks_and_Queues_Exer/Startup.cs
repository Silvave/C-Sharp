namespace _02_Stacks_and_Queues_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Startup
    {
        public static void Main()
        {
            //E1_ReverseNumbers();
            E2_BasicStackOperations();
            //E3_MaximumElement();
            //E4_BasicQueueOperations();
            //E5_SequenceWithQueue();
            //E6_TruckTourFirstSolution();
            //E6_TruckTourSecondSolution();
            //E7_BalancedParentheses();
            //E8_RecursiveFibonacci();
            //E9_StackFibonacci();
            //E10_SimpleTextEditor();
            //E11_PoisonousPlants(); // Not optimized.. gives 77/100 in judge
            //E11_PoisonousPlantsSecondSolution(); // Optimized with - The Stock Span Problem algorithm
        }

        public static void E11_PoisonousPlantsSecondSolution()
        {
            int numberOfPlants = int.Parse(Console.ReadLine());

            int[] arrPlants = Console.ReadLine()
                                    .Split()
                                    .Select(int.Parse)
                                    .ToArray();

            int[] days = new int[numberOfPlants];

            var proximityStack = new Stack<int>();
            proximityStack.Push(0);

            for (int i = 1; i < arrPlants.Length; i++)
            {
                int maxDays = 0;
                while (proximityStack.Count > 0 && arrPlants[proximityStack.Peek()] >= arrPlants[i])
                {
                    maxDays = Math.Max(maxDays, days[proximityStack.Pop()]);
                }

                if (proximityStack.Count > 0)
                {
                    days[i] = maxDays + 1;
                }

                proximityStack.Push(i);
            }

            Console.WriteLine(days.Max());
        }

        public static void E11_PoisonousPlants()
        {
            int numberOfPlants = int.Parse(Console.ReadLine());

            int index = 1;
            var plantsQueue = new Queue<Tuple<int, int>>();

            Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(number =>
                {
                    plantsQueue.Enqueue(Tuple.Create(index, int.Parse(number)));
                    index++;
                });

            int days = 0;
            while (true)
            {
                bool noGmoPlants = true;

                var startPlant = plantsQueue.Dequeue();
                plantsQueue.Enqueue(startPlant);

                var leftPlant = startPlant;
                while (startPlant.Item1 != plantsQueue.Peek().Item1)
                {
                    var rightPlant = plantsQueue.Peek();

                    if (leftPlant.Item2 < rightPlant.Item2)
                    {
                        leftPlant = plantsQueue.Dequeue();
                        noGmoPlants = false;
                    }
                    else
                    {
                        leftPlant = plantsQueue.Dequeue();
                        plantsQueue.Enqueue(leftPlant);
                    }
                }

                if (noGmoPlants)
                {
                    break;
                }

                days++;
            }

            Console.WriteLine(days);
        }

        public static void E10_SimpleTextEditor()
        {
            int numberOfOperations = int.Parse(Console.ReadLine());

            var stack = new Stack<string>();
            stack.Push(""); // Initial string

            for (int i = 0; i < numberOfOperations; i++)
            {
                var operationCmd = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var command = operationCmd[0];
                switch (command)
                {
                    case "1":
                        string apendedText = stack.Peek() + operationCmd[1];
                        stack.Push(apendedText);
                        break;
                    case "2":
                        int count = int.Parse(operationCmd[1]);
                        string currentStr = stack.Peek();
                        var updateStr = currentStr.Remove(currentStr.Length - count, count);
                        stack.Push(updateStr);
                        break;
                    case "3":
                        int index = int.Parse(operationCmd[1]);
                        Console.WriteLine(stack.Peek()[index - 1]);
                        break;
                    default: // command 4
                        stack.Pop();
                        break;
                }
            }
        }

        public static void E9_StackFibonacci()
        {
            int n = int.Parse(Console.ReadLine());

            var fibonacciStack = new Stack<long>();

            fibonacciStack.Push(0);
            fibonacciStack.Push(1);

            for (int i = 1; i < n; i++)
            {
                var num2 = fibonacciStack.Pop();
                var num1 = fibonacciStack.Peek();

                fibonacciStack.Push(num2);
                fibonacciStack.Push(num1 + num2);
            }

            Console.WriteLine(fibonacciStack.Peek());
        }

        public static void E8_RecursiveFibonacci()
        {
            int n = int.Parse(Console.ReadLine());

            // Wrong constrains in word file: n <= 49, in judge: n <= 50
            if (n < 1 || n > 50)
            {
                Console.WriteLine("Invalid number!");
                return;
            }

            Console.WriteLine(getFibonacci(0, 1, n));
        }

        private static long getFibonacci(long v1, long v2, int n)
        {
            if (n == 0)
            {
                return v1;
            }

            return getFibonacci(v2, v2 + v1, n - 1);
        }

        public static void E7_BalancedParentheses()
        {
            string paranthesesInput = Console.ReadLine();

            Regex regex = new Regex(@"^(?=[{\[(])[{}()[\]]+$");

            if (paranthesesInput.Length % 2 != 0 || paranthesesInput.Length > 1000 ||
                !regex.IsMatch(paranthesesInput))
            {
                Console.WriteLine("NO");
                return;
            }

            List<char[]> templates = new List<char[]>
            {
                new char[] { '{','}' },
                new char[] { '(',')' },
                new char[] { '[',']' }
            };

            Stack<char> stackParan = new Stack<char>();

            var listLeftParant = new List<char> { '{', '(', '[' };
            for (int i = 0; i < paranthesesInput.Length; i++)
            {
                var currentParan = paranthesesInput[i];

                if (listLeftParant.Contains(currentParan))
                {
                    stackParan.Push(currentParan);
                }
                else if (templates.Any(arr => arr[0] == stackParan.Peek() &&
                                              arr[1] == currentParan))
                {
                    stackParan.Pop();
                }
                else
                {
                    break; // No point to continue if nothing is valid
                }
            }

            Console.WriteLine(stackParan.Count == 0 ? "YES" : "NO");
        }

        public static void E6_TruckTourSecondSolution()
        {
            int numberOfPumps = int.Parse(Console.ReadLine());

            if (numberOfPumps < 1 || numberOfPumps > 1000001)
            {
                Console.WriteLine("Invalid number of pumps!");
                return;
            }

            List<Tuple<int, long>> pumpList = new List<Tuple<int, long>>();

            for (int i = 0; i < numberOfPumps; i++)
            {
                long[] pumpInfo = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();

                pumpList.Add(Tuple.Create(i, pumpInfo[0] - pumpInfo[1]));
            }

            foreach (var tuple in pumpList.Where(tuple => tuple.Item2 >= 0))
            {
                var truckIndex = tuple.Item1;
                var truckGasTank = tuple.Item2;
                for (int i = 1; i < pumpList.Count; i++)
                {
                    if (truckIndex + i < pumpList.Count)
                        truckGasTank += pumpList[truckIndex + i].Item2;
                    else
                        truckGasTank += pumpList[(truckIndex + i) - pumpList.Count].Item2;

                    if (truckGasTank < 0)
                        break;
                }
                if (truckGasTank >= 0)
                {
                    Console.WriteLine(truckIndex);
                    break;
                }
            }
        }

        public static void E6_TruckTourFirstSolution()
        {
            int numberOfPumps = int.Parse(Console.ReadLine());

            if (numberOfPumps < 1 || numberOfPumps > 1000001)
            {
                Console.WriteLine("Invalid number of pumps!");
                return;
            }

            Queue<Tuple<int, long, long>> pumpQueue = new Queue<Tuple<int, long, long>>();

            for (int i = 0; i < numberOfPumps; i++)
            {
                long[] pumpInfo = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();

                pumpQueue.Enqueue(Tuple.Create(i, pumpInfo[0], pumpInfo[1]));
            }

            var isCycleComplited = false;
            while (true)
            {
                var startPump = pumpQueue.Dequeue();
                pumpQueue.Enqueue(startPump);

                long truckGasTank = startPump.Item2;
                long distanceToNextPump = startPump.Item3;
                while (truckGasTank >= distanceToNextPump)
                {
                    truckGasTank -= distanceToNextPump;

                    var nextPump = pumpQueue.Dequeue();
                    pumpQueue.Enqueue(nextPump);

                    if (nextPump == startPump)
                    {
                        isCycleComplited = true;
                        break;
                    }

                    distanceToNextPump = nextPump.Item3;

                    truckGasTank += nextPump.Item2;
                }

                if (isCycleComplited)
                {
                    Console.WriteLine(startPump.Item1);
                    break;
                }
            }
        }

        public static void E5_SequenceWithQueue()
        {
            long number = long.Parse(Console.ReadLine());

            List<long> result = new List<long>();
            result.Add(number);

            Queue<long> currentQueue = new Queue<long>();
            currentQueue.Enqueue(number);

            while (result.Count < 50)
            {
                long currentNum = currentQueue.Dequeue();
                long firstElem = currentNum + 1;
                long secondElem = (currentNum * 2) + 1;
                long thirdElem = currentNum + 2;

                currentQueue.Enqueue(firstElem);
                currentQueue.Enqueue(secondElem);
                currentQueue.Enqueue(thirdElem);

                result.Add(firstElem);
                result.Add(secondElem);
                result.Add(thirdElem);
            }

            Console.WriteLine(string.Join(" ", result.Take(50)));
        }

        public static void E4_BasicQueueOperations()
        {
            int[] specialElems = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] numbersArr = Console.ReadLine()
                 .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            int elemToEnque = specialElems[0];
            int elemToDeque = specialElems[1];
            int elemToLook = specialElems[2];

            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < elemToEnque + elemToDeque; i++)
            {
                if (i < elemToEnque)
                {
                    queue.Enqueue(numbersArr[i]);
                }
                else
                {
                    queue.Dequeue();
                }
            }

            if (queue.Count() == 0)
            {
                Console.WriteLine(0);
            }
            else if (queue.Contains(elemToLook))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine(queue.Min());
            }
        }

        public static void E3_MaximumElement()
        {
            int numberQueries = int.Parse(Console.ReadLine());

            if (numberQueries < 1 || numberQueries > 100000)
            {
                Console.WriteLine("Invalid range of column number!");
                return;
            }

            Stack<long> stackAllElems = new Stack<long>();
            Stack<long> stackMaxElems = new Stack<long>();

            long currentMaxNumber = 0;
            stackMaxElems.Push(currentMaxNumber);

            for (int i = 0; i < numberQueries; i++)
            {
                long[] query = Console.ReadLine().Split(new[] { ' ' },
                                    StringSplitOptions.RemoveEmptyEntries)
                                    .Select(long.Parse)
                                    .ToArray();

                int command = (int)query[0];

                if (command < 1 || command > 3)
                {
                    Console.WriteLine("Invalid command!");
                    continue;
                }

                //switch (command)
                //{
                //    case 1: stackAllElems.Push(query[1]); break;
                //    case 2: stackAllElems.Pop(); break;
                //    case 3: Console.WriteLine(stackAllElems.Max()); break; // Slow operation with .Max
                //}                                                          // with a lot of elements

                // Performance optimized max number
                if (command == 1)
                {
                    long elemNumber = query[1];

                    if (elemNumber < 1 || elemNumber > 1000000000)
                    {
                        Console.WriteLine("Invalid range of number to add!");
                        continue;
                    }

                    stackAllElems.Push(elemNumber);

                    if (elemNumber >= stackMaxElems.Peek()) // Multiple equal max numbers
                    {                                       // should also be pushed 
                        stackMaxElems.Push(elemNumber);
                    }
                }
                else if (command == 2)
                {
                    long popedElem = stackAllElems.Pop();
                    if (popedElem == stackMaxElems.Peek())
                    {
                        stackMaxElems.Pop();
                    }
                }
                else // command == 3
                {
                    if (stackMaxElems.Count() > 1)
                    {
                        Console.WriteLine(stackMaxElems.Peek());
                    }
                    else
                    {
                        Console.WriteLine("No current max number!");
                    }
                }
            }
        }

        public static void E2_BasicStackOperations()
        {
            int[] specialNums = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] numberArr = Console.ReadLine()
                                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

            int elemsToPush = specialNums[0];
            int elemsToPop = specialNums[1];
            int isNumberIn = specialNums[2];

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < elemsToPush + elemsToPop; i++)
            {
                if (i < elemsToPush)
                {
                    stack.Push(numberArr[i]);
                }
                else
                {
                    stack.Pop();
                }
            }

            if (stack.Contains(isNumberIn))
            {
                Console.WriteLine("true");
                return;
            }

            //Console.WriteLine(stack.Count() == 0 ? 0 : stack.Min());

            // Ontly with stack
            if (stack.Count() == 0)
            {
                Console.WriteLine(0);
                return;
            }

            int minNumber = int.MaxValue;
            foreach (int number in stack)
            {
                if (number < minNumber)
                {
                    minNumber = number;
                }
            }

            Console.WriteLine(minNumber);
        }

        public static void E1_ReverseNumbers()
        {
            //int[] numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            //                                  .Select(int.Parse)
            //                                  .ToArray();
            //Stack<int> stack = new Stack<int>(numbers);

            Console.WriteLine(string
                                .Join(" ", new Stack<int>(Console.ReadLine()
                                                            .Split(new[] { ' ' },
                                                                    StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(int.Parse))));
        }
    }
}