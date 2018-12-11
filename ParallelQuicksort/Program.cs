using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelQuicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = GetUnsortedList();
            //List<int> list = new List<int>(new int[] { 3, 10, 1, 5, 2, 11, 4 });
            list = Quicksort(list);
            Console.ReadLine();
        }

        static List<int> GetUnsortedList()
        {
            List<int> list = new List<int>();
            Random randNum = new Random();
            for (int i = 0; i < 10; i++)
            {
                list.Add(randNum.Next(0, 100));
            }
            return list;
        }

        static List<int> Quicksort(List<int> unsortedList)
        {
            int pivot = unsortedList[unsortedList.Count - 1];
            unsortedList.RemoveAt(unsortedList.Count - 1);
            int a = 0;
            int b = unsortedList.Count() - 1;

            while (true)
            {
                while (unsortedList[Math.Min(a, unsortedList.Count - 1)] < pivot)
                {
                    if (a >= unsortedList.Count())
                    {
                        break;
                    }
                    a++;
                }
                while (unsortedList[b] > pivot)
                {
                    if (b <= 0)
                    {

                        break;
                    }
                    b--;
                }
                if (a < b)
                {
                    unsortedList = SwapValues(unsortedList, a, b);
                }
                else
                {
                    break;
                }
            }
            List<int> partition1 = new List<int>();
            List<int> partition2 = new List<int>();
            partition1.AddRange(unsortedList.GetRange(0, a));
            partition2.AddRange(unsortedList.GetRange(Math.Min(a, unsortedList.Count), unsortedList.Count - a));

            Console.WriteLine(string.Join(",", partition1.ToArray()));
            Console.WriteLine(string.Join(",", partition2.ToArray()));
            Console.WriteLine("pivot:");
            Console.WriteLine(pivot);
            var thread1 = new Thread(
                () =>
                {
                    if (partition1.Count > 1)
                    {
                        partition1 = Quicksort(partition1);
                    }
                });
            var thread2 = new Thread(
                () =>
                {
                    if (partition2.Count > 1)
                    {
                        partition2 = Quicksort(partition2);
                    }
                });
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            partition1.Add(pivot);
            partition1.AddRange(partition2);
            Console.WriteLine(string.Join(",", partition1.ToArray()));
            return partition1;
        }

        static List<int> SwapValues(List<int> list, int a, int b)
        {
            int c = list[a];
            list[a] = list[b];
            list[b] = c;
            return list;
        }
    }
}
