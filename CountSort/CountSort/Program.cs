using System;
using System.Diagnostics;

namespace CountSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var arrayToBeSortet = new int[1000000];
            for (int i = 0; i < arrayToBeSortet.Length; i++)
                arrayToBeSortet[i] = random.Next(0, 1000000);

            var timer = new Stopwatch();
            timer.Start();
            var arraySortedByComparisonCounting = ComparisonCountingSort(arrayToBeSortet);
            timer.Stop();
            Console.WriteLine("Koha per ComparisonCountingSort: " + timer.ElapsedMilliseconds + " miliseconds");

            timer.Reset();

            timer.Start();
            var arraySortedByDistributionCounting = DistributionCountingSort(arrayToBeSortet, 0, 1000000);
            timer.Stop();
            Console.WriteLine("Koha per DistributionCountingSort: " + timer.ElapsedMilliseconds + " miliseconds");
        }


        public static int[] ComparisonCountingSort(int[] a)
        {
            var s = new int[a.Length];
            var count = new int[a.Length];

            for (int i = 0; i < a.Length - 1; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] < a[j])
                        count[j] += 1;
                    else
                        count[i] += 1;
                }
            }

            for (int i = 0; i < a.Length; i++)
                s[count[i]] = a[i];

            return s;
        }

        public static int[] DistributionCountingSort(int[] a, int l, int u)
        {
            var s = new int[a.Length];
            var d = new int[u - l + 1];

            for (int i = 0; i < a.Length; i++)
                d[a[i] - l] = d[a[i] - l] + 1;

            for (int i = 1; i < (u - l + 1); i++)
                d[i] = d[i - 1] + d[i];

            for (int i = a.Length - 1; i >= 0; --i)
            {
                int j = a[i] - l;
                s[d[j] - 1] = a[i];
                d[j] = d[j] - 1;
            }

            return s;
        }
    }
}
