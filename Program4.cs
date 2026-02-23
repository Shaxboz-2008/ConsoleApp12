using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp16
{

    //ЗАДАЧА 1
    public class Box<T>
    {
        private readonly T _content;
        public const string Purpose = "Storage";

        public Box(T content)
        {
            _content = content;
        }

        public T GetContentOrDefault(T defaultValue)
        {
            if (_content == null || _content.Equals(default(T)))
                return defaultValue;

            return _content;
        }

        public override string ToString()
        {
            return "Box: " + (_content?.ToString() ?? "empty");
        }
    }

    //ЗАДАЧА 2

    public class Shape
    {
        public readonly string Color;

        public Shape(string color)
        {
            Color = color;
        }

        public virtual double GetArea()
        {
            return 0;
        }
    }

    public class Circle : Shape
    {
        public readonly double Radius;

        public Circle(string color, double radius) : base(color)
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    public class Rectangle : Shape
    {
        public readonly double Width;
        public readonly double Height;

        public Rectangle(string color, double width, double height) : base(color)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    //ЗАДАЧА 3

    public abstract class Vehicle
    {
        public const int MaxSpeedLimit = 300;
        public readonly int Speed;

        public Vehicle(int speed)
        {
            Speed = speed > MaxSpeedLimit ? MaxSpeedLimit : speed;
        }

        public abstract string GetInfo();
    }

    public class Car : Vehicle
    {
        public string FuelType;

        public Car(int speed, string fuelType) : base(speed)
        {
            FuelType = fuelType;
        }

        public override string GetInfo()
        {
            return "Car, Speed: " + Speed;
        }
    }

    public class Bicycle : Vehicle
    {
        public bool HasGears;

        public Bicycle(int speed, bool hasGears) : base(speed)
        {
            HasGears = hasGears;
        }

        public override string GetInfo()
        {
            return "Bicycle, Speed: " + Speed;
        }
    }

    //ЗАДАЧА 4

    public class Worker
    {
        public readonly string Name;
        public readonly decimal BasePay;

        public Worker(string name, decimal basePay)
        {
            Name = name;
            BasePay = basePay;
        }

        public virtual decimal CalculatePay(int hours)
        {
            return BasePay * hours;
        }
    }

    public class Manager : Worker
    {
        public decimal Bonus;

        public Manager(string name, decimal basePay, decimal bonus)
            : base(name, basePay)
        {
            Bonus = bonus;
        }

        public override decimal CalculatePay(int hours)
        {
            return base.CalculatePay(hours) + Bonus;
        }
    }

    public class Intern : Worker
    {
        public Manager Mentor;

        public Intern(string name, decimal basePay, Manager mentor)
            : base(name, basePay)
        {
            Mentor = mentor;
        }

        public override decimal CalculatePay(int hours)
        {
            if (Mentor != null)
                return base.CalculatePay(hours) + 0.2m * Mentor.CalculatePay(hours);

            return base.CalculatePay(hours);
        }
    }

    //ЗАДАЧА 5

    public abstract class Storage<TKey, TValue>
    {
        public const int DefaultCapacity = 10;
        protected Dictionary<TKey, TValue> items = new Dictionary<TKey, TValue>();

        public abstract void Add(TKey key, TValue value);

        public virtual TValue Get(TKey key)
        {
            if (items.ContainsKey(key))
                return items[key];

            return default(TValue);
        }
    }

    public class StringKeyedStorage<TValue> : Storage<string, TValue>
    {
        public override void Add(string key, TValue value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("Invalid key");

            if (items.Count >= DefaultCapacity)
                throw new Exception("Storage full");

            items.Add(key, value);
        }
    }

    //ЗАДАЧА 6

    public abstract class Comparator<T>
    {
        public abstract int Compare(T x, T y);
    }

    public class IntComparator : Comparator<int>
    {
        public override int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }

    public class StringLengthComparator : Comparator<string>
    {
        public override int Compare(string x, string y)
        {
            return x.Length.CompareTo(y.Length);
        }
    }

    public class Pair<T>
    {
        public T First;
        public T Second;

        public Pair(T first, T second)
        {
            First = first;
            Second = second;
        }

        public T GetLarger(Comparator<T> comparator)
        {
            if (comparator.Compare(First, Second) >= 0)
                return First;
            else
                return Second;
        }
    }

    //ЗАДАЧА 7

    public abstract class Logger
    {
        public string Name;
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public Logger(string name)
        {
            Name = name;
        }

        public abstract void Log(string message);
    }

    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(string name) : base(name) { }

        public override void Log(string message)
        {
            Console.WriteLine(Name + ": " + message);
        }
    }

    public class FileLogger : Logger
    {
        private string fileName;

        public FileLogger(string name) : base(name)
        {
            fileName = name + ".txt";
        }

        public override void Log(string message)
        {
            File.AppendAllText(fileName, Name + ": " + message + "\n");
        }
    }

    public class TimestampLogger<T> : Logger where T : Logger
    {
        private T inner;

        public TimestampLogger(T logger) : base(logger.Name)
        {
            if (logger == null)
                throw new Exception("Logger is null");

            inner = logger;
        }

        public override void Log(string message)
        {
            string time = DateTime.Now.ToString(DateTimeFormat);
            inner.Log("[" + time + "] " + message);
        }
    }

    //ЗАДАЧА 8

    public abstract class Figure
    {
        public string Color;

        public Figure(string color)
        {
            Color = color;
        }

        public abstract double GetArea();
    }

    public class FigureCircle : Figure
    {
        public double Radius;

        public FigureCircle(string color, double radius) : base(color)
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    public class FigureRectangle : Figure
    {
        public double Width;
        public double Height;

        public FigureRectangle(string color, double width, double height)
            : base(color)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    public abstract class FigureComparator
    {
        public abstract int Compare(Figure a, Figure b);
    }

    public class AreaComparator : FigureComparator
    {
        public override int Compare(Figure a, Figure b)
        {
            return a.GetArea().CompareTo(b.GetArea());
        }
    }

    //ЗАДАЧА 9

    public class Stack<T> where T : class
    {
        public const int DefaultSize = 10;
        private T[] items;
        private int top = 0;

        public Stack(int size = DefaultSize)
        {
            items = new T[size];
        }

        public void Push(T item)
        {
            if (item == null)
                throw new Exception("Null item");

            if (top >= items.Length)
                throw new Exception("Stack full");

            items[top++] = item;
        }

        public T Pop()
        {
            if (top == 0)
                return null;

            return items[--top];
        }

        public T Peek()
        {
            if (top == 0)
                return null;

            return items[top - 1];
        }

        public override string ToString()
        {
            return string.Join(", ", items.Take(top));
        }
    }

    public class Person
    {
        public string Name;

        public Person(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    //ЗАДАЧА 10

    public class LibraryItem
    {
        public Guid Id;
        public string Title;
        public string Description;

        public LibraryItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public virtual string GetInfo()
        {
            return Title + " (" + Id + ")";
        }
    }

    public class Book : LibraryItem
    {
        public string Author;

        public Book(string title, string author) : base(title)
        {
            Author = author;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + " Author: " + Author;
        }
    }

    public class Magazine : LibraryItem
    {
        public int IssueNumber;

        public Magazine(string title, int number) : base(title)
        {
            IssueNumber = number;
        }
    }

    public abstract class Catalog<T> where T : LibraryItem
    {
        public const int MaxItems = 1000;
        protected Dictionary<Guid, T> items = new Dictionary<Guid, T>();

        public abstract void Add(T item);

        public virtual T FindById(Guid id)
        {
            if (items.ContainsKey(id))
                return items[id];

            return null;
        }
    }

    public class LibraryCatalog<T> : Catalog<T> where T : LibraryItem
    {
        public override void Add(T item)
        {
            if (items.ContainsKey(item.Id))
                throw new Exception("Duplicate ID");

            if (items.Count >= MaxItems)
                throw new Exception("Catalog full");

            items.Add(item.Id, item);
        }
    }

    public static class CatalogExtensions
    {
        public static Book[] FindBooksByAuthor(Catalog<Book> catalog, string author)
        {
            List<Book> result = new List<Book>();

            foreach (var item in catalog.GetType()
                .GetField("items", System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance)
                .GetValue(catalog) as Dictionary<Guid, Book>)
            {
                if (item.Value.Author == author)
                    result.Add(item.Value);
            }

            return result.ToArray();
        }
    }


    public class Program
    {
        public static void PrintVehicle<T>(T vehicle) where T : Vehicle
        {
            Console.WriteLine(vehicle.GetInfo());
        }

        public static T FindMax<T>(T[] items, FigureComparator comparator) where T : Figure
        {
            if (items.Length == 0)
                return null;

            T max = items[0];

            for (int i = 1; i < items.Length; i++)
            {
                if (comparator.Compare(items[i], max) > 0)
                    max = items[i];
            }

            return max;
        }

        public static void Main()
        {
            Console.WriteLine("===== ЗАДАЧА 1 =====");
            Box<int> intBox = new Box<int>(0);
            Console.WriteLine(intBox.GetContentOrDefault(100));

            Box<string> stringBox = new Box<string>(null);
            Console.WriteLine(stringBox.GetContentOrDefault("default"));
            Console.WriteLine(stringBox);


            Console.WriteLine("\n===== ЗАДАЧА 2 =====");
            Shape[] shapes =
            {
            new Circle("Red", 5),
            new Rectangle("Blue", 4, 6)
        };

            foreach (var s in shapes)
                Console.WriteLine(s.Color + " Area: " + s.GetArea());


            Console.WriteLine("\n===== ЗАДАЧА 3 =====");
            Car car = new Car(350, "Petrol");
            Bicycle bike = new Bicycle(25, true);

            PrintVehicle(car);
            PrintVehicle(bike);


            Console.WriteLine("\n===== ЗАДАЧА 4 =====");
            Manager manager = new Manager("Alex", 20, 500);
            Intern intern1 = new Intern("Tom", 10, manager);
            Intern intern2 = new Intern("Bob", 10, null);

            Worker[] workers = { manager, intern1, intern2 };

            foreach (var w in workers)
                Console.WriteLine(w.Name + " Salary: " + w.CalculatePay(160));


            Console.WriteLine("\n===== ЗАДАЧА 5 =====");
            StringKeyedStorage<int> storage = new StringKeyedStorage<int>();
            storage.Add("one", 1);
            Console.WriteLine(storage.Get("one"));

            try
            {
                storage.Add(null, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


            Console.WriteLine("\n===== ЗАДАЧА 6 =====");
            Pair<int> intPair = new Pair<int>(5, 10);
            Console.WriteLine("Larger int: " + intPair.GetLarger(new IntComparator()));

            Pair<string> strPair = new Pair<string>("cat", "elephant");
            Console.WriteLine("Longer string: " + strPair.GetLarger(new StringLengthComparator()));


            Console.WriteLine("\n===== ЗАДАЧА 7 =====");
            TimestampLogger<ConsoleLogger> logger =
                new TimestampLogger<ConsoleLogger>(new ConsoleLogger("MainLogger"));

            logger.Log("Program started");

            try
            {
                TimestampLogger<ConsoleLogger> badLogger =
                    new TimestampLogger<ConsoleLogger>(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logger error: " + ex.Message);
            }


            Console.WriteLine("\n===== ЗАДАЧА 8 =====");
            Figure[] figures =
            {
            new FigureCircle("Green", 3),
            new FigureRectangle("Yellow", 4, 5)
        };

            AreaComparator comp = new AreaComparator();
            Figure maxFigure = FindMax(figures, comp);

            Console.WriteLine("Max area: " + maxFigure.GetArea());


            Console.WriteLine("\n===== ЗАДАЧА 9 =====");
            Stack<string> stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");
            Console.WriteLine(stack);
            Console.WriteLine("Pop: " + stack.Pop());

            Stack<Person> peopleStack = new Stack<Person>();
            peopleStack.Push(new Person("John"));
            peopleStack.Push(new Person("Anna"));
            Console.WriteLine(peopleStack);


            Console.WriteLine("\n===== ЗАДАЧА 10 =====");
            LibraryCatalog<Book> catalog = new LibraryCatalog<Book>();

            Book b1 = new Book("C# Basics", "Smith");
            Book b2 = new Book("Advanced C#", "Smith");
            Book b3 = new Book("Java Intro", "Brown");

            catalog.Add(b1);
            catalog.Add(b2);
            catalog.Add(b3);

            Book[] smithBooks = CatalogExtensions.FindBooksByAuthor(catalog, "Smith");

            foreach (var b in smithBooks)
                Console.WriteLine(b.GetInfo());

            try
            {
                catalog.Add(b1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catalog error: " + ex.Message);
            }
        }
    }
}