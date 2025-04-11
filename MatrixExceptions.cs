using System;

namespace lab7
{
  public class MatrixSizeException : Exception
  {
    public MatrixSizeException(string message) : base(message) { }
  }

  public class MatrixDeterminantZeroException : Exception
  {
    public MatrixDeterminantZeroException(string message) : base(message) { }
  }
}