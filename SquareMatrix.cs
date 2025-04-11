using System;

namespace lab7
{
  public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
  {
    private static Random s_random = new Random();
    public int[,] _matrix;
    public int Size { get; private set; }

    public SquareMatrix(int size)
    {
      if (size <= 0) throw new MatrixSizeException("Размер матрицы был указан некорректно");
      Size = size;
      _matrix = new int[size, size];
      GenerateSquareMatrix();
    }

    public void GenerateSquareMatrix()
    {
      for (int rowIndex = 0; rowIndex < Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex)
        {
          _matrix[rowIndex, columnIndex] = s_random.Next(1000);
        }
      }
    }

    public static SquareMatrix operator +(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      if (firstMatrix.Size != secondMatrix.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      SquareMatrix result = new SquareMatrix(firstMatrix.Size);

      for (int rowIndex = 0; rowIndex < firstMatrix.Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; ++columnIndex)
        {
          result._matrix[rowIndex, columnIndex] = firstMatrix._matrix[rowIndex, columnIndex] + secondMatrix._matrix[rowIndex, columnIndex];
        }
      }

      return result;
    }

    public static SquareMatrix operator -(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      if (firstMatrix.Size != secondMatrix.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      SquareMatrix result = new SquareMatrix(firstMatrix.Size);

      for (int rowIndex = 0; rowIndex < firstMatrix.Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; ++columnIndex)
        {
          result._matrix[rowIndex, columnIndex] = firstMatrix._matrix[rowIndex, columnIndex] - secondMatrix._matrix[rowIndex, columnIndex];
        }
      }

      return result;
    }

    public static SquareMatrix operator *(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      if (firstMatrix.Size != secondMatrix.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      SquareMatrix result = new SquareMatrix(firstMatrix.Size);

      for (int rowIndex = 0; rowIndex < firstMatrix.Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; ++columnIndex)
        {
          result._matrix[rowIndex, columnIndex] = 0;
          for (int elementIndex = 0; elementIndex < firstMatrix.Size; ++elementIndex)
          {
            result._matrix[rowIndex, columnIndex] += firstMatrix._matrix[rowIndex, elementIndex] * secondMatrix._matrix[elementIndex, columnIndex];
          }
        }
      }

      return result;
    }

    public static int operator !(SquareMatrix matrix)
    {
      return CalculateDeterminant(matrix._matrix, matrix.Size);
    }

    public static double[,] operator ~(SquareMatrix matrix)
    {
      int determinant = !matrix;
      if (determinant == 0)
      {
        throw new MatrixDeterminantZeroException("Обратной матрицы не существует, так как определитель равен нулю.");
      }

      double[,] inverseMatrix = new double[matrix.Size, matrix.Size];
      int[,] adjugateMatrix = GetAdjugateMatrix(matrix._matrix, matrix.Size);

      for (int rowIndex = 0; rowIndex < matrix.Size; rowIndex++)
      {
        for (int columnIndex = 0; columnIndex < matrix.Size; columnIndex++)
        {
          inverseMatrix[rowIndex, columnIndex] = (double)adjugateMatrix[rowIndex, columnIndex] / determinant;
        }
      }

      return inverseMatrix;
    }

    public static bool operator >(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      if (firstMatrix.Size != secondMatrix.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      int firstSum = 0, secondSum = 0;
      for (int rowIndex = 0; rowIndex < firstMatrix.Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; ++columnIndex)
        {
          firstSum += firstMatrix._matrix[rowIndex, columnIndex];
          secondSum += secondMatrix._matrix[rowIndex, columnIndex];
        }
      }

      return firstSum > secondSum;
    }

    public static bool operator <(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      if (firstMatrix.Size != secondMatrix.Size)
      {
        throw new MatrixSizeException("Матрицы должны быть одного размера");
      }

      int firstSum = 0, secondSum = 0;
      for (int rowIndex = 0; rowIndex < firstMatrix.Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; ++columnIndex)
        {
          firstSum += firstMatrix._matrix[rowIndex, columnIndex];
          secondSum += secondMatrix._matrix[rowIndex, columnIndex];
        }
      }

