namespace _09_Manual_str_processing_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class Startup
    {
        public static void Main()
        {
            //E1_ReverseString();
            //E2_StringLenght();
            //E3_FormattinNumbers();
            //E4_ConvertString();
            //E5_ConvertNBaseToTenBase();
            //E6_CountSumstringOcc();
            //E7_SumBigNumbers();
            //E8_MultiplyBigNumber();
            //E9_TextFilter();
            //E10_UnicodeCharacters();
            //E11_Palindromes();
            //E12_CharacterMultiplier();
            //E13_MagicExchangeableWords();
            //E14_LettersChangeNumbers();
            //E15_MelrahShake();
            E16_ExtractHyperlinks();

        }

        public static void E16_ExtractHyperlinks()
        {
            // TODO:
            // <a[^>]*\s+(href\s*=\s*(?<link>[^ >]+)) <- ?<link> == group name
            // ^- does not catch properly: "javascript:alert('hi -> "javascript:alert('hi yo')"

            // <a\s*[^>]*\s+(\bhref\s*=\s*(["']?[^>]+["']?)) <- ? == {0,1}
            // <a\s*[^>]*\s+(\bhref\s*=\s*(["']?.+?["'> ]))

            // <a[^>]*\s+href\s*=\s*['"]?(?<link>[^"]+?)['" ]{0,2}(?=[>"]) <- ?<link> == group name
            // ^- does not catch properly: http://www.nakov.com class='new' -> http://www.nakov.com
        }

        public static void E15_MelrahShake()
        {
            string randomChars = Console.ReadLine();
            string pattern = Console.ReadLine();

            int firstMatchIdx = randomChars.IndexOf(pattern);
            int lastMatchIdx = randomChars.LastIndexOf(pattern);

            while (firstMatchIdx != -1 && firstMatchIdx != lastMatchIdx)
            {
                randomChars = randomChars.Remove(firstMatchIdx, pattern.Length);
                randomChars = randomChars.Remove(lastMatchIdx - pattern.Length, pattern.Length);

                Console.WriteLine("Shaked it.");

                pattern = pattern.Remove(pattern.Length / 2, 1);

                if (pattern.Length == 0)
                {
                    break;
                }
                
                firstMatchIdx = randomChars.IndexOf(pattern);
                lastMatchIdx = randomChars.LastIndexOf(pattern);
            }

            Console.WriteLine("No shake.");
            Console.WriteLine(randomChars);    
        }

        public static void E14_LettersChangeNumbers()
        {
            string[] specialNumbers = Console.ReadLine()
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            decimal sum = 0M;
            foreach (string spNumber in specialNumbers)
            {
                var startLetter = spNumber.First();
                var lastLetter = spNumber.Last();
                var strNum = spNumber.Substring(1, spNumber.Length - 2);
                decimal num = decimal.Parse(strNum);

                if (char.IsUpper(startLetter))
                {
                    sum += (num / (startLetter % 64));
                }
                else if (char.IsLower(startLetter))
                {
                    sum += (num * (startLetter % 96));
                }

                if (char.IsUpper(lastLetter))
                {
                    sum -= (lastLetter % 64);
                }
                else if (char.IsLower(lastLetter))
                {
                    sum += (lastLetter % 96);
                }
            }

            Console.WriteLine($"{sum:f2}");
        }

        public static void E13_MagicExchangeableWords()
        {
            string[] inputArg = Console.ReadLine().Split();

            var wordOne = inputArg[0];
            var wordTwo = inputArg[1];

            var maxLenght = wordOne.Length > wordTwo.Length ? wordOne.Length : wordTwo.Length;
            var minLenght = wordOne.Length < wordTwo.Length ? wordOne.Length : wordTwo.Length;

            var smallerWord = wordOne.Length < wordTwo.Length ? wordOne : wordTwo;
            var bigggerWord = wordOne.Length >= wordTwo.Length ? wordOne : wordTwo;

            var magicDict = new Dictionary<char, char>();

            bool isExchangeable = true;
            for (int i = 0; i < maxLenght; i++)
            {
                if (i < minLenght)
                {
                    if (!magicDict.ContainsKey(bigggerWord[i]) && 
                        !magicDict.ContainsValue(smallerWord[i]))
                    {
                        magicDict.Add(bigggerWord[i], smallerWord[i]);
                    }
                    else if (magicDict.ContainsKey(bigggerWord[i]) && 
                            magicDict[bigggerWord[i]] != smallerWord[i])
                    {
                        isExchangeable = false;
                        break;
                    }

                    continue;
                }

                if (!magicDict.ContainsKey(bigggerWord[i]))
                {
                    isExchangeable = false;
                    break;
                }
            }

            Console.WriteLine(isExchangeable ? "true" : "false");
        }

        public static void E12_CharacterMultiplier()
        {
            string[] arg = Console.ReadLine().Split();
            string inputOne = arg[0];
            string inputTwo = arg[1];

            var maxLenght = inputOne.Length > inputTwo.Length ? inputOne.Length : inputTwo.Length;
            var minLenght = inputOne.Length < inputTwo.Length ? inputOne.Length : inputTwo.Length;

            long sum = 0;
            for (int i = 0; i < maxLenght; i++)
            {
                if (i < minLenght)
                {
                    sum += (inputOne[i] * inputTwo[i]);
                }
                else if (inputOne.Length > inputTwo.Length)
                {
                    sum += inputOne[i];
                }
                else
                {
                    sum += inputTwo[i];
                }
            }

            Console.WriteLine(sum);
        }

        public static void E11_Palindromes()
        {
            string[] inputWords = Console.ReadLine()
                .Split(new[] { ' ', ',', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word == String.Concat(word.Reverse()))
                .Distinct()
                .OrderBy(w => w)
                .ToArray();

            Console.WriteLine($"[{string.Join(", ", inputWords)}]");
        }

        public static void E10_UnicodeCharacters()
        {
            var text = Console.ReadLine();

            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                result += "\\u" + ((int)text[i]).ToString("X4").ToLower();
            }

            Console.WriteLine(result);
        }

        public static void E9_TextFilter()
        {
            string[] banWords = Console.ReadLine()
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string text = Console.ReadLine();

            foreach (var word in banWords)
            {
                text = text.Replace(word,new string('*', word.Length));
            }

            Console.WriteLine(text);
        }

        public static void E8_MultiplyBigNumber()
        {
            string firstStrNum = Console.ReadLine().TrimStart('0');
            int secondNum = int.Parse(Console.ReadLine());

            if (secondNum == 0)
            {
                Console.WriteLine(0);
                return;
            }
            else if (secondNum == 1)
            {
                Console.WriteLine(firstStrNum);
                return;
            }

            string result = "";

            long sum = 0;
            for (int i = 0; i < firstStrNum.Length; i++)
            {
                var firstNum = int.Parse(firstStrNum[firstStrNum.Length - 1 - i].ToString());

                sum += (firstNum * secondNum);
                result += sum % 10;
                sum /= 10;
            }

            if (sum > 0)
            {
                result += string.Join("", sum.ToString().Reverse());
            }

            Console.WriteLine(string.Join("", result.Reverse()));
        }

        public static void E7_SumBigNumbers()
        {
            string firstStrNum = Console.ReadLine().TrimStart('0');
            string secondStrNum = Console.ReadLine().TrimStart('0');

            var minLenght = firstStrNum.Length < secondStrNum.Length ? firstStrNum.Length : secondStrNum.Length;
            var maxLenght = firstStrNum.Length > secondStrNum.Length ? firstStrNum.Length : secondStrNum.Length;

            string result = "";

            long sum = 0;
            var firstNum = 0;
            var secondNum = 0;
            for (int i = 0; i < maxLenght; i++)
            {
                if (i < minLenght)
                {
                    firstNum = int.Parse(firstStrNum[firstStrNum.Length - 1 - i].ToString());
                    secondNum = int.Parse(secondStrNum[secondStrNum.Length - 1 - i].ToString());

                    sum += firstNum + secondNum;
                     result += sum % 10;
                    sum /= 10;
                }
                else if (firstStrNum.Length > secondStrNum.Length)
                {
                    firstNum = int.Parse(firstStrNum[firstStrNum.Length - 1 - i].ToString());

                    sum += firstNum;
                    result += sum % 10;
                    sum /= 10;
                }
                else
                {
                    secondNum = int.Parse(secondStrNum[secondStrNum.Length - 1 - i].ToString());

                    sum += secondNum;
                    result += sum % 10;
                    sum /= 10;
                }
            }

            if (sum > 0)
            {
                result = result.Insert(result.Length, string.Join("", sum.ToString().Reverse()));
            }
            
            Console.WriteLine(string.Join("", result.Reverse()));
        }

        public static void E6_CountSumstringOcc()
        {
            string sentence = Console.ReadLine().ToLower();
            string keyWord = Console.ReadLine().ToLower();

            var count = 0;
            var foundWord = sentence.IndexOf(keyWord);

            while (foundWord != -1)
            {
                count++;
                foundWord = sentence.IndexOf(keyWord, foundWord + 1);
            }

            Console.WriteLine(count);
        }

        public static void E5_ConvertNBaseToTenBase()
        {
            var numbersStr = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string bTenNumber = numbersStr[1];

            BigInteger baseNumber = BigInteger.Parse(numbersStr[0]);
            BigInteger baseTenNumber = BigInteger.Parse(numbersStr[1]);

            BigInteger result = new BigInteger(0);

            for (int i = 0; i < bTenNumber.Length; i++)
            {
                BigInteger n = new BigInteger(char.GetNumericValue(bTenNumber[i]));
                result += n * BigInteger.Pow(baseNumber, bTenNumber.Length - i - 1);
            }

            Console.WriteLine(result);
        }

        public static void E4_ConvertString()
        {
            var numbersStr = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            byte baseNumber = byte.Parse(numbersStr[0]);
            BigInteger number = BigInteger.Parse(numbersStr[1]);

            if (number == 0)
            {
                Console.WriteLine(0);
                return;
            }

            string result = "";

            while (number > 0)
            {
                result = number % baseNumber + result;
                number /= baseNumber;
            }

            Console.WriteLine(result);
        }

        public static void E3_FormattinNumbers()
        {
            var numbersStr = Console.ReadLine()
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            long a = long.Parse(numbersStr[0]);
            decimal b = decimal.Parse(numbersStr[1]);
            decimal c = decimal.Parse(numbersStr[2]);

            var hexA = a.ToString("X").PadRight(10, ' ');
            var binaryA = Convert.ToString(a, 2).PadLeft(10, '0');

            if (binaryA.Length > 10)
            {
                binaryA = binaryA.Substring(0, 10);
            }

            Console.WriteLine("|{0}|{1}|{2, 10:f2}|{3, -10:f3}|",
                hexA, binaryA, b, c);
        }

        public static void E2_StringLenght()
        {
            var text = Console.ReadLine();

            string result = text;
            if (text.Length > 20)
            {
                result = text.Substring(0, 20);
            }
            else
            {
                result = text;
            }

            Console.WriteLine(result+ new string('*', 20-result.Length));
        }

        public static void E1_ReverseString()
        {
            var input = Console.ReadLine();
            var sb = new StringBuilder();

            for (int i = input.Length-1; i >= 0; i--)
            {
                sb.Append(input[i]);
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
