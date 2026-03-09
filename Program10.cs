using System;
using System.Collections.Generic;

namespace ConsoleApp2
{

    // Задача 1
    class SmartStack<T>
    {
        private List<T> items = new List<T>();
        private int maxSize;

        public SmartStack(int size)
        {
            maxSize = size;
        }

        public void Push(T value)
        {
            if (items.Count >= maxSize)
                throw new InvalidOperationException("Stack overflow");

            items.Add(value);
        }

        public IEnumerable<T> GetReverse()
        {
            for (int i = items.Count - 1; i >= 0; i--)
                yield return items[i];
        }

        public static SmartStack<T> operator +(SmartStack<T> a, SmartStack<T> b)
        {
            SmartStack<T> result = new SmartStack<T>(a.maxSize);

            if (a.items.Count + b.items.Count > a.maxSize)
                throw new InvalidOperationException("Limit exceeded");

            foreach (var x in a.items)
                result.items.Add(x);

            foreach (var x in b.items)
                result.items.Add(x);

            return result;
        }
    }

    // Задача 2
    class Polynomial
    {
        public List<int> coef = new List<int>();

        public Polynomial(List<int> c)
        {
            coef = c;
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            List<int> res = new List<int>();
            int max = Math.Max(a.coef.Count, b.coef.Count);

            for (int i = 0; i < max; i++)
            {
                int x = i < a.coef.Count ? a.coef[i] : 0;
                int y = i < b.coef.Count ? b.coef[i] : 0;

                res.Add(x + y);
            }

            return new Polynomial(res);
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            List<int> res = new List<int>();
            int max = Math.Max(a.coef.Count, b.coef.Count);

            for (int i = 0; i < max; i++)
            {
                int x = i < a.coef.Count ? a.coef[i] : 0;
                int y = i < b.coef.Count ? b.coef[i] : 0;

                res.Add(x - y);
            }

            return new Polynomial(res);
        }

        public IEnumerable<(int coef, int power)> GetNonZeroCoefficients()
        {
            for (int i = 0; i < coef.Count; i++)
                if (coef[i] != 0)
                    yield return (coef[i], i);
        }

        public static Polynomial operator *(Polynomial p, int n)
        {
            List<int> res = new List<int>();

            foreach (var x in p.coef)
                res.Add(x * n);

            return new Polynomial(res);
        }
    }

    // Задача 3

    class SafeDictionary<TKey, TValue>
    {
        public Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        public void Add(TKey key, TValue value)
        {
            dict[key] = value;
        }

        public static SafeDictionary<TKey, TValue> operator -(SafeDictionary<TKey, TValue> a, SafeDictionary<TKey, TValue> b)
        {
            SafeDictionary<TKey, TValue> result = new SafeDictionary<TKey, TValue>();

            foreach (var x in a.dict)
            {
                if (!b.dict.ContainsKey(x.Key))
                    result.dict[x.Key] = x.Value;
            }

            return result;
        }

        public IEnumerable<TKey> GetSortedKeys()
        {
            List<TKey> keys = new List<TKey>(dict.Keys);
            keys.Sort();

            foreach (var k in keys)
                yield return k;
        }

        public static bool operator ==(SafeDictionary<TKey, TValue> a, SafeDictionary<TKey, TValue> b)
        {
            if (a.dict.Count != b.dict.Count)
                return false;

            foreach (var x in a.dict)
            {
                if (!b.dict.ContainsKey(x.Key))
                    return false;

                if (!x.Value.Equals(b.dict[x.Key]))
                    return false;
            }

            return true;
        }

        public static bool operator !=(SafeDictionary<TKey, TValue> a, SafeDictionary<TKey, TValue> b)
        {
            return !(a == b);
        }
    }

    // Задача 4

    class Folder
    {
        public string Name;
        public List<string> files = new List<string>();
        public List<Folder> folders = new List<Folder>();

        public Folder(string name)
        {
            Name = name;
        }

        public IEnumerable<string> GetAllFiles()
        {
            foreach (var f in files)
                yield return f;

            foreach (var folder in folders)
                foreach (var f in folder.GetAllFiles())
                    yield return f;
        }

        public static Folder operator +(Folder f, string file)
        {
            f.files.Add(file);
            return f;
        }

        public static Folder operator /(Folder f, string name)
        {
            foreach (var folder in f.folders)
                if (folder.Name == name)
                    return folder;

            Folder newFolder = new Folder(name);
            f.folders.Add(newFolder);
            return newFolder;
        }
    }

    // Задача 5

    class Tensor2D
    {
        Dictionary<(int, int), double> data = new Dictionary<(int, int), double>();

        public double this[int r, int c]
        {
            get
            {
                if (data.ContainsKey((r, c)))
                    return data[(r, c)];
                return 0;
            }
            set
            {
                if (value != 0)
                    data[(r, c)] = value;
            }
        }

        public static Tensor2D operator *(Tensor2D a, Tensor2D b)
        {
            Tensor2D result = new Tensor2D();

            foreach (var x in a.data)
            {
                var pos = x.Key;
                double val = x.Value;

                foreach (var y in b.data)
                {
                    if (pos.Item2 == y.Key.Item1)
                    {
                        int r = pos.Item1;
                        int c = y.Key.Item2;

                        result[r, c] += val * y.Value;
                    }
                }
            }

            return result;
        }

        public IEnumerable<(int, int, double)> GetSpiral()
        {
            foreach (var x in data)
                yield return (x.Key.Item1, x.Key.Item2, x.Value);
        }

        public static bool operator true(Tensor2D t)
        {
            return t.data.Count > 0;
        }

        public static bool operator false(Tensor2D t)
        {
            return t.data.Count == 0;
        }
    }


    class Program
    {
        static void Main()
        {
            // 1
            SmartStack<int> s = new SmartStack<int>(10);
            s.Push(1);
            s.Push(2);
            s.Push(3);

            foreach (var x in s.GetReverse())
                Console.WriteLine(x);

            // 2
            Polynomial p = new Polynomial(new List<int> { 1, 0, 3 });

            foreach (var x in p.GetNonZeroCoefficients())
                Console.WriteLine($"coef={x.coef} power={x.power}");

            // 3
            SafeDictionary<int, string> d = new SafeDictionary<int, string>();
            d.Add(2, "A");
            d.Add(1, "B");

            foreach (var k in d.GetSortedKeys())
                Console.WriteLine(k);

            // 4
            Folder root = new Folder("root");
            root += "file1.txt";

            Folder sub = root / "docs";
            sub += "file2.txt";

            foreach (var f in root.GetAllFiles())
                Console.WriteLine(f);

            // 5
            Tensor2D t = new Tensor2D();
            t[0, 0] = 5;

            if (t)
                Console.WriteLine("Tensor не пустой");
        }
    }
}
