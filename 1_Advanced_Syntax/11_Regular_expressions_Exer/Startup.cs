namespace _11_Regular_expressions_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            //E1_MatchFullName();
            //E2_MatchPhoneNumber();
            //E3_SeriesOfLetters();
            //E4_ReplaceTag();
            //E5_ExtractEmail();
            //E6_SentenceExtractor();
            E7_ValidUsernames();
        }

        public static void E7_ValidUsernames()
        {
            // TODO
        
        }

        public static void E6_SentenceExtractor()
        {
            string keyWord = Console.ReadLine();
            string text = Console.ReadLine();

            Regex regex = new Regex($@"(?>^|\s)((\w+\s)*{keyWord}(\s\w+)*)([\.!?])");

            MatchCollection matches = regex.Matches(text);

            for (int i = 0; i < matches.Count; i++)
            {
                Console.WriteLine(matches[i].Value.Trim());
            }
        }

        public static void E5_ExtractEmail()
        {
            string inputText = Console.ReadLine();

            string emailUser = @"(?:^|\s+)([A-Za-z][\w\.-]*?[A-Za-z])";
            string emailHost = @"([a-z][a-z-]*[a-z])(\.[a-z][a-z-]*[a-z])+(?=\s+|[\.!?,]|$)";

            Regex emailRegex = new Regex($@"{emailUser}@{emailHost}");

            MatchCollection emailMatches = emailRegex.Matches(inputText);

            for (int i = 0; i < emailMatches.Count; i++)
            {
                Console.WriteLine(emailMatches[i].Value.Trim());
            }
        }

        public static void E4_ReplaceTag()
        {
            string inputTag = Console.ReadLine();

            Regex tagRegex = new Regex(@"(\s*)<a (href="".*"")>(.*)<\/a>(\s*)");

            while (inputTag != "end")
            {
                inputTag = tagRegex.Replace(inputTag, @"$1[URL $2]$3[/URL]$4");

                Console.WriteLine(inputTag);

                inputTag = Console.ReadLine();
            }
        }

        public static void E3_SeriesOfLetters()
        {
            var inputText = Console.ReadLine();

            inputText = Regex.Replace(inputText, @"([a-z])\1+", "$1");

            Console.WriteLine(inputText);
        }

        public static void E2_MatchPhoneNumber()
        {
            var inputPhoneNumber = Console.ReadLine();

            Regex phoneRegex = new Regex(@"(^| )\+359( |-)2\2\d{3}\2\d{4}$");

            while (inputPhoneNumber != "end")
            {
                Match match = phoneRegex.Match(inputPhoneNumber);

                if (match.Success)
                {
                    Console.WriteLine(match);
                }

                inputPhoneNumber = Console.ReadLine();
            }
        }

        public static void E1_MatchFullName()
        {
            var inputName = Console.ReadLine();

            Regex fullnameRegex = new Regex(@"[A-Z][a-z]+ [A-Z][a-z]+");

            while (inputName != "end")
            {
                Match match = fullnameRegex.Match(inputName);

                if (match.Success)
                {
                    Console.WriteLine(match);
                }

                inputName = Console.ReadLine();
            }
        }
    }
}
