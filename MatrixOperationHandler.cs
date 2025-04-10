using System;

namespace lab7
{
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