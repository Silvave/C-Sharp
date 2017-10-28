namespace _14_LINQ_Lab
{
    using System;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        public static void Main()
        {
            // Practicing LINQ
            //E1_TakeTwo();
            //E2_UpperStrings();
            //E3_FirstName();
            //E4_AverageOfDoubles();
            //E5_MinEvenNumber();
            //E6_FindAndSumIntegers();
            //E7_BoundedNumbers();
            //E8_MapDistincts();
        }

        public static void E8_MapDistincts()
        {
            var citiesDict = Console.ReadLine()
                                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(str => str.Split(':'))
                                    .GroupBy(s => s[0], s => long.Parse(s[1]))
                                    .ToDictionary(g => g.Key, g => g.ToList());

            var minPopulation = long.Parse(Console.ReadLine());

            var resultCities = citiesDict
                                    .Where(kv => kv.Value.Sum() >= minPopulation)
                                    .OrderByDescending(kv => kv.Value.Sum())
                                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value
                                                                               .OrderByDescending(e => e)
                                                                               .Take(5)
                                                                               .ToArray());

            foreach (var kvp in resultCities)
            {
                Console.WriteLine($@"{kvp.Key}: {string.Join(" ", kvp.Value)}");
            }
        }

        public static void E7_BoundedNumbers()
        {
            int[] numParams = Console.ReadLine()
                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .OrderBy(e => e)
                                        .ToArray();

            int[] nums = Console.ReadLine()
                                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .Where(n => n >= numParams[0] && n <= numParams[1])
                                        .ToArray();

            Console.WriteLine(string.Join(" ", nums));
        }

        public static void E6_FindAndSumIntegers()
        {
            long[] nums = Console.ReadLine()
                                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Where(str => str.Trim('-').All(c => char.IsDigit(c)))
                                    .Select(long.Parse)
                                    .ToArray();

            Console.WriteLine(nums.Count() != 0 ? nums.Sum().ToString() : "No match");
        }

        public static void E5_MinEvenNumber()
        {
            var result = Console.ReadLine()
                                    .Split()
                                    .Select(double.Parse)
                                    .Where(n => n % 2 == 0);

            Console.WriteLine(result.Count() != 0 ? result.Min().ToString("F2")  : "No match");
        }

        public  static void E4_AverageOfDoubles()
        {
            Console.WriteLine(Console.ReadLine()
                                    .Split()
                                    .Select(double.Parse)
                                    .Average()
                                    .ToString("F2"));
        }

        public static void E3_FirstName()
        {
            string[] names = Console.ReadLine().Split();
            string[] letters = Console.ReadLine().ToLower().Split();

            string firstMatchName = names
                                .Where(n => letters.Contains(n[0].ToString().ToLower()))
                                .OrderBy(e => e)
                                .FirstOrDefault();

            if (firstMatchName == null)
            {
                Console.WriteLine("No match");
            }
            else
            {
                Console.WriteLine(firstMatchName);
            }
        }

        public static void E2_UpperStrings()
        {
            Console.WriteLine(Console.ReadLine()
                                    .Split()
                                    .Select(str => str.ToUpper())
                                    .Aggregate("",
                                        (s, e) => s + e + " ")
                                    .Trim());
        }

        public static void E1_TakeTwo()
        {
            Console.WriteLine(Console.ReadLine()
                                    .Split(new[] { ' ' }, 
                                        StringSplitOptions.RemoveEmptyEntries)
                                    .Select(int.Parse)
                                    .Where(n => n >= 10 && n <= 20)
                                    .Distinct()
                                    .Take(2)
                                    .Aggregate(new StringBuilder(),
                                        (sb, n) => sb.Append($"{n} "))
                                    .ToString()
                                    .Trim());
        }
    }
}
