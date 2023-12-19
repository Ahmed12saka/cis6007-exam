using System;
using System.Threading.Tasks;

public class Program
{
    private static int[]? array;
    private static int N;
    private static int foundIndex = -1;
    private static readonly object lockObject = new object();

    static void Main(string[] args)
    {
        // Create a very large unsorted array of numbers.
        array = new int[10000000];
        Random random = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next();
        }

        // Set the value of N.
        N = 5000000;

        // Start the parallel algorithm.
        Parallel.For(0, 10, i =>
        {
            int startIndex = i * array.Length / 10;
            int endIndex = (i + 1) * array.Length / 10 - 1;

            for (int j = startIndex; j <= endIndex; j++)
            {
                if (array[j] > N)
                {
                    lock (lockObject)
                    {
                        if (foundIndex == -1)
                        {
                            foundIndex = j;
                            break;
                        }
                    }
                }
            }
        });

        // If the foundIndex is not -1, then a number bigger than N was found.
        if (foundIndex != -1)
        {
            Console.WriteLine("A number bigger than N was found at index {0}: {1}", foundIndex, array[foundIndex]);
        }
        else
        {
            Console.WriteLine("A number bigger than N was not found.");
        }
    }
}
