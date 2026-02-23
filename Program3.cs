using System;
using System.Collections.Generic;

namespace ConsoleApp15
{
    // 1.1
    class Wrapper<T>
    {
        private readonly T _value;
        public const string TypeName = "Wrapper";

        public Wrapper(T value)
        {
            _value = value;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            if (_value == null || _value.Equals(default(T)))
                return defaultValue;

            return _value;
        }

        public override string ToString()
        {
            return $"{TypeName}: {_value?.ToString() ?? "null"}";
        }
    }

    // 1.2
    class Entity
    {
        public readonly Guid Id;

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public virtual string Description
        {
            get { return $"Entity {Id}"; }
        }
    }

    class NamedEntity : Entity
    {
        public readonly string Name;

        public NamedEntity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            Name = name;
        }

        public override string Description
        {
            get
            {
                if (Name == null)
                    return base.Description;

                return $"Named entity \"{Name}\" (Id: {Id})";
            }
        }
    }

    // 1.3
    abstract class Shape
    {
        public const double Pi = 3.1415926535;
        public readonly string Color;

        public Shape(string color)
        {
            Color = color;
        }

        public abstract double GetArea();
    }

    class Circle : Shape
    {
        public readonly double Radius;

        public Circle(string color, double radius) : base(color)
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Pi * Radius * Radius;
        }
    }

    class Rectangle : Shape
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

    static class ShapeHelper
    {
        public static void PrintInfo<T>(T shape) where T : Shape
        {
            Console.WriteLine($"Color: {shape.Color}, Area: {shape.GetArea()}");
        }
    }

    // 1.4
    class Employee
    {
        public readonly string FullName;
        public readonly decimal BaseSalary;

        public Employee(string name, decimal salary)
        {
            FullName = name;
            BaseSalary = salary;
        }

        public virtual decimal CalculateSalary()
        {
            return BaseSalary;
        }
    }

    class Manager : Employee
    {
        public readonly decimal Bonus;

        public Manager(string name, decimal salary, decimal bonus)
            : base(name, salary)
        {
            Bonus = bonus;
        }

        public override decimal CalculateSalary()
        {
            return BaseSalary + Bonus;
        }
    }

    class Intern : Employee
    {
        public Manager Mentor;

        public Intern(string name, decimal salary, Manager mentor)
            : base(name, salary)
        {
            Mentor = mentor;
        }

        public override decimal CalculateSalary()
        {
            if (Mentor != null)
                return BaseSalary + 0.1m * Mentor.CalculateSalary();

            return BaseSalary;
        }
    }

    // 1.5
    abstract class Message
    {
        public const string DefaultAuthor = "System";
        public readonly string Text;

        public Message(string text)
        {
            Text = text;
        }

        public abstract string GetFormattedText();
    }

    class EmailMessage : Message
    {
        public readonly string Recipient;

        public EmailMessage(string text, string recipient)
            : base(text)
        {
            Recipient = recipient;
        }

        public override string GetFormattedText()
        {
            return $"[Email to {Recipient}] {Text}";
        }
    }

    class SmsMessage : Message
    {
        public readonly string PhoneNumber;

        public SmsMessage(string text, string phone)
            : base(text)
        {
            PhoneNumber = phone;
        }

        public override string GetFormattedText()
        {
            return $"[SMS to {PhoneNumber}] {Text}";
        }
    }

    class MessageProcessor<T> where T : Message
    {
        private readonly List<T> _messages = new List<T>();

        public void AddMessage(T message)
        {
            if (message != null)
                _messages.Add(message);
        }

        public void ProcessAll()
        {
            foreach (var m in _messages)
                Console.WriteLine(m?.GetFormattedText());
        }
    }

    // 1.6
    abstract class Vehicle
    {
        public readonly int MaxSpeed;
        public const int MinSpeed = 0;

        public Vehicle(int maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        public abstract string GetInfo();
    }

    class Car : Vehicle
    {
        public string FuelType;

        public Car(int speed, string fuel) : base(speed)
        {
            FuelType = fuel;
        }

        public override string GetInfo()
        {
            return $"Car (Fuel: {FuelType}, MaxSpeed: {MaxSpeed})";
        }
    }

    class Bicycle : Vehicle
    {
        public bool HasGears;

        public Bicycle(int speed, bool hasGears) : base(speed)
        {
            HasGears = hasGears;
        }

        public override string GetInfo()
        {
            return $"Bicycle (Gears: {HasGears}, MaxSpeed: {MaxSpeed})";
        }
    }

    static class VehicleHelper
    {
        public static T GetFaster<T>(T a, T b) where T : Vehicle
        {
            if (a.MaxSpeed >= b.MaxSpeed)
                return a;
            return b;
        }
    }

    class Program
    {
        static void Main()
        {
            // 1.1
            var w1 = new Wrapper<int?>(null);
            var w2 = new Wrapper<string>("Hello");
            Console.WriteLine(w1);
            Console.WriteLine(w2);
            Console.WriteLine(w1.GetValueOrDefault(100));

            // 1.2
            var entity = new NamedEntity("Alice");
            Console.WriteLine(entity.Description);

            // 1.3
            var circle = new Circle("Red", 5);
            var rect = new Rectangle("Blue", 4, 6);
            ShapeHelper.PrintInfo(circle);
            ShapeHelper.PrintInfo(rect);

            // 1.4
            Manager manager = new Manager("Bob", 5000, 1000);
            Intern intern = new Intern("Tom", 2000, manager);

            Employee[] employees = { manager, intern };
            foreach (var e in employees)
                Console.WriteLine($"{e.FullName}: {e.CalculateSalary()}");

            // 1.5
            var emailProcessor = new MessageProcessor<EmailMessage>();
            emailProcessor.AddMessage(new EmailMessage("Hello!", "user@mail.com"));
            emailProcessor.ProcessAll();

            // 1.6
            Car car = new Car(200, "Petrol");
            Bicycle bike = new Bicycle(40, true);

            var faster = VehicleHelper.GetFaster(car, car);
            Console.WriteLine("Faster: " + faster.GetInfo());
        }
    }
}