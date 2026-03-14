using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {
        // 1
        static HashSet<int> CleanData(List<HashSet<int>> sources, HashSet<int> blacklist)
        {
            HashSet<int> result = new HashSet<int>(sources[0]);

            foreach (var set in sources)
            {
                result.IntersectWith(set);
            }

            result.ExceptWith(blacklist);

            return result;
        }


        // 2
        static void CompareVersions(HashSet<string> versionA, HashSet<string> versionB,
                                    out HashSet<string> added, out HashSet<string> removed)
        {
            added = new HashSet<string>(versionB);
            added.ExceptWith(versionA);

            removed = new HashSet<string>(versionA);
            removed.ExceptWith(versionB);
        }


        static void Main()
        {
            // ЗАДАНИЕ 1
            Console.WriteLine("Задание 1:");

            var s1 = new HashSet<int> { 1, 2, 3, 4 };
            var s2 = new HashSet<int> { 2, 3, 5 };
            var s3 = new HashSet<int> { 2, 3, 6 };

            var blacklist = new HashSet<int> { 3 };

            var sources = new List<HashSet<int>> { s1, s2, s3 };

            var clean = CleanData(sources, blacklist);

            foreach (var x in clean)
                Console.WriteLine(x);



            // ЗАДАНИЕ 2
            Console.WriteLine("\nЗадание 2:");

            var versionA = new HashSet<string> { "Login", "Search", "Profile" };
            var versionB = new HashSet<string> { "Login", "Search", "Chat", "ProfileEdit" };

            CompareVersions(versionA, versionB, out var added, out var removed);

            Console.WriteLine("Добавлены:");
            foreach (var f in added)
                Console.WriteLine(f);

            Console.WriteLine("Удалены:");
            foreach (var f in removed)
                Console.WriteLine(f);



            // ЗАДАНИЕ 3
            Console.WriteLine("\nЗадание 3:");

            var user1 = new HashSet<string> { "music", "games", "sport" };
            var user2 = new HashSet<string> { "games", "movies", "travel" };

            var unique = new HashSet<string>(user1);

            unique.SymmetricExceptWith(user2);

            Console.WriteLine("Уникальные интересы:");

            foreach (var tag in unique)
                Console.WriteLine(tag);
        }
    }
}
