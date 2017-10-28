namespace _12_Functional_programming_Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using MyExtensions;

    public static class Startup
    {
        public static void Main()
        {
            //E1_SortNumbers();
            //E2_SumNumbers();
            //E3_PrintUppercaseWords();
            //E4_AddVAT();
            E5_FilterByAge();

            // Delegates and Expression trees
            //Testing();
            //TestingFuncs();
        }

        public static void TestingFuncs()
        {
            Func<int, Func<int, bool>, bool> testCondition = (n, condtion) => condtion(n);

            Action<bool> printResult = (b) => Console.WriteLine(b);

            printResult(testCondition(100, n => n > 100));
            printResult(testCondition(100, n => n == 100));
            printResult(testCondition(100, n => n < 100));

            var arrNums = new int[4] { 1, 2, 3, 4 };
            var asd = arrNums.Filter(n => n % 2 == 0).ForEach(n => Console.WriteLine(n + 100));

            var set = new HashSet<string>();
            set.Add("Gosho");
            set.Add("Mishoo");
            set.Add("Bobobob");
            set.Add("Mobobobo");
            set.Add("Johny");
            
            set
              .Filter(name => name.Length < 6 || name.StartsWith("M"))
              .ForEach(name => Console.WriteLine($"Name: {name}"));
        }

        public static void Testing()
        {
            Func<int> myNumber = () => 1;

            Expression<Func<int, int, int>> myExpression = (a, b) => a + b;

            int number = myNumber();

            var expTreeBody = myExpression.Body;
            var expTree = myExpression.Compile();

            Console.WriteLine(number);
            Console.WriteLine(expTreeBody);
            Console.WriteLine(expTree(1, 2));
            Console.WriteLine(myExpression.Compile()(1, 2));
        }

        public static void E5_FilterByAge()
        {
            int lines = int.Parse(Console.ReadLine());

            List<Person> people = new List<Person>();

            Func<string[]> inputPerson = () => Console.ReadLine().Split(',');

            Func<string[], Person> parsePerson = (arr) => new Person()
            {
                Name = arr[0].Trim(),
                Age = arr[1].Trim().parseInt()
            };

            for (int i = 0; i < lines; i++)
            {
                var person = parsePerson(inputPerson());
                people.Add(person);
            }

            string condition = Console.ReadLine();
            int age = Console.ReadLine().parseInt();
            Func<Person, string> format = Console.ReadLine()
                                            .Split(new[] { ' ' }, 
                                                StringSplitOptions.RemoveEmptyEntries)
                                            .PersonFormat();



            people
                .FilterPeople(AgeLimit(condition), age)
                .PrintPeopleInfo(p => Console.WriteLine(format(p)));
        }

        public class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        public static Func<Person, string> PersonFormat(this string[] personFormats)
        {
            Func<Person, string> format;

            if (personFormats.Count() == 2)
            {
                format = p => $"{p.Name} - {p.Age}";
            }
            else if (personFormats[0] == "name")
            {
                format = p => $"{p.Name}";
            }
            else
            {
                format = p => $"{p.Age}";
            }

            return format;
        }

        public static void PrintPeopleInfo(
            this List<Person> peopleCollection,
            Action<Person> action)
        {
            foreach (var person in peopleCollection)
            {
                action(person);
            }
        }

        //public static void PrintPeopleInfo(
        //    this List<Person> peopleCollection,
        //    Func<Person, string> format)
        //{   
        //    foreach (var person in peopleCollection)
        //    {
        //        Console.WriteLine(format(person));
        //    }
        //}

        public static List<Person> FilterPeople(
            this List<Person> collectionPeople,
            Func<Person, int, bool> action,
            int age)
        {
            var result = new List<Person>();
            foreach (var person in collectionPeople)
            {
                if (action(person, age))
                {
                    result.Add(person);
                }
            }

            return result;
        }

        public static Func<Person ,int, bool> AgeLimit(string ageType)
        {
            Func<Person, int, bool> result;
            if (ageType == "older")
            {
                result = (p, n) => p.Age >= n;
            }
            else
            {
                result = (p, n) => p.Age < n;
            }

            return result;
        }

        public static int parseInt(this string strAge)
        {
            return int.Parse(strAge);
        }

        public static void E4_AddVAT()
        {
            Func<string[]> inputArr = () => Console.ReadLine()
                                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Func<string, double> doubleParse = (str) => double.Parse(str.Trim());

            Func<string[], List<double>> arrNumbers = arr => arr.Select(doubleParse).ToList();

            Action<List<double>> printVatNumbers = arr => arr.ForEach(n => Console.WriteLine($"{n * 1.20:f2}"));

            printVatNumbers(arrNumbers(inputArr()));
        }

        public static void E3_PrintUppercaseWords()
        {
            Func<string[]> input = () => Console.ReadLine()
                                                .Split(new[] { ' ' },
                                                    StringSplitOptions.RemoveEmptyEntries);

            Func<string[], List<string>> uppercaseWords = arr => arr.Where(w => char.IsUpper(w[0])).ToList();

            Action<List<string>> printList = list => list.ForEach(word => Console.WriteLine(word));

            printList(uppercaseWords(input()));
        }

        public static void E2_SumNumbers()
        {
            // Practicing Actions and Funcs
            Func<string> readStr = () => Console.ReadLine();

            Func<string, int> parseInt = int.Parse;

            Func<string, int[]> strToArrInt = (input) => input
                                                .Split(new string[] { ", " }, 
                                                    StringSplitOptions.RemoveEmptyEntries)
                                                .Select(parseInt)
                                                .ToArray();

            Action<int[]> result = (arr) => Console.WriteLine(arr.Count());
            result += (arr) => Console.WriteLine(arr.Sum());

            result(strToArrInt(readStr()));
        }

        public static void E1_SortNumbers()
        {
            // Using only func programming - Lambda
            //Console.WriteLine(string.Join(", ", Console.ReadLine()
            //                                        .Split(new string[] { ", " }, 
            //                                                StringSplitOptions.RemoveEmptyEntries)
            //                                        .Select(int.Parse)
            //                                        .Where(n => n % 2 == 0)
            //                                        .OrderBy(n => n)));

            Console.WriteLine(Console.ReadLine()
                                .Split(new string[] { ", " },
                                        StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .Where(n => n % 2 == 0)
                                .OrderBy(n => n)
                                .Aggregate(new StringBuilder(),
                                    (sb, num) => sb.Append($"{num}, "))
                                .ToString()
                                .TrimEnd(',', ' '));
        }
    }
}
