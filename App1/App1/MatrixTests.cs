using System;
using Xunit;
using FluentAssertions;

namespace App1
{

    public class MatrixTests
    {
        
        [Fact]
        public void Add_ShouldThrowArgumentException_WhenMatricesHaveDifferentDimensions()
        {
            Matrix matrix1 = new Matrix(2, 2);
            Matrix matrix2 = new Matrix(2, 3);
            
            Assert.Throws<ArgumentException>(() => Matrix.Add(matrix1, matrix2));
        }
        
        [Fact]
        public void Add_ShouldHaveSameDimensions()
        {
            Matrix matrix1 = new Matrix(2, 3);
            Matrix matrix2 = new Matrix(2, 3);

            Matrix result = Matrix.Add(matrix1, matrix2);

            result.Rows.Should().Be(matrix1.Rows);
            result.Columns.Should().Be(matrix1.Columns);
        }

        [Theory]
        [InlineData(2, 3, 1, 2, 3, 4, 5, 6, 2, 4, 6, 8, 10, 12)]
        [InlineData(2, 3, 10, 20, 30, 40, 50, 60, 20, 40, 60, 80, 100, 120)]
        public void Add_ShouldReturnSumOfMatrices(int rows, int columns, params int[] values)
        {
            Matrix matrix1 = new Matrix(rows, columns);
            Matrix matrix2 = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix1[i, j] = values[i * columns + j];
                    matrix2[i, j] = values[rows * columns + i * columns + j];
                }
            }

            Matrix result = Matrix.Add(matrix1, matrix2);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Assert.Equal(values[i * columns + j] + values[rows * columns + i * columns + j], result[i, j]);
                }
            }
        }

    }

}