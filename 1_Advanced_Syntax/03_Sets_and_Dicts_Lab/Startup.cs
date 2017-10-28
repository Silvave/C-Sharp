namespace _03_Sets_and_Dicts_Lab
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            // Sets
            //E1_ParkingLot();
            //E2_SoftUniParty();

            // Dictionaries
            //E3_CountSameValuesInArray();
            //E4_AcademyGraduation();

            // Testing stuctures speed
            //Testing();
        }

        public static void Testing()
        {
            var watch = new Stopwatch();

            // testing adding speeds
            var list = new List<int>();

            Console.WriteLine("1000000 Elements add to:");
            watch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(i);
            }
            watch.Stop();

            Console.WriteLine($"  List for {watch.ElapsedTicks} ticks");

            var hashSet = new HashSet<int>();

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                hashSet.Add(i);
            }
            watch.Stop();

            Console.WriteLine($"  HashSet for {watch.ElapsedTicks} ticks");

            var sortedSet = new SortedSet<int>();

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                sortedSet.Add(i);
            }
            watch.Stop();

            Console.WriteLine($"  SortedSet for {watch.ElapsedTicks} ticks");

            var dict = new Dictionary<int, int>();

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                dict.Add(i, i);
            }
            watch.Stop();

            Console.WriteLine($"  Dictionary for {watch.ElapsedTicks} ticks");

            var sortedDict = new SortedDictionary<int, int>();

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                sortedDict.Add(i, i);
            }
            watch.Stop();

            Console.WriteLine($"  SortedDictionary for {watch.ElapsedTicks} ticks");

            Console.WriteLine(new string('-', 50));

            // Testing searching speeds
            Console.WriteLine("Found a value in:");

            watch.Restart();
            list.Contains(888888);
            watch.Stop();
            Console.WriteLine($"  List for {watch.ElapsedTicks} ticks");

            watch.Restart();
            hashSet.Contains(888888);
            watch.Stop();
            Console.WriteLine($"  HashSet for {watch.ElapsedTicks} ticks");

            watch.Restart();
            sortedSet.Contains(888888);
            watch.Stop();
            Console.WriteLine($"  SortedSet for {watch.ElapsedTicks} ticks");

            watch.Restart();
            dict.ContainsKey(888888);
            watch.Stop();
            Console.WriteLine($"  Dictionary - key for {watch.ElapsedTicks} ticks");

            watch.Restart();
            dict.ContainsValue(888888);
            watch.Stop();
            Console.WriteLine($"  Dictionary - value for {watch.ElapsedTicks} ticks");

            watch.Restart();
            sortedDict.ContainsKey(888888);
            watch.Stop();
            Console.WriteLine($"  SortedDictionary - key for {watch.ElapsedTicks} ticks");

            watch.Restart();
            sortedDict.ContainsValue(888888);
            watch.Stop();
            Console.WriteLine($"  SortedDictionary - value for {watch.ElapsedTicks} ticks");

            Console.WriteLine(new string('-', 50));

            // Testing remove speed
            Console.WriteLine("Removing 10000 elements in:");
            watch.Restart();
            for (int i = 0; i < 10000; i++)
            {
                list.Remove(i);
            }
            watch.Stop();

            Console.WriteLine($"  List: {watch.ElapsedTicks} ticks");

            watch.Restart();
            for (int i = 0; i < 10000; i++)
            {
                hashSet.Remove(i);
            }
            watch.Stop();

            Console.WriteLine($"  HashSet: {watch.ElapsedTicks} ticks");

            watch.Restart();
            for (int i = 0; i < 10000; i++)
            {
                sortedSet.Remove(i);
            }
            watch.Stop();

            Console.WriteLine($"  SortedSet: {watch.ElapsedTicks} ticks");

            watch.Restart();
            for (int i = 0; i < 10000; i++)
            {
                dict.Remove(i);
            }
            watch.Stop();

            Console.WriteLine($"  Dictionary: {watch.ElapsedTicks} ticks");

            watch.Restart();
            for (int i = 0; i < 10000; i++)
            {
                sortedDict.Remove(i);
            }
            watch.Stop();

            Console.WriteLine($"  SortedDictionary: {watch.ElapsedTicks} ticks");
        }

        public static void E4_AcademyGraduation()
        {
            var studentsSortedDic = new SortedDictionary<string, List<double>>();

            var numberOfStudents = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfStudents; i++)
            {
                var studentName = Console.ReadLine();
                var scores = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(strNum => double.Parse(strNum, CultureInfo.InvariantCulture))
                    .ToList(); // Should be with decimal not with double because 
                               // precise math operations are not correct.. in judge is double
                studentsSortedDic.Add(studentName, scores);
            }

            foreach (var kvp in studentsSortedDic)
            {
                Console.WriteLine($"{kvp.Key} is graduated with {kvp.Value.Average()}");
            }
        }

        public static void E3_CountSameValuesInArray()
        {
            var dicNumbers = new SortedDictionary<decimal, int>();

            Console.ReadLine().Split(new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(strNum =>
                {
                    var num = decimal.Parse(strNum);
                    if (!dicNumbers.ContainsKey(num))
                    {
                        dicNumbers[num] = 0;
                    }
                    dicNumbers[num]++;
                });

            foreach (var kvp in dicNumbers)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value} times");
            }
        }

        public static void E2_SoftUniParty()
        {
            string cmd = Console.ReadLine();

            var people = new SortedSet<string>();

            while (cmd != "PARTY")
            {
                people.Add(cmd);

                cmd = Console.ReadLine();
            }

            cmd = Console.ReadLine();
            while (cmd != "END")
            {
                people.Remove(cmd);

                cmd = Console.ReadLine();
            }

            Console.WriteLine(people.Count);

            Console.WriteLine(string.Join("\n", people));
        }

        public static void E1_ParkingLot()
        {
            string[] args = Console.ReadLine().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            SortedSet<string> carNumbers = new SortedSet<string>();

            while (args[0] != "END")
            {
                var command = args[0];
                var number = args[1];

                switch (command)
                {
                    case "IN":
                        carNumbers.Add(number);
                        break;
                    case "OUT":
                        carNumbers.Remove(number);
                        break;
                }

                args = Console.ReadLine().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            }

            Console.WriteLine(carNumbers.Count > 0 ? string.Join("\n", carNumbers) : "Parking Lot is Empty");
        }
    }
}
