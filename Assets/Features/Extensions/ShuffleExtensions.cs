using System;

namespace Features.Extensions
{
  public static class ShuffleExtensions
  {
    private static readonly Random random = new Random();
    
    public static void Shuffle<T> (this T[] array)
    {
      int n = array.Length;
      T temp;
      int k;
      while (n > 1)
      {
        n--;
        
        k = random.Next(n);
        temp = array[n];
        array[n] = array[k];
        array[k] = temp;
      }
    }
  }
}