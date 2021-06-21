using System;
using System.Collections.Generic;
using System.Linq;

namespace Ozon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)) }) == 1);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 20, 0, 0)),
                (new DateTime(2021, 06, 19, 10, 0, 0), new DateTime(2021, 06, 19, 11, 0, 0)),
            }) == 2);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 13, 0, 0)),
            }) == 2);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0)),
            }) == 1);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 10, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0))
            }) == 3);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0)),
                (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 14, 0, 0)),
                (new DateTime(2021, 06, 19, 12, 0, 0), new DateTime(2021, 06, 19, 14, 0, 0)),
                (new DateTime(2021, 06, 19, 14, 0, 0), new DateTime(2021, 06, 19, 15, 0, 0)),
            }) == 4);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 20, 0, 0)),
                (new DateTime(2021, 06, 19, 10, 0, 0), new DateTime(2021, 06, 19, 20, 0, 0)),
                (new DateTime(2021, 06, 19, 19, 0, 0), new DateTime(2021, 06, 19, 20, 0, 0))
            }) == 3);
            Console.WriteLine(MaxCars(new[] { (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 10, 0, 0)),
                (new DateTime(2021, 06, 19, 9, 0, 0), new DateTime(2021, 06, 19, 11, 0, 0)),
                (new DateTime(2021, 06, 19, 10, 0, 0), new DateTime(2021, 06, 19, 11, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0)),
                (new DateTime(2021, 06, 19, 11, 0, 0), new DateTime(2021, 06, 19, 12, 0, 0)),
                (new DateTime(2021, 06, 19, 12, 0, 0), new DateTime(2021, 06, 19, 14, 0, 0)),
                (new DateTime(2021, 06, 19, 12, 0, 0), new DateTime(2021, 06, 19, 13, 0, 0)),
                                (new DateTime(2021, 06, 19, 12, 0, 0), new DateTime(2021, 06, 19, 12, 30, 0)),

                (new DateTime(2021, 06, 19, 13, 0, 0), new DateTime(2021, 06, 19, 14, 0, 0)),
                (new DateTime(2021, 06, 19, 14, 0, 0), new DateTime(2021, 06, 19, 15, 0, 0)),
            }) == 5);
        }

        static long MaxCars((DateTime start, DateTime end)[] times)
        {
            long res = 0;
            var ends = new PriorityQueue<DateTime>();
            foreach (var time in times.OrderBy(x => x.start).ThenBy(x => x.end))
            {
                while (ends.Count > 0 && time.start > ends.Peek())
                    ends.Dequeue();

                ends.Enqueue(time.end);
                res = Math.Max(res, ends.Count);
            }

            return res;
        }
        class PriorityQueue<T>  where T : IComparable
        {
            private List<T> list;
            public int Count { get { return list.Count; } }
            public readonly bool IsDescending;

            public PriorityQueue()
            {
                list = new List<T>();
            }

            public PriorityQueue(bool isdesc)
                : this()
            {
                IsDescending = isdesc;
            }

            public PriorityQueue(int capacity)
                : this(capacity, false)
            { }

            public PriorityQueue(IEnumerable<T> collection)
                : this(collection, false)
            { }

            public PriorityQueue(int capacity, bool isdesc)
            {
                list = new List<T>(capacity);
                IsDescending = isdesc;
            }

            public PriorityQueue(IEnumerable<T> collection, bool isdesc)
                : this()
            {
                IsDescending = isdesc;
                foreach (var item in collection)
                    Enqueue(item);
            }


            public void Enqueue(T x)
            {
                list.Add(x);
                int i = Count - 1;

                while (i > 0)
                {
                    int p = (i - 1) / 2;
                    if ((IsDescending ? -1 : 1) * list[p].CompareTo(x) <= 0) break;

                    list[i] = list[p];
                    i = p;
                }

                if (Count > 0) list[i] = x;
            }

            public T Dequeue()
            {
                T target = Peek();
                T root = list[Count - 1];
                list.RemoveAt(Count - 1);

                int i = 0;
                while (i * 2 + 1 < Count)
                {
                    int a = i * 2 + 1;
                    int b = i * 2 + 2;
                    int c = b < Count && (IsDescending ? -1 : 1) * list[b].CompareTo(list[a]) < 0 ? b : a;

                    if ((IsDescending ? -1 : 1) * list[c].CompareTo(root) >= 0) break;
                    list[i] = list[c];
                    i = c;
                }

                if (Count > 0) list[i] = root;
                return target;
            }

            public T Peek()
            {
                if (Count == 0) throw new InvalidOperationException("Queue is empty.");
                return list[0];
            }

            public void Clear()
            {
                list.Clear();
            }
        }
    }
}
