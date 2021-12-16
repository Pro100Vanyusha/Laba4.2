using System;

namespace Laba4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix =  {
                                    {2, 5, 4},
                                    {1, 3, 2},
                                    {2, 10, 9}
                                };

            double[] freeTerms = { 30, 150, 110 };

            double[] solution = matrix.SolvingSLAE(freeTerms);

            Console.Write("Розв'язання (X1, X2, X3): ");
            Console.WriteLine(string.Join(", ", solution));
        }
        public static class MatrixCalculation
        {
            public static double Determiner(this double[,] matrix)
            {
                int size = matrix.GetLength(0);
                if (matrix.Length == 0 || size != matrix.GetLength(1))
                    throw new ArgumentException();
                switch (size)
                {
                    case 1: return matrix[0, 0];
                    case 2: return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
                }
                double det = 0;
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        double detLess = matrix[i, j] * RemoveRowCol(matrix, i, j).Determiner();
                        if ((i + j) % 2 == 0)
                            det += detLess;
                        else
                            det -= detLess;
                    }
                return det;
            }
            private static double[,] RemoveRowCol(double[,] matrix, int row, int col)
            {
                int size = matrix.GetLength(0) - 1;

                double[,] ret = new double[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        ret[i, j] = matrix
                            [
                                i + (i >= row ? 1 : 0),
                                j + (j >= col ? 1 : 0)
                            ];
                return ret;
            }
            private static double[,] ReplaceCol(double[,] matrix, double[] vector, int col)
            {
                int size = matrix.GetLength(0);
                double[,] ret = new double[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (j == col)
                            ret[i, j] = vector[i];
                        else
                            ret[i, j] = matrix[i, j];
                return ret;
            }
            public static double[] SolvingSLAE(this double[,] matrix, double[] freeTerms)
            {
                int size = matrix.GetLength(0);
                if (
                        matrix == null || freeTerms == null
                        || matrix.Length == 0 || size != matrix.GetLength(1)
                        || size != freeTerms.Length
                    )
                    throw new ArgumentException();

                double det = matrix.Determiner();
                if (det == 0)
                    return null;
                double[] ret = new double[size];
                for (int i = 0; i < size; i++)
                    ret[i] = ReplaceCol(matrix, freeTerms, i).Determiner() / det;
                return ret;
            }
        }
    }
}
