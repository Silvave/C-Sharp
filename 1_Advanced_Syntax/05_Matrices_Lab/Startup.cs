namespace _05_Matrices_Lab
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            //E1_SumMatrixElements();
            //E2_SquareWithMaximumSum();
            //E3_GroupNumbers();
            //E4_PascalTriangle();
        }

        public static void E4_PascalTriangle()
        {
            var inputNumber = int.Parse(Console.ReadLine());

            long[][] pascalMatrix = new long[inputNumber][];

            for (int row = 0; row < pascalMatrix.Length; row++)
            {
                pascalMatrix[row] = new long[row + 1];

                pascalMatrix[row][0] = 1;
                pascalMatrix[row][pascalMatrix[row].Length-1] = 1;

                if (row > 1)
                {
                    for (int col = 1; col < pascalMatrix[row].Length - 1; col++)
                    {
                        pascalMatrix[row][col] = pascalMatrix[row - 1].Skip(col - 1).Take(2).Sum();
                    }
                }
            }

            foreach (var arrNum in pascalMatrix)
            {
                Console.WriteLine(string.Join(" ", arrNum));
            }
        }

        public static void E3_GroupNumbers()
        {
            long[] inputNumbers = Console.ReadLine()
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            long[][] matrix = new long[3][];

            matrix = matrix.Select(elem => elem = new long[0]).ToArray();

            for (int i = 0; i < inputNumbers.Length; i++)
            {
                int remainderNum = (int)Math.Abs(inputNumbers[i] % 3);

                matrix[remainderNum] = matrix[remainderNum]
                                        .Concat(inputNumbers.Skip(i).Take(1)).ToArray();
            }

            foreach (var arrNums in matrix)
            {
                Console.WriteLine(string.Join(" ", arrNums));
            }
        }

        public static void E2_SquareWithMaximumSum()
        {
            var matrixInfo = Console.ReadLine()
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = matrixInfo[0];
            var cols = matrixInfo[1];

            int[][] matrix = new int[rows][];

            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = Console.ReadLine()
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }

            int[][] maxSubMatrixValues = new int[2][];

            int maxSumSubMatx = 0;
            for (int row = 0; row < matrix.Length - 1; row++)
            {
                for (int col = 0; col < matrix[row].Length - 1; col++)
                {
                    int[] subMatrix2x2 = matrix[row].Skip(col).Take(2)
                                            .Concat(matrix[row + 1].Skip(col).Take(2)).ToArray();

                    if (subMatrix2x2.Sum() > maxSumSubMatx)
                    {
                        maxSumSubMatx = subMatrix2x2.Sum();
                        maxSubMatrixValues[0] = matrix[row].Skip(col).Take(2).ToArray();
                        maxSubMatrixValues[1] = matrix[row + 1].Skip(col).Take(2).ToArray();
                    }
                }
            }


            maxSubMatrixValues
                .ToList()
                .ForEach(arrRow => Console.WriteLine(string.Join(" ", arrRow)));
            Console.WriteLine(maxSumSubMatx);
        }

        public static void E1_SumMatrixElements()
        {
            var matrixInfo = Console.ReadLine()
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = matrixInfo[0];
            var cols = matrixInfo[1];

            //int[,] matrix = new int[rows][cols]; // Columns does not have .Lenght
            // and other prop of an array when 
            // matrix is initialized this way

            int[][] matrix = new int[rows][];

            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = Console.ReadLine()
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }

            Console.WriteLine(matrix.Length);
            Console.WriteLine(matrix[0].Length);
            Console.WriteLine(matrix.Select(arrCols => arrCols.Sum()).Sum());
        }
    }
}
