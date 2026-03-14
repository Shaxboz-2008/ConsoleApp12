using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Smart Wallet ===");

            Money m1 = new Money(100, "USD");
            Money m2 = new Money(100, "EUR");

            Money result = m1 + m2;

            Console.WriteLine(result.Amount + " " + result.Currency);


            Console.WriteLine("\n=== RPG Combat ===");

            Stats hero = new Stats(10, 5, 2);
            Stats sword = new Stats(5, 0, 0);

            Stats newStats = hero + sword;

            Console.WriteLine("Сила: " + newStats.Strength);


            Console.WriteLine("\n=== Warehouse ===");

            ProductBatch b1 = new ProductBatch("Phone", 10, 500);
            ProductBatch b2 = new ProductBatch("Phone", 20, 600);

            ProductBatch merged = b1 + b2;

            Console.WriteLine("Цена: " + merged.Price);


            Console.WriteLine("\n=== Traffic Monitor ===");

            Packet p1 = new Packet("abc");
            Packet p2 = new Packet("abc");

            Console.WriteLine(p1 == p2);


            Console.WriteLine("\n=== Scheduler ===");

            TimeSlot t1 = new TimeSlot(DateTime.Now, DateTime.Now.AddHours(2));
            TimeSlot t2 = new TimeSlot(DateTime.Now.AddHours(1), DateTime.Now.AddHours(3));

            Console.WriteLine(t1 & t2);


            Console.WriteLine("\n=== VFS ===");

            Directory d1 = new Directory("A");
            d1.Files.Add(new File("a.txt", 100));

            Directory d2 = new Directory("B");
            d2.Files.Add(new File("b.txt", 200));

            Directory mergedDir = d1 + d2;

            Console.WriteLine(mergedDir.Files.Count);


            Console.WriteLine("\n=== Pipeline ===");

            PipelineEngine engine = new PipelineEngine();

            engine.Process(new Packet("data"));


            Console.WriteLine("\n=== RPG Stats ===");

            Stat hp = new Stat(100);

            hp.OnChanged += s => Console.WriteLine("HP changed: " + s.Value);

            hp.Add(new Modifier(20));


            Console.WriteLine("\n=== Trading ===");

            Price price = new Price(100);

            Console.WriteLine(price % 2);


            Console.WriteLine("\n=== Notifications ===");

            NotificationDispatcher dispatcher = new NotificationDispatcher();

            dispatcher.Send("Hello");
        }
    }

    // 1

    class Money
    {
        public double Amount;
        public string Currency;

        public Money(double amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        static double Convert(double amount, string from, string to)
        {
            if (from == to) return amount;

            if (from == "EUR" && to == "USD") return amount * 1.1;
            if (from == "USD" && to == "EUR") return amount * 0.9;

            return amount;
        }

        public static Money operator +(Money a, Money b)
        {
            double converted = Convert(b.Amount, b.Currency, a.Currency);
            return new Money(a.Amount + converted, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            double converted = Convert(b.Amount, b.Currency, a.Currency);
            return new Money(a.Amount - converted, a.Currency);
        }

        public static bool operator >(Money a, Money b)
        {
            return a.Amount > Convert(b.Amount, b.Currency, a.Currency);
        }

        public static bool operator <(Money a, Money b)
        {
            return a.Amount < Convert(b.Amount, b.Currency, a.Currency);
        }
    }

    // 2

    class Stats
    {
        public int Strength;
        public int Agility;
        public int Intelligence;

        public Stats(int s, int a, int i)
        {
            Strength = s;
            Agility = a;
            Intelligence = i;
        }

        public static Stats operator +(Stats a, Stats b)
        {
            return new Stats(
                a.Strength + b.Strength,
                a.Agility + b.Agility,
                a.Intelligence + b.Intelligence
            );
        }
    }

    // 3

    class ProductBatch
    {
        public string Name;
        public int Quantity;
        public double Price;

        public ProductBatch(string n, int q, double p)
        {
            Name = n;
            Quantity = q;
            Price = p;
        }

        public static ProductBatch operator +(ProductBatch a, ProductBatch b)
        {
            int total = a.Quantity + b.Quantity;

            double avg = ((a.Price * a.Quantity) +
                          (b.Price * b.Quantity)) / total;

            return new ProductBatch(a.Name, total, avg);
        }
    }

    // 4

    class Packet
    {
        public string Payload;

        public Packet(string p)
        {
            Payload = p;
        }

        public static bool operator ==(Packet a, Packet b)
        {
            return a.Payload == b.Payload;
        }

        public static bool operator !=(Packet a, Packet b)
        {
            return a.Payload != b.Payload;
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => Payload.GetHashCode();
    }

    // 5

    class TimeSlot
    {
        public DateTime Start;
        public DateTime End;

        public TimeSlot(DateTime s, DateTime e)
        {
            Start = s;
            End = e;
        }

        public static bool operator &(TimeSlot a, TimeSlot b)
        {
            return a.Start < b.End && b.Start < a.End;
        }
    }

    // 6

    class File
    {
        public string Name;
        public int Size;

        public File(string n, int s)
        {
            Name = n;
            Size = s;
        }
    }

    class Directory
    {
        public string Name;

        public List<File> Files = new List<File>();

        public Dictionary<string, Directory> SubDirs =
            new Dictionary<string, Directory>();

        public Directory(string name)
        {
            Name = name;
        }

        public static Directory operator +(Directory a, Directory b)
        {
            Directory d = new Directory("Merged");

            d.Files.AddRange(a.Files);
            d.Files.AddRange(b.Files);

            return d;
        }

        public static bool operator >(Directory a, Directory b)
        {
            return a.Files.Sum(f => f.Size) >
                   b.Files.Sum(f => f.Size);
        }

        public static bool operator <(Directory a, Directory b)
        {
            return a.Files.Sum(f => f.Size) <
                   b.Files.Sum(f => f.Size);
        }
    }

    // 7

    class PipelineEngine
    {
        List<Func<Packet, Packet>> steps =
            new List<Func<Packet, Packet>>();

        public PipelineEngine()
        {
            steps.Add(p => p);
            steps.Add(p => p);
        }

        public void Process(Packet p)
        {
            foreach (var step in steps)
            {
                p = step(p);
            }
        }
    }

    // 8

    class Stat
    {
        public int Value;

        public event Action<Stat> OnChanged;

        public Stat(int v)
        {
            Value = v;
        }

        public void Add(Modifier m)
        {
            Value += m.Value;
            OnChanged?.Invoke(this);
        }
    }

    class Modifier
    {
        public int Value;

        public Modifier(int v)
        {
            Value = v;
        }
    }

    // 9

    class Price
    {
        public double Value;

        public Price(double v)
        {
            Value = v;
        }

        public static double operator %(Price p, double fee)
        {
            return p.Value * fee / 100;
        }
    }

    // 10

    class NotificationDispatcher
    {
        public event Action OnAllDeliveriesCompleted;

        List<string> users = new List<string>
    {
        "Admin",
        "User",
        "Guest"
    };

        public void Send(string msg)
        {
            foreach (var u in users)
            {
                Console.WriteLine($"Send to {u}: {msg}");
            }

            OnAllDeliveriesCompleted?.Invoke();
        }
    }
}
