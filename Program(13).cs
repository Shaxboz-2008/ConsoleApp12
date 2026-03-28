using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace ConsoleApp5
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Умный кошелек ===");

            Wallet wallet = new Wallet(1000);

            wallet.OnBudgetExceeded += (category, sum) =>
            {
                Console.WriteLine($"Превышен бюджет в категории {category}: {sum}");
            };

            wallet.Add("Food", new Transaction(500));
            wallet.Add("Food", new Transaction(700));

            Console.WriteLine();


            Console.WriteLine("=== RPG ===");

            Character hero = new Character(new Stats(10, 5, 3));
            Item sword = new Item(new Stats(5, 0, 0));

            hero += sword;
            Console.WriteLine($"Сила героя: {hero.Stats.Str}");

            Console.WriteLine();


            Console.WriteLine("=== Склад ===");

            Warehouse warehouse = new Warehouse();

            warehouse.LowStockWarning += p =>
            {
                Console.WriteLine($"Мало товара: {p.Name}");
            };

            warehouse.Add(new ProductBatch("Apple", 10, 100));
            warehouse.Add(new ProductBatch("Apple", 20, 50));

            warehouse.Remove("Apple", 140);

            Console.WriteLine();


            Console.WriteLine("=== Трафик ===");

            TrafficMonitor monitor = new TrafficMonitor();

            monitor.ThreatDetected += p =>
            {
                Console.WriteLine("Найдена угроза!");
            };

            monitor.Process(new Packet("bad"));

            Console.WriteLine();


            Console.WriteLine("=== Планировщик ===");

            TimeSlot t1 = new TimeSlot(10, 12);
            TimeSlot t2 = new TimeSlot(11, 13);

            if (t1 & t2)
                Console.WriteLine("Есть пересечение");

            Console.WriteLine();


            Console.WriteLine("=== Файлы ===");

            Directory dir = new Directory("Root");
            dir.AddFile("file1.txt");

            var found = dir.Search(f => f.Contains("file1"));
            foreach (var f in found)
                Console.WriteLine(f);

            Console.WriteLine();


            Console.WriteLine("=== Pipeline ===");

            Pipeline pipe = new Pipeline();

            pipe.AddStep(p => { p.Data += "_log"; return p; });
            pipe.AddStep(p => { p.Data += "_ok"; return p; });

            var result = pipe.Run(new Packet("data"));
            Console.WriteLine(result.Data);
        }
    }

    class Money
    {
        public decimal Amount;
        public string Currency;

        public Money(decimal a, string c)
        {
            Amount = a;
            Currency = c;
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                b = new Money(b.Amount * 100, a.Currency);

            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static bool operator >(Money a, Money b) => a.Amount > b.Amount;
        public static bool operator <(Money a, Money b) => a.Amount < b.Amount;
    }

    class Transaction
    {
        public decimal Amount;
        public Transaction(decimal a) { Amount = a; }
    }

    class Wallet
    {
        public delegate void BudgetHandler(string category, decimal sum);
        public event BudgetHandler OnBudgetExceeded;

        private Dictionary<string, List<Transaction>> data = new();

        private decimal limit;

        public Wallet(decimal limit)
        {
            this.limit = limit;
        }

        public void Add(string category, Transaction t)
        {
            if (!data.ContainsKey(category))
                data[category] = new List<Transaction>();

            data[category].Add(t);

            decimal sum = data[category].Sum(x => x.Amount);

            if (sum > limit)
                OnBudgetExceeded?.Invoke(category, sum);
        }
    }

    class Stats
    {
        public int Str, Agi, Int;

        public Stats(int s, int a, int i)
        {
            Str = s; Agi = a; Int = i;
        }

        public static Stats operator +(Stats a, Stats b)
        {
            return new Stats(a.Str + b.Str, a.Agi + b.Agi, a.Int + b.Int);
        }
    }

    class Character
    {
        public Stats Stats;

        public Character(Stats s)
        {
            Stats = s;
        }

        public static Character operator +(Character c, Item i)
        {
            c.Stats += i.Stats;
            return c;
        }
    }

    class Item
    {
        public Stats Stats;
        public Item(Stats s) { Stats = s; }
    }

    class ProductBatch
    {
        public string Name;
        public decimal Price;
        public int Quantity;

        public ProductBatch(string n, decimal p, int q)
        {
            Name = n;
            Price = p;
            Quantity = q;
        }

        public static ProductBatch operator +(ProductBatch a, ProductBatch b)
        {
            int total = a.Quantity + b.Quantity;
            decimal avg = (a.Price * a.Quantity + b.Price * b.Quantity) / total;

            return new ProductBatch(a.Name, avg, total);
        }
    }

    class Warehouse
    {
        public event Action<ProductBatch> LowStockWarning;

        private Dictionary<string, ProductBatch> items = new();

        public void Add(ProductBatch p)
        {
            if (items.ContainsKey(p.Name))
                items[p.Name] += p;
            else
                items[p.Name] = p;
        }

        public void Remove(string name, int count)
        {
            var p = items[name];
            p.Quantity -= count;

            if (p.Quantity < 5)
                LowStockWarning?.Invoke(p);
        }
    }


    class Packet
    {
        public string Data;

        public Packet(string d)
        {
            Data = d;
        }

        public static bool operator ==(Packet a, Packet b)
        {
            return a.Data == b.Data;
        }

        public static bool operator !=(Packet a, Packet b)
        {
            return a.Data != b.Data;
        }

        public override bool Equals(object obj) => false;
        public override int GetHashCode() => 0;
    }

    class TrafficMonitor
    {
        public event Action<Packet> ThreatDetected;

        public void Process(Packet p)
        {
            if (p.Data.Contains("bad"))
                ThreatDetected?.Invoke(p);
        }
    }


    class TimeSlot
    {
        public int Start, End;

        public TimeSlot(int s, int e)
        {
            Start = s; End = e;
        }

        public static bool operator &(TimeSlot a, TimeSlot b)
        {
            return a.Start < b.End && b.Start < a.End;
        }
    }


    class Directory
    {
        public string Name;
        private List<string> files = new();

        public Directory(string name)
        {
            Name = name;
        }

        public void AddFile(string file)
        {
            files.Add(file);
        }

        public List<string> Search(Predicate<string> p)
        {
            return files.FindAll(p);
        }
    }


    class Pipeline
    {
        private List<Func<Packet, Packet>> steps = new();

        public void AddStep(Func<Packet, Packet> step)
        {
            steps.Add(step);
        }

        public Packet Run(Packet p)
        {
            foreach (var step in steps)
                p = step(p);

            return p;
        }
    }
}
