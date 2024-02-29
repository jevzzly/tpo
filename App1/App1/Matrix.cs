using System;

namespace App1
{
    public class Matrix
    {
        private int[,] data;

        public int Rows { get; }
        public int Columns { get; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            data = new int[Rows, Columns];
        }

        public int this[int row, int column]
        {
            get => data[row, column];
            set => data[row, column] = value;
        }

        public static Matrix Add(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
            {
                throw new ArgumentException("Matrices must have the same dimensions.");
            }

            int rows = matrix1.Rows;
            int columns = matrix1.Columns;
            Matrix result = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }
    }

}