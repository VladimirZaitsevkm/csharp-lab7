using System;

namespace lab7
{
  public delegate SquareMatrix MatrixDiagonalizationDelegate(SquareMatrix matrix);

  public static class MatrixOperations
  {
    public static SquareMatrix DemoDiagonalization(SquareMatrix matrix)
    {
      SquareMatrix result = new SquareMatrix(matrix.Size);

      for (int i = 0; i < matrix.Size; i++)
      {
        for (int j = 0; j < matrix.Size; j++)
        {
          result._matrix[i, j] = (i == j) ? matrix._matrix[i, j] : 0;
        }
      }

      return result;
    }
  }

  public abstract class MatrixOperationHandler
  {
    protected MatrixOperationHandler NextHandler { get; private set; }

    public MatrixOperationHandler SetNext(MatrixOperationHandler handler)
    {
      NextHandler = handler;
      return handler;
    }

    public virtual void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (NextHandler != null)
        NextHandler.Handle(matrix1, matrix2, choice);
      else
        Console.WriteLine("Операция не найдена.");
    }
  }

  public class AdditionHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 1)
      {
        SquareMatrix sum = matrix1 + matrix2;
        Console.WriteLine("Результат сложения:\n" + sum);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class SubtractionHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 2)
      {
        SquareMatrix diff = matrix1 - matrix2;
        Console.WriteLine("Результат вычитания:\n" + diff);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class MultiplicationHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 3)
      {
        SquareMatrix product = matrix1 * matrix2;
        Console.WriteLine("Результат умножения:\n" + product);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class DeterminantHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 4)
      {
        int determinant = !matrix1;
        Console.WriteLine("Определитель матрицы 1: " + determinant);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class InverseMatrixHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 5)
      {
        try
        {
          double[,] inverse = ~matrix1;
          Console.WriteLine("Обратная матрица 1:");
          for (int i = 0; i < matrix1.Size; i++)
          {
            for (int j = 0; j < matrix1.Size; j++)
            {
              Console.Write(inverse[i, j].ToString("F2") + "\t");
            }
            Console.WriteLine();
          }
        }
        catch (MatrixDeterminantZeroException ex)
        {
          Console.WriteLine("Ошибка: " + ex.Message);
        }
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class CloneHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 6)
      {
        SquareMatrix clone = (SquareMatrix)matrix1.Clone();
        Console.WriteLine("Клон матрицы 1:\n" + clone);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class CompareHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 7)
      {
        if (matrix1 == matrix2)
          Console.WriteLine("Матрицы равны");
        else if (matrix1 > matrix2)
          Console.WriteLine("Матрица 1 больше матрицы 2");
        else
          Console.WriteLine("Матрица 2 больше матрицы 1");
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class TraceHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 8)
      {
        int trace = matrix1.Trace();
        Console.WriteLine("След матрицы 1 (Trace): " + trace);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class TransposeHandler : MatrixOperationHandler
  {
    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 9)
      {
        SquareMatrix transposed = matrix1.Transpose();
        Console.WriteLine("Транспонированная матрица:\n" + transposed);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }

  public class DiagonalizeHandler : MatrixOperationHandler
  {
    private MatrixDiagonalizationDelegate _diagonalize;

    public DiagonalizeHandler(MatrixDiagonalizationDelegate diagonalize)
    {
      _diagonalize = diagonalize;
    }

    public override void Handle(SquareMatrix matrix1, SquareMatrix matrix2, int choice)
    {
      if (choice == 10)
      {
        SquareMatrix diagonalized = _diagonalize(matrix1);
        Console.WriteLine("Диагонализированная матрица:\n" + diagonalized);
      }
      else
        base.Handle(matrix1, matrix2, choice);
    }
  }
}