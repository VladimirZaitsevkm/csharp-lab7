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
        int size = int.Parse(Console.ReadLine());

        SquareMatrix matrix1 = new SquareMatrix(size);
        SquareMatrix matrix2 = new SquareMatrix(size);

        Console.WriteLine("Матрица 1:\n" + matrix1);
        Console.WriteLine("Матрица 2:\n" + matrix2);

        // Инициализация цепочки обработчиков
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

          int choice = int.Parse(Console.ReadLine());
          if (choice == 0) break;

          // Запуск обработки через цепочку
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
      // Создаем обработчики
      var addition = new AdditionHandler();
      var subtraction = new SubtractionHandler();
      var transpose = new TransposeHandler();
      var diagonalize = new DiagonalizeHandler(MatrixOperations.DemoDiagonalization);

      // Строим цепочку
      addition.SetNext(subtraction)
             .SetNext(transpose)
             .SetNext(diagonalize);

      return addition;
    }
  }
}