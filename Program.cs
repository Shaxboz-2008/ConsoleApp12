using System;
using System.Collections.Generic;

namespace BigPracticeProject
{
    // ЗАДАЧА 1
    class Book
    {
        public string Title;
        public string Author;
        public int Year;

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }

    // ЗАДАЧА 2
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentId { get; set; }
        public double AverageGrade { get; private set; }

        public Student(string firstName, string lastName, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentId = id;
            AverageGrade = 0.0;
        }

        public void UpdateGrade(double grade)
        {
            AverageGrade = grade;
        }
    }

    // ЗАДАЧА 3
    enum TrafficLightColor { Red, Yellow, Green }

    class TrafficLight
    {
        public TrafficLightColor CurrentColor { get; set; }

        public TrafficLight(TrafficLightColor startColor)
        {
            CurrentColor = startColor;
        }

        public void ChangeColor()
        {
            if (CurrentColor == TrafficLightColor.Red)
                CurrentColor = TrafficLightColor.Green;
            else if (CurrentColor == TrafficLightColor.Green)
                CurrentColor = TrafficLightColor.Yellow;
            else
                CurrentColor = TrafficLightColor.Red;
        }
    }

    // ЗАДАЧА 4
    class Rectangle
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        public Rectangle(double width, double height)
        {
            if (width > 0 && height > 0)
            {
                Width = width;
                Height = height;
            }
        }

        public double Area => Width * Height;
        public double Perimeter => 2 * (Width + Height);
    }

    // ЗАДАЧА 5
    class BankAccount
    {
        public string AccountNumber { get; }
        public string OwnerName { get; set; }
        public decimal Balance { get; private set; }

        public BankAccount(string number, string owner)
        {
            AccountNumber = number;
            OwnerName = owner;
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= Balance)
                Balance -= amount;
        }
    }

    // ЗАДАЧА 6
    class LibraryBook
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public bool IsAvailable { get; set; }

        public LibraryBook(string title, string author)
        {
            Title = title;
            Author = author;
            IsAvailable = true;
        }
    }

    class Library
    {
        private List<LibraryBook> books = new List<LibraryBook>();

        public void AddBook(LibraryBook book)
        {
            books.Add(book);
        }

        public List<LibraryBook> FindBooksByAuthor(string author)
        {
            return books.FindAll(b => b.Author == author);
        }

        public void BorrowBook(string title)
        {
            foreach (var book in books)
            {
                if (book.Title == title && book.IsAvailable)
                {
                    book.IsAvailable = false;
                    Console.WriteLine("Книга выдана");
                    return;
                }
            }
            Console.WriteLine("Книга недоступна");
        }
    }

    // ЗАДАЧА 7
    class GameCharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }

        public GameCharacter(string name, int strength)
        {
            Name = name;
            Health = 100;
            Strength = strength;
        }

        public void Attack(GameCharacter enemy)
        {
            enemy.Health -= Strength;
            Console.WriteLine($"{Name} атаковал {enemy.Name}. Здоровье врага: {enemy.Health}");
        }

        public void Attack(GameCharacter enemy, int multiplier)
        {
            enemy.Health -= Strength * multiplier;
            Console.WriteLine($"{Name} усилил атаку! Здоровье врага: {enemy.Health}");
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > 100) Health = 100;
        }
    }

    // ЗАДАЧА 8
    enum DeviceStatus { Off, On, Standby }

    class SmartDevice
    {
        public string Name { get; set; }
        public DeviceStatus Status { get; set; }
        public int PowerConsumption { get; set; }

        public SmartDevice(string name, int power)
        {
            Name = name;
            PowerConsumption = power;
            Status = DeviceStatus.Off;
        }
    }

    class SmartRoom
    {
        public List<SmartDevice> Devices = new List<SmartDevice>();

        public void AddDevice(SmartDevice device)
        {
            Devices.Add(device);
        }

        public void TurnOnAllDevices()
        {
            foreach (var d in Devices)
                d.Status = DeviceStatus.On;
        }

        public void TurnOffAllDevices()
        {
            foreach (var d in Devices)
                d.Status = DeviceStatus.Off;
        }

        public int TotalPowerConsumption
        {
            get
            {
                int total = 0;
                foreach (var d in Devices)
                    if (d.Status == DeviceStatus.On)
                        total += d.PowerConsumption;
                return total;
            }
        }
    }

    // ЗАДАЧА 9
    class Product
    {
        public int Id;
        public string Name;
        public decimal Price;
    }

    class OrderItem
    {
        public Product Product;
        public int Quantity;

        public decimal TotalPrice => Product.Price * Quantity;
    }

    class Order
    {
        public int OrderId;
        public string CustomerName;
        public List<OrderItem> Items = new List<OrderItem>();

        public void AddItem(Product product, int quantity)
        {
            Items.Add(new OrderItem { Product = product, Quantity = quantity });
        }

        public decimal OrderTotal
        {
            get
            {
                decimal total = 0;
                foreach (var item in Items)
                    total += item.TotalPrice;
                return total;
            }
        }
    }

    // ЗАДАЧА 10
    enum Direction { None, Up, Down }
    enum DoorStatus { Open, Closed }

    class Elevator
    {
        public int CurrentFloor { get; private set; }
        public int MaxFloor { get; }
        public int MinFloor => 1;

        public Direction Direction { get; private set; }
        public DoorStatus DoorStatus { get; private set; }
        public bool IsMoving { get; private set; }

        public Elevator(int maxFloor)
        {
            MaxFloor = maxFloor;
            CurrentFloor = 1;
            DoorStatus = DoorStatus.Closed;
            Direction = Direction.None;
        }

        public void Call(int targetFloor)
        {
            MoveTo(targetFloor);
        }

        public void MoveTo(int targetFloor)
        {
            if (targetFloor < MinFloor || targetFloor > MaxFloor)
                return;

            CloseDoor();
            IsMoving = true;

            if (targetFloor > CurrentFloor)
                Direction = Direction.Up;
            else if (targetFloor < CurrentFloor)
                Direction = Direction.Down;

            CurrentFloor = targetFloor;
            IsMoving = false;
            Direction = Direction.None;
            OpenDoor();
        }

        public void OpenDoor()
        {
            if (!IsMoving)
                DoorStatus = DoorStatus.Open;
        }

        public void CloseDoor()
        {
            DoorStatus = DoorStatus.Closed;
        }

        public string Status
        {
            get
            {
                if (IsMoving)
                    return $"Moving {Direction} to floor {CurrentFloor}";
                else
                    return $"Stopped on floor {CurrentFloor}, doors {DoorStatus}";
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== ЛИФТ ===");
            Elevator elevator = new Elevator(5);

            Console.WriteLine(elevator.Status);
            elevator.Call(4);
            Console.WriteLine(elevator.Status);
            elevator.MoveTo(2);
            Console.WriteLine(elevator.Status);
        }
    }
}