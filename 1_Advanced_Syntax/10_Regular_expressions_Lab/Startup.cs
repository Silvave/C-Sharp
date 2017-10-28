namespace _10_Regular_expressions_Lab
{
    using System;
    using System.Text.RegularExpressions;

    public class Startup
    {
        public static void Main()
        {
            // Part I: Basic Regex
            //E1_MatchCount();
            //E2_VowelCount();
            //E3_NonDigitCount();
            //E4_ExtractIntegerNumbers();
            //E5_ExtractTags();

            // Part II: Regex Constructs
            //E6_ValidUsernames();
            //E7_ValidTime();
            //E8_ExtractQuotations();
            //E8_ExtractQuotationsSolutTwo();
        }

        public static void E8_ExtractQuotationsSolutTwo()
        {
            string text = Console.ReadLine();

            Regex quotRegex = new Regex(@"(""|')(?<quot>.*?)\1"); // \1 repeats the first group - (""|')

            MatchCollection matches = quotRegex.Matches(text);

            for (int i = 0; i < matches.Count; i++)
            {
                Console.WriteLine(matches[i].Groups["quot"]);
            }
        }

        public static void E8_ExtractQuotations()
        {
            string text = Console.ReadLine();

            Regex quotRegex = new Regex(@"'(?<single>[^\n]+?)'|""(?<double>[^\n]+?)""");

            MatchCollection matches = quotRegex.Matches(text);

            for (int i = 0; i < matches.Count; i++)
            {
                var singleQuots = matches[i].Groups["single"];
                var doubleQuots = matches[i].Groups["double"];

                Console.WriteLine(singleQuots.Success ? singleQuots.Value : doubleQuots.Value);

                //if (singleQuots.Success)
                //{
                //    Console.WriteLine(singleQuots.Value);
                //}
                //else if (doubleQuots.Success)
                //{
                //    Console.WriteLine(doubleQuots.Value);
                //}
            }
        }

        public static void E7_ValidTime()
        {
            string str = Console.ReadLine();

            Regex timeRegex = new Regex(@"^(0[\d]|1[0-2]):([0-5][\d]):([0-5][\d]) (AM|PM)$");

            while (str != "END")
            {
                Match match = timeRegex.Match(str);

                if (match.Success)
                {
                    if (match.Value == "00:00:00 PM" || 
                        match.Value == "00:00:00 AM")
                    {
                        Console.WriteLine("invalid");
                    }
                    else
                    {
                        Console.WriteLine("valid");
                    }
                }
                else
                {
                    Console.WriteLine("invalid");
                }

                str = Console.ReadLine();
            }
        }

        public static void E6_ValidUsernames()
        {
            string text = Console.ReadLine();

            Regex usernameRegex = new Regex(@"^[\w-]{3,16}$");

            while (text != "END")
            {
                Match match = usernameRegex.Match(text);

                Console.WriteLine(match.Success ? "valid" : "invalid");

                text = Console.ReadLine();
            }
        }

        public static void E5_ExtractTags()
        {
            var inputText = Console.ReadLine();

            Regex tagsRegex = new Regex("<.+?>");

            while (inputText != "END")
            {
                MatchCollection tagMatches = tagsRegex.Matches(inputText);

                foreach (var tag in tagMatches)
                {
                    Console.WriteLine(tag);
                }

                inputText = Console.ReadLine();
            }
        }

        public static void E4_ExtractIntegerNumbers()
        {
            var inputText = Console.ReadLine();

            Regex intNumbersRegex = new Regex(@"\d+");

            MatchCollection matches = intNumbersRegex.Matches(inputText);

            foreach (var match in matches)
            {
                Console.WriteLine(match);
            }
        }

        public static void E3_NonDigitCount()
        {
            var inputText = Console.ReadLine();

            Regex nonDigitRegex = new Regex(@"[^\d]");

            MatchCollection matches = nonDigitRegex.Matches(inputText);

            Console.WriteLine($"Non-digits: {matches.Count}");
        }

        public static void E2_VowelCount()
        {
            var inputText = Console.ReadLine();

            Regex vowelRegex = new Regex("[aeiouy]", RegexOptions.IgnoreCase);

            MatchCollection vowelMatches = vowelRegex.Matches(inputText);

            Console.WriteLine($"Vowels: {vowelMatches.Count}");
        }

        public static void E1_MatchCount()
        {
            var searchWord = Console.ReadLine();
            var text = Console.ReadLine();

            Regex regex = new Regex(searchWord);
            MatchCollection matches = regex.Matches(text);

            Console.WriteLine(matches.Count);
        }
    }
}
