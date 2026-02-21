using System;
using System.Collections.Generic;

namespace PracticeTasks
{
    // ЗАДАЧА 1
    class MathConstants
    {
        public const double Pi = 3.1415926535;
        public static readonly double E = 2.7182818284;
    }

    // ЗАДАЧА 2 и 3
    class Book
    {
        public readonly string Title;
        public string Author;
        public int Pages;

        public Book(string title, string author, int pages)
        {
            Title = title;
            Author = author;
            Pages = pages;
        }

        public void PrintInfo()
        {
            string author = Author ?? "Автор неизвестен";
            Console.WriteLine($"Книга: {Title}, Автор: {author}, Страниц: {Pages}");
        }
    }

    // ЗАДАЧА 4
    class Library
    {
        public const int MaxBooks = 1000;
        private Book[] _catalog;
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Library()
        {
            _catalog = new Book[MaxBooks];
        }

        public void AddBook(Book book)
        {
            for (int i = 0; i < _catalog.Length; i++)
            {
                if (_catalog[i] == null)
                {
                    _catalog[i] = book;
                    return;
                }
            }
        }
    }

    // ЗАДАЧА 5
    class Student
    {
        public string Name { get; }
        public string Email { get; set; }
        private List<int> _grades;

        public IReadOnlyList<int> Grades => _grades;

        public Student(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Имя не может быть пустым");

            Name = name;
            _grades = new List<int>();
        }

        public void AddGrade(int grade)
        {
            _grades.Add(grade);
        }
    }

    // ЗАДАЧА 6
    class Product
    {
        public readonly int Id;
        public string Name { get; }
        public decimal Price;

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    class Customer
    {
        public readonly int Id;
        public string FullName;
        public string Address;

        public Customer(int id, string name)
        {
            Id = id;
            FullName = name;
        }
    }

    class Order
    {
        public readonly int OrderId;
        public readonly Customer Customer;
        private List<Product> _products = new List<Product>();

        public Order(int id, Customer customer)
        {
            OrderId = id;
            Customer = customer ?? throw new ArgumentNullException("Customer не может быть null");
        }

        public void AddProduct(Product p)
        {
            if (p != null)
                _products.Add(p);
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var p in _products)
                    total += p.Price;
                return total;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // Задача 1
            Console.WriteLine("=== ЗАДАЧА 1 ===");
            Console.WriteLine($"Pi = {MathConstants.Pi}");
            Console.WriteLine($"E = {MathConstants.E}");

            // Задача 2 и 3
            Console.WriteLine("\n=== ЗАДАЧА 2 и 3 ===");
            Book book = new Book("Война и мир", null, 1225);
            book.PrintInfo();

            // Задача 4
            Console.WriteLine("\n=== ЗАДАЧА 4 ===");
            Library library = new Library();
            library.Name = "Моя библиотека";

            library.AddBook(book);
            Console.WriteLine("Книга добавлена в библиотеку");

            // Задача 5
            Console.WriteLine("\n=== ЗАДАЧА 5 ===");
            Student student = new Student("Иван");
            student.Email = "ivan@example.com";
            student.AddGrade(5);
            student.AddGrade(4);

            Console.WriteLine($"Студент: {student.Name}, Email: {student.Email}");
            Console.WriteLine("Оценки: " + string.Join(", ", student.Grades));

            // Задача 6
            Console.WriteLine("\n=== ЗАДАЧА 6 ===");
            Customer customer = new Customer(1, "Петр");
            Order order = new Order(100, customer);

            Product p1 = new Product(1, "Телефон", 500);
            Product p2 = new Product(2, "Наушники", 100);

            order.AddProduct(p1);
            order.AddProduct(p2);

            Console.WriteLine($"Сумма заказа: {order.TotalPrice}");
        }
    }
}