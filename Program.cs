using System;

namespace lab7
{
  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("Введите размер матриц:");
        int size;
        while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
        {
          Console.WriteLine("Некорректный ввод. Размер должен быть положительным числом. Попробуйте еще раз:");
        }

        SquareMatrix matrix1 = new SquareMatrix(size);
        SquareMatrix matrix2 = new SquareMatrix(size);

        Console.WriteLine("Матрица 1:\n" + matrix1);
        Console.WriteLine("Матрица 2:\n" + matrix2);

        var chain = SetupHandlers();

        while (true)
        {
          Console.WriteLine("\nВыберите операцию:");
          Console.WriteLine("1. Сложение матриц");
          Console.WriteLine("2. Вычитание матриц");
          Console.WriteLine("3. Умножение матриц");
          Console.WriteLine("4. Определитель матрицы 1");
          Console.WriteLine("5. Обратная матрица 1");
          Console.WriteLine("6. Клонирование матрицы 1");
          Console.WriteLine("7. Сравнение матриц");
          Console.WriteLine("8. След матрицы 1 (Trace)");
          Console.WriteLine("9. Транспонирование матрицы 1");
          Console.WriteLine("10. Диагонализация матрицы 1");
          Console.WriteLine("0. Выход");

          int choice;
          while (!int.TryParse(Console.ReadLine(), out choice))
          {
            Console.WriteLine("Некорректный ввод. Введите число от 0 до 10:");
          }

          if (choice == 0) break;
          if (choice < 0 || choice > 10)
          {
            Console.WriteLine("Неверный выбор операции. Попробуйте еще раз.");
            continue;
          }

          chain.Handle(matrix1, matrix2, choice);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Ошибка: " + ex.Message);
      }
    }

    private static MatrixOperationHandler SetupHandlers()
    {
      var addition = new AdditionHandler();
      var subtraction = new SubtractionHandler();
      var multiplication = new MultiplicationHandler();
      var determinant = new DeterminantHandler();
      var inverse = new InverseMatrixHandler();
      var clone = new CloneHandler();
      var compare = new CompareHandler();
      var trace = new TraceHandler();
      var transpose = new TransposeHandler();
      var diagonalize = new DiagonalizeHandler(MatrixOperations.DemoDiagonalization);

      addition.SetNext(subtraction)
             .SetNext(multiplication)
             .SetNext(determinant)
             .SetNext(inverse)
             .SetNext(clone)
             .SetNext(compare)
             .SetNext(trace)
             .SetNext(transpose)
             .SetNext(diagonalize);

      return addition;
    }
  }
}