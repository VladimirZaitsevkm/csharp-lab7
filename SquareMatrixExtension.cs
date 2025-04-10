using lab7;
using System;

namespace lab7
{
  public static class SquareMatrixExtension
  {
    public static SquareMatrix Transpose(this SquareMatrix matrix)
    {
      SquareMatrix transposed = new SquareMatrix(matrix.Size);

      for (int row = 0; row < matrix.Size; ++row)
      {
        for (int column = 0; column < matrix.Size; ++column)
        {
          transposed._matrix[row, column] = matrix._matrix[column, row];
        }
      }

      return transposed;
    }

    public static int Trace(this SquareMatrix matrix)
    {
      int trace = 0;

      for (int i = 0; i < matrix.Size; ++i)
      {
        trace += matrix._matrix[i, i];
      }

      return trace;
    }
  }
}
