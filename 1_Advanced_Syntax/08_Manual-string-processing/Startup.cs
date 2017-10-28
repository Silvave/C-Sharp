namespace _08_Manual_string_processing_Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        public static void Main()
        {
            //E1_StudentResults();
            //E2_ParseUrls();
            //E3_ParseTags();
            //E4_SpecialWords();
            //E5_ConcatString();

        }

        public static void E5_ConcatString()
        {
            var rowsNumber = int.Parse(Console.ReadLine());

            var sb = new StringBuilder();

            for (int i = 0; i < rowsNumber; i++)
            {
                var word = Console.ReadLine();
                sb.Append($"{word} ");
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }

        public static void E4_SpecialWords()
        {
            var specialWords = new Dictionary<string, int>();

            var specialWordsInput = Console.ReadLine()
                .Split(new[] { '(', ')', '[', ']', '<', '>', ',', '-', '!', '?', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToArray();

            var inputText = Console.ReadLine()
                .Split(new[] { '(', ')', '[', ']', '<', '>', ',', '-', '!', '?', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < specialWordsInput.Length; i++)
            {
                var word = specialWordsInput[i];
                if (!specialWords.ContainsKey(word))
                {
                    specialWords.Add(word, 0);
                }

                for (int m = 0; m < inputText.Length; m++)
                {
                    if (inputText[m] == word)
                    {
                        specialWords[word]++;
                    }
                }
            }

            foreach (var kvp in specialWords)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }
        }

        public static void E3_ParseTags()
        {
            var inputText = Console.ReadLine();

            var opedTag = "<upcase>";
            var closeTag = "</upcase>";

            var startIdx = inputText.IndexOf(opedTag);
            while (startIdx != -1)
            {
                var endIdx = inputText.IndexOf(closeTag, startIdx + 1);

                if (endIdx == -1)
                {
                    break;
                }

                var tagsText = inputText.Substring(startIdx, endIdx + closeTag.Length - startIdx);

                var filteredTagsText = tagsText.Replace(opedTag, string.Empty)
                                            .Replace(closeTag, string.Empty).ToUpper();

                inputText = inputText.Replace(tagsText, filteredTagsText);

                startIdx = inputText.IndexOf(opedTag);
            }

            Console.WriteLine(inputText);
        }

        public static void E2_ParseUrls()
        {
            string protocolSep = "://";
            string resourceSep = "/";

            string[] urlInfo = Console.ReadLine()
                .Split(new string[] { protocolSep }, StringSplitOptions.None);

            if (urlInfo.Length != 2)
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var protocol = urlInfo[0];
            var serverIndex = urlInfo[1].IndexOf(resourceSep);

            if (serverIndex == -1)
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var server = urlInfo[1].Substring(0, serverIndex);
            var resources = urlInfo[1].Substring(serverIndex+1);

            Console.WriteLine($"Protocol = {protocol}");
            Console.WriteLine($"Server = {server}");
            Console.WriteLine($"Resources = {resources}");
        }

        public static void E1_StudentResults()
        {
            int number = int.Parse(Console.ReadLine());

            var studentGradesInfo = new Dictionary<string, List<double>>();

            for (int i = 0; i < number; i++)
            {
                string[] studentInfo = Console.ReadLine().Split('-');
                string studentName = studentInfo[0].Trim();
                double[] studentGrades = studentInfo[1]
                    .Split(',')
                    .Select(str => double.Parse(str.Trim()))
                    .ToArray();

                if (!studentGradesInfo.ContainsKey(studentName))
                {
                    studentGradesInfo.Add(studentName, new List<double>());
                }

                studentGradesInfo[studentName].AddRange(studentGrades);
            }

            Console.WriteLine($"{"Name",-10}|{"CAdv",7}|{"COOP",7}|{"AdvOOP",7}|{"Average",7}|");

            foreach (var kvp in studentGradesInfo)
            {
                var name = kvp.Key;
                var grades = kvp.Value;
                Console.WriteLine("{0,-10}|{1,7:f2}|{2,7:f2}|{3,7:f2}|{4,7:f4}|",
                                   name, grades[0], grades[1], grades[2], grades.Average());
            }
        }
    }
}
