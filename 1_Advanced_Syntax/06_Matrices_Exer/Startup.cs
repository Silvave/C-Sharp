namespace _06_Matrices_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            //E1_MatrixOfPalindromes();
            //E2_DiagonalDifference();
            //E3_2x2SquaresInMatrix();
            //E4_MaximalSum();
            //E5_RubiksMatrix();
        }

        public static void E5_RubiksMatrix()
        {
            // TODO:
        }

        public static void E4_MaximalSum()
        {
            var matrixParams = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            long rows = matrixParams[0];
            long cols = matrixParams[1];

            long[][] matrix = new long[rows][];

            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine()
                                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(long.Parse)
                                .ToArray();
            }

            long maxSum = 0;
            long[][] maxSquareMatrix = new long[3][];
            for (int row = 0; row < rows - 2; row++)
            {
                for (int col = 0; col < cols - 2; col++)
                {
                    var topArr = matrix[row].Skip(col).Take(3).ToArray();
                    var middleArr = matrix[row + 1].Skip(col).Take(3).ToArray();
                    var bottomArr = matrix[row + 2].Skip(col).Take(3).ToArray();
                    var arr3x3 = topArr.Concat(middleArr.Concat(bottomArr)).ToArray();

                    if (arr3x3.Sum() > maxSum)
                    {
                        maxSum = arr3x3.Sum();
                        maxSquareMatrix[0] = topArr;
                        maxSquareMatrix[1] = middleArr;
                        maxSquareMatrix[2] = bottomArr;
                    }
                }
            }

            Console.WriteLine($"Sum = {maxSum}");
            foreach (var arrNumbers in maxSquareMatrix)
            {
                Console.WriteLine(string.Join(" ", arrNumbers));
            }
        }

        public static void E3_2x2SquaresInMatrix()
        {
            var arrParams = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

            char[][] matrix = new char[arrParams[0]][];

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();
            }

            int equalCharMatrix2x2 = 0;
            for (int row = 0; row < matrix.Length - 1; row++)
            {
                for (int col = 0; col < matrix[row].Length - 1; col++)
                {
                    char[] top = matrix[row].Skip(col).Take(2).ToArray();
                    char[] bottom = matrix[row + 1].Skip(col).Take(2).ToArray();

                    if (top.Concat(bottom).Distinct().Count() == 1)
                    {
                        equalCharMatrix2x2++;
                    }
                }
            }
            Console.WriteLine(equalCharMatrix2x2);
        }

        public static void E2_DiagonalDifference()
        {
            var num = int.Parse(Console.ReadLine());

            //int[][] matrix = new int[num][];

            //var leftSum = 0;
            //var rightSum = 0;
            var sum = 0;
            for (int i = 0; i < num; i++)
            {
                var input = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                //leftSum += matrix[i][i];
                //rightSum += matrix[i][matrix.Length - 1 - i];
                sum += input[i] - input[input.Length - 1 - i];
            }

            Console.WriteLine(Math.Abs(sum));
        }

        public static void E1_MatrixOfPalindromes()
        {
            var matrixParams = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            var row = matrixParams[0];
            var col = matrixParams[1];

            string[][] matrix = new string[row][];

            for (int i = 0; i < row; i++)
            {
                matrix[i] = new string[col];

                for (int j = 0; j < col; j++)
                {
                    matrix[i][j] = "" + alphabet[i] + alphabet[i + j] + alphabet[i];
                }
            }

            foreach (var arrPalindromes in matrix)
            {
                Console.WriteLine(string.Join(" ", arrPalindromes));
            }
        }
    }
}