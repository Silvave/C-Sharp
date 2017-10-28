namespace _13_Functional_programming_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Startup
    {
        public static void Main()
        {
            // #Overkills #NoCodeQuality... just training delegates

            //E1_ActionPrint();
            //E2_KnightsOfHonor();
            //E3_CustomMinFunction();
            //E4_FindEvensOrOdd();
            //E5_AppliedArithmetics();
            //E6_ReverseAndExclude();
            //E7_PredicateForNames();
            //E8_CustomComparator();
            //E9_ListOfPredicates();
            //E10_PredicateParty();
            //E11_PartyReservations(); // Partial solution without objects
            E11_PartyReservationsSolutTwo();
        }

        public static void E11_PartyReservationsSolutTwo()
        {
            var allPartyPeople = Console.ReadLine()
                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(str => new Person(str))
                                        .ToList();

            Func<string, Func<string, string, bool>> filterNames = opt =>
            {
                if (opt == "Starts with")
                {
                    return (n, c) => n.StartsWith(c);
                }
                else if (opt == "Ends with")
                {
                    return (n, c) => n.EndsWith(c);
                }
                else if (opt == "Length")
                {
                    return (n, c) => n.Length == int.Parse(c);
                }
                else // Contains
                {
                    return (n, c) => n.Contains(c);
                }
            };

            string input = Console.ReadLine();
            while (input != "Print")
            {
                var cmd = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                string command = cmd[0];
                string option = cmd[1];
                string criteria = cmd[2];

                Func<string, string, bool> operation;
                switch (command)
                {
                    case "Remove filter":
                        operation = filterNames(option);

                        allPartyPeople
                            .Where(p => operation(p.Name, criteria))
                            .ToList()
                            .ForEach(p => p.Attended = true);
                        break;
                    case "Add filter":
                        operation = filterNames(option);

                        allPartyPeople
                            .Where(p => operation(p.Name, criteria))
                            .ToList()
                            .ForEach(p => p.Attended = false);
                        break;
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(string.Join(" ", allPartyPeople
                                                    .Where(p => p.Attended == true)
                                                    .Select(p => p.Name)));
        }

        class Person
        {
            public Person(string name)
            {
                this.Name = name;
                this.Attended = true;
            }

            public string Name { get; set; }

            public bool Attended { get; set; }
        }

        public static void E11_PartyReservations()
        {
            // This solution gets all peoppe but when printing the results,
            // changes the order of appearance of same names - input:
            //      Pesho Misho Slav Mika Sony Maria Slav Slav
            //      Add filter; Starts with; P
            //      Add filter; Starts with; M
            //      Print
            // right output: Slav Sony Slav Slav

            var allPartyPeople = Console.ReadLine()
                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .GroupBy(s => s) // Needs to be with people with same name
                                        .ToDictionary(g => g.Key, g => g.ToList().Count());

            var filteredPartyPeople = allPartyPeople.ToDictionary(kv => kv.Key, kv => "no filter");

            Func<string, string, List<string>> filterNames = (p, s) =>
            {
                List<string> result = new List<string>();
                if (p == "Starts with")
                {
                    result = filteredPartyPeople.Keys.Where(k => k.StartsWith(s)).ToList();
                }
                else if (p == "Ends with")
                {
                    result = filteredPartyPeople.Keys.Where(k => k.EndsWith(s)).ToList();
                }
                else if (p == "Length")
                {
                    int needLength = int.Parse(s);
                    result = filteredPartyPeople.Keys.Where(k => k.Length == needLength).ToList();
                }
                else if (p == "Contains")
                {
                    result = filteredPartyPeople.Keys.Where(k => k.Contains(s)).ToList();
                }
                return result;
            };

            string input = Console.ReadLine();
            while (input != "Print")
            {
                var cmd = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => s.Trim())
                                        .ToArray();

                string command = cmd[0];
                string option = cmd[1];
                string criteria = cmd[2];

                switch (command)
                {
                    case "Remove filter":
                        filterNames(option, criteria)
                            .ForEach(name => filteredPartyPeople[name] = "no filter");
                        break;
                    case "Add filter":
                        filterNames(option, criteria)
                            .ForEach(name => filteredPartyPeople[name] = "added filter");
                        break;
                }

                input = Console.ReadLine();
            }

            var leftPartyPeople = new StringBuilder();
            foreach (var key in filteredPartyPeople
                                    .Where(kvp => kvp.Value == "no filter")
                                    .Select(kvp => kvp.Key))
            {
                var people = String.Concat(Enumerable.Repeat($"{key} ", allPartyPeople[key]));
                leftPartyPeople.Append(people);
            }

            Console.WriteLine(leftPartyPeople.ToString().TrimEnd());
        }

        public static void E10_PredicateParty()
        {
            var partyPeople = Console.ReadLine()
                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .GroupBy(s => s)
                                        .ToDictionary(g => g.Key, g => g.ToArray().Length);

            Func<string, string, List<string>> filterNames = (p, s) =>
            {
                if (p == "startswith")
                {
                    return partyPeople.Keys.Where(k => k.ToLower().StartsWith(s)).ToList();
                }
                else if (p == "endswith")
                {
                    return partyPeople.Keys.Where(k => k.ToLower().EndsWith(s)).ToList();
                }
                else // Length
                {
                    return partyPeople.Keys.Where(k => k.Length == long.Parse(s)).ToList();
                }
            };

            string input = Console.ReadLine();
            while (input != "Party!")
            {
                var cmd = input.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string command = cmd[0];
                string option = cmd[1];
                string criteria = cmd[2];

                switch (command)
                {
                    case "remove":
                        filterNames(option, criteria)
                            .ForEach(name => partyPeople.Remove(name));
                        break;
                    case "double":
                        filterNames(option, criteria)
                            .ForEach(name => partyPeople[name] *= 2);
                        break;
                }

                input = Console.ReadLine();
            }

            var leftPartyPeople = new StringBuilder();
            foreach (var kvp in partyPeople)
            {
                var people = String.Concat(Enumerable.Repeat($"{kvp.Key}, ", kvp.Value));
                leftPartyPeople.Append(people);
            }

            if (leftPartyPeople.ToString() == string.Empty)
            {
                Console.WriteLine("Nobody is going to the party!");
            }
            else
            {
                Console.WriteLine($"{leftPartyPeople.ToString().TrimEnd(',', ' ')} are going to the party!");
            }
        }

        public static void E9_ListOfPredicates()
        {
            long rangeNum = long.Parse(Console.ReadLine());

            int[] predicateNums = Console.ReadLine()
                                            .Split(' ')
                                            .Select(int.Parse)
                                            .Distinct()
                                            .ToArray();

            Func<long, int, bool> filterNum = (n, p) => n % p == 0;

            // Too slow with memory loss
            //int[] resultNums = Enumerable.Range(1, (int)rangeNum)
            //                            .Where(n => predicateNums.All(p => filterNum(n, p)))
            //                            .ToArray();

            Console.WriteLine(string.Join(" ", FilterRangeNumbers(rangeNum, predicateNums, filterNum)));
        }

        private static HashSet<long> FilterRangeNumbers(long rangeNum, int[] predicateNums,
            Func<long, int, bool> filterNum)
        {
            HashSet<long> resultNums = new HashSet<long>();

            for (long i = 1; i <= rangeNum; i++)
            {
                bool isDivNum = true;
                foreach (var p in predicateNums)
                {
                    if (!filterNum(i, p))
                    {
                        isDivNum = false;
                        break;
                    }
                }

                if (isDivNum)
                {
                    resultNums.Add(i);
                }
            }

            return resultNums;
        }

        public static void E8_CustomComparator()
        {
            double[] arrNums = Console.ReadLine().Split().Select(double.Parse).ToArray();
            double[] secondArrNums = arrNums;

            Action<double[]> sortingOper = arr =>
            {
                bool check = false;
                do
                {
                    check = false;
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        var firstNum = arr[i];
                        var secondNum = arr[i + 1];

                        if (Math.Abs(secondNum % 2) < Math.Abs(firstNum % 2))
                        {
                            arr[i] = secondNum;
                            arr[i + 1] = firstNum;
                            check = true;
                            continue;
                        }

                        if (Math.Abs(secondNum) % 2 != Math.Abs(firstNum) % 2)
                        {
                            continue;
                        }

                        if (firstNum > secondNum)
                        {
                            arr[i] = secondNum;
                            arr[i + 1] = firstNum;
                            check = true;
                            continue;
                        }
                    }
                } while (check);
            };

            sortingOper(arrNums);
            Console.WriteLine(string.Join(" ", arrNums));


            // Second Solution with Array.Sort and custom comprator
            Func<double, double, int> comparator = (a, b) =>
            {
                if (a % 2 == 0 && b % 2 != 0) return -1;
                if (a % 2 != 0 && b % 2 == 0) return 1;
                return a.CompareTo(b);
            };

            Array.Sort(secondArrNums, new Comparison<double>(comparator));
            Console.WriteLine(string.Join(" ", secondArrNums));
        }

        public static void E7_PredicateForNames()
        {
            Func<string> readRow = () => Console.ReadLine();

            Action<string> printName = name => Console.WriteLine(name);

            var lenght = int.Parse(readRow());

            Func<string, bool> checkNameLenght = str => str.Length <= lenght;

            readRow()
                .Split()
                .Where(checkNameLenght)
                .ToList()
                .ForEach(printName);
        }

        public static void E6_ReverseAndExclude()
        {
            Func<string> readRow = () => Console.ReadLine();

            int[] numbers = readRow()
                                .Split()
                                .Select(int.Parse)
                                .ToArray();

            var operNum = int.Parse(readRow());

            Action<int[]> printArr = arr => Console.WriteLine(string.Join(" ", arr
                                                                                .Where(n => n % operNum != 0)
                                                                                .Reverse()));

            printArr(numbers);
        }

        public static void E5_AppliedArithmetics()
        {
            List<int> listNumbers = Console.ReadLine().Split().Select(int.Parse).ToList();

            Action<List<int>> printList = list => Console.WriteLine(string.Join(" ", list));

            Func<string, Func<int, int>> templateNumber = cmd =>
            {
                Func<int, int> result;
                switch (cmd)
                {
                    case "add":
                        result = n => n + 1;
                        break;
                    case "subtract":
                        result = n => n - 1;
                        break;
                    default: // multiply
                        result = n => n * 2;
                        break;
                }

                return result;
            };

            string command = Console.ReadLine();

            while (command != "end")
            {
                if (command == "print")
                {
                    printList(listNumbers);
                }
                else
                {
                    Func<int, int> numberOperation = templateNumber(command);

                    listNumbers = listNumbers.Select(numberOperation).ToList();
                }

                command = Console.ReadLine();
            }
        }

        public static void E4_FindEvensOrOdd()
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList();
            var command = Console.ReadLine();

            Func<string, Func<int, bool>> conditionFunc = cmd =>
            {
                Func<int, bool> result;
                if (cmd == "even")
                    result = n => n % 2 == 0;
                else
                    result = n => n % 2 != 0;

                return result;
            };

            Func<int, bool> checkNumber = conditionFunc(command);

            Console.WriteLine(Enumerable.Range(input[0], input[1] - input[0] + 1)
                                    .Where(checkNumber)
                                    .Aggregate(new StringBuilder(),
                                        (sb, n) => sb.Append($"{n} "))
                                    .ToString()
                                    .Trim());
        }

        public static void E3_CustomMinFunction()
        {
            //Func<int[], int> customMinValue = (arr) =>
            //{
            //    int minNum = int.MaxValue;
            //    foreach (var number in arr)
            //    {
            //        if (number < minNum)
            //        {
            //            minNum = number;
            //        }
            //    }

            //    return minNum;
            //};

            //int[] inputArrNums = Console.ReadLine()
            //    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            //    .Select(int.Parse)
            //    .ToArray();

            //Console.WriteLine(customMinValue(inputArrNums));

            Func<int, int, int> checkMinNum = (a, b) => a < b ? b = a : b;

            Console.WriteLine(Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray()
                .CustomMinNumber(checkMinNum));
        }

        public static int CustomMinNumber(this int[] arr
            , Func<int, int, int> action)
        {
            int minNum = int.MaxValue;
            foreach (var number in arr)
            {
                minNum = action(number, minNum);
            }

            return minNum;
        }

        public static void E2_KnightsOfHonor()
        {
            Action<string> printKnight = k => Console.WriteLine($"Sir {k}");

            Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(k => printKnight(k));
        }

        public static void E1_ActionPrint()
        {
            Action<string> printWord = w => Console.WriteLine(w);

            Console.ReadLine()
                .Split()
                .ToList()
                .ForEach(w => printWord(w));
        }
    }
}