      return firstSum < secondSum;
    }

    public static bool operator >=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      return firstMatrix > secondMatrix || firstMatrix == secondMatrix;
    }

    public static bool operator <=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      return firstMatrix < secondMatrix || firstMatrix == secondMatrix;
    }

    public static bool operator ==(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      if (ReferenceEquals(firstMatrix, secondMatrix))
        return true;

      if (firstMatrix is null || secondMatrix is null)
        return false;

      if (firstMatrix.Size != secondMatrix.Size)
        return false;

      for (int rowIndex = 0; rowIndex < firstMatrix.Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; ++columnIndex)
        {
          if (firstMatrix._matrix[rowIndex, columnIndex] != secondMatrix._matrix[rowIndex, columnIndex])
            return false;
        }
      }

      return true;
    }

    public static bool operator !=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
      return !(firstMatrix == secondMatrix);
    }

    public static explicit operator int(SquareMatrix matrix)
    {
      return !matrix;
    }

    public static explicit operator double[,](SquareMatrix matrix)
    {
      return ~matrix;
    }

    public static bool operator true(SquareMatrix matrix)
    {
      return !matrix != 0;
    }

    public static bool operator false(SquareMatrix matrix)
    {
      return !matrix == 0;
    }

    public override string ToString()
    {
      string result = "";
      for (int rowIndex = 0; rowIndex < Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex)
        {
          result += _matrix[rowIndex, columnIndex] + "\t";
        }
        result += "\n";
      }
      return result;
    }

    public int CompareTo(SquareMatrix other)
    {
      if (other == null) return 1;

      int thisSum = 0, otherSum = 0;
      for (int rowIndex = 0; rowIndex < Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex)
        {
          thisSum += _matrix[rowIndex, columnIndex];
          otherSum += other._matrix[rowIndex, columnIndex];
        }
      }

      return thisSum.CompareTo(otherSum);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      SquareMatrix other = (SquareMatrix)obj;
      return this == other;
    }

    public override int GetHashCode()
    {
      int hash = 17;
      for (int rowIndex = 0; rowIndex < Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex)
        {
          hash = hash * 23 + _matrix[rowIndex, columnIndex].GetHashCode();
        }
      }
      return hash;
    }

    public object Clone()
    {
      SquareMatrix clone = new SquareMatrix(Size);
      for (int rowIndex = 0; rowIndex < Size; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < Size; ++columnIndex)
        {
          clone._matrix[rowIndex, columnIndex] = _matrix[rowIndex, columnIndex];
        }
      }
      return clone;
    }

    private static int CalculateDeterminant(int[,] matrix, int size)
    {
      if (size == 1)
        return matrix[0, 0];

      if (size == 2)
        return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

      int determinant = 0;
      int sign = 1;

      for (int columnIndex = 0; columnIndex < size; columnIndex++)
      {
        int[,] subMatrix = GetSubMatrix(matrix, size, 0, columnIndex);
        determinant += sign * matrix[0, columnIndex] * CalculateDeterminant(subMatrix, size - 1);
        sign = -sign;
      }

      return determinant;
    }

    private static int[,] GetAdjugateMatrix(int[,] matrix, int size)
    {
      int[,] adjugateMatrix = new int[size, size];

      for (int rowIndex = 0; rowIndex < size; rowIndex++)
      {
        for (int columnIndex = 0; columnIndex < size; columnIndex++)
        {
          int[,] subMatrix = GetSubMatrix(matrix, size, rowIndex, columnIndex);
          int sign = ((rowIndex + columnIndex) % 2 == 0) ? 1 : -1;
          adjugateMatrix[columnIndex, rowIndex] = sign * CalculateDeterminant(subMatrix, size - 1);
        }
      }

      return adjugateMatrix;
    }

    private static int[,] GetSubMatrix(int[,] matrix, int size, int rowToRemove, int columnToRemove)
    {
      int[,] subMatrix = new int[size - 1, size - 1];
      int row = 0, column = 0;

      for (int rowIndex = 0; rowIndex < size; rowIndex++)
      {
        if (rowIndex == rowToRemove) continue;

        column = 0;
        for (int columnIndex = 0; columnIndex < size; columnIndex++)
        {
          if (columnIndex == columnToRemove) continue;
          subMatrix[row, column] = matrix[rowIndex, columnIndex];
          column++;
        }
        row++;
      }

      return subMatrix;
    }
  }
}