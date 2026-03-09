using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        // 1.
        static IEnumerable<int> GetEvenNumbers(int max)
        {
            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                    yield return i;
            }
        }

        // 2.
        static IEnumerable<string> FilterByLength(IEnumerable<string> source, int minLength)
        {
            foreach (string s in source)
            {
                if (s.Length >= minLength)
                    yield return s;
            }
        }

        // 3.
        static IEnumerable<int> PowerOfTwo()
        {
            int n = 2;

            while (true)
            {
                yield return n;
                n = n * 2;
            }
        }

        // 4.
        static IEnumerable<T> ReverseIterator<T>(List<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }

        static void Main()
        {
            // 1
            Console.WriteLine("Четные числа:");

            foreach (int n in GetEvenNumbers(20))
            {
                Console.Write(n + " ");
            }

            Console.WriteLine();


            // 2
            Console.WriteLine("\nФильтр по длине:");

            List<string> names = new List<string>()
        {
            "Alex",
            "Bob",
            "Alexander",
            "Tom"
        };

            foreach (string name in FilterByLength(names, 4))
            {
                Console.WriteLine(name);
            }


            // 3
            Console.WriteLine("\nСтепени двойки:");

            foreach (int n in PowerOfTwo())
            {
                if (n > 1000)
                    break;

                Console.WriteLine(n);
            }


            // 4
            Console.WriteLine("\nРеверс списка:");

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };

            foreach (int n in ReverseIterator(numbers))
            {
                Console.WriteLine(n);
            }


            // 5
            Console.WriteLine("\nПодсчет слов:");

            string text = "cat dog cat bird dog cat";

            string[] words = text.Split(' ');

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (dict.ContainsKey(word))
                    dict[word]++;
                else
                    dict[word] = 1;
            }

            foreach (var pair in dict)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }
        }
    }
}
