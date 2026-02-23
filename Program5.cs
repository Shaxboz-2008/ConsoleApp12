using System;
using System.Collections.Generic;

namespace ConsoleApp17
{

    public interface IAnimal
    {
        void MakeSound();
    }

    public class Dog : IAnimal
    {
        public void MakeSound()
        {
            Console.WriteLine("Гав");
        }
    }

    public class Cat : IAnimal
    {
        public void MakeSound()
        {
            Console.WriteLine("Мяу");
        }
    }

    public interface IDevice
    {
        void TurnOn();
        void TurnOff();
    }

    public class TV : IDevice
    {
        public void TurnOn()
        {
            Console.WriteLine("TV включён");
        }

        public void TurnOff()
        {
            Console.WriteLine("TV выключен");
        }
    }

    public class Radio : IDevice
    {
        public void TurnOn()
        {
            Console.WriteLine("Radio включено");
        }

        public void TurnOff()
        {
            Console.WriteLine("Radio выключено");
        }
    }

    public interface IShape
    {
        double GetArea();
    }

    public class Square : IShape
    {
        public double Side;

        public Square(double side)
        {
            Side = side;
        }

        public double GetArea()
        {
            return Side * Side;
        }
    }

    public class Rectangle : IShape
    {
        public double Width;
        public double Height;

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double GetArea()
        {
            return Width * Height;
        }
    }

    public interface ICleaner
    {
        void Clean();
    }

    public interface IChargable
    {
        void Charge();
    }

    public class Robot : ICleaner, IChargable
    {
        public void Clean()
        {
            Console.WriteLine("Робот убирает...");
        }

        public void Charge()
        {
            Console.WriteLine("Робот заряжается...");
        }
    }

    public interface IStorage
    {
        void Save(string data);
        string Load();
    }

    public class DatabaseStorage : IStorage
    {
        public void Save(string data)
        {
            Console.WriteLine("Данные сохранены в базу");
        }

        public string Load()
        {
            Console.WriteLine("Данные загружены из базы");
            return "Data";
        }
    }


    class Program
    {
        static void UseDevice(IDevice device)
        {
            device.TurnOn();
            device.TurnOff();
        }

        static void CheckAge(int age)
        {
            if (age < 0 || age > 120)
                throw new ArgumentOutOfRangeException("Возраст вне допустимого диапазона");

            Console.WriteLine("Возраст корректный");
        }

        static void Main()
        {
            Console.WriteLine("1. Голоса животных");
            List<IAnimal> animals = new List<IAnimal>
        {
            new Dog(),
            new Cat()
        };

            foreach (var animal in animals)
                animal.MakeSound();

            Console.WriteLine("\n2. Универсальный пульт");
            UseDevice(new TV());
            UseDevice(new Radio());


            Console.WriteLine("\n3. Площади фигур");
            List<IShape> shapes = new List<IShape>
        {
            new Square(4),
            new Rectangle(5, 3)
        };

            foreach (var shape in shapes)
                Console.WriteLine("Площадь: " + shape.GetArea());

            Console.WriteLine("\n4. Робот");
            Robot robot = new Robot();
            robot.Clean();
            robot.Charge();

            Console.WriteLine("\n5. Хранилище");
            IStorage storage = new DatabaseStorage();
            storage.Save("Hello");
            storage.Load();


            Console.WriteLine("\n6. Деление");
            try
            {
                Console.Write("Введите первое число: ");
                int a = int.Parse(Console.ReadLine());

                Console.Write("Введите второе число: ");
                int b = int.Parse(Console.ReadLine());

                int result = a / b;
                Console.WriteLine("Результат: " + result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка: деление на ноль!");
            }

            Console.WriteLine("\n7. Парсинг");
            try
            {
                Console.Write("Введите число: ");
                int number = int.Parse(Console.ReadLine());
                Console.WriteLine("Вы ввели: " + number);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введено не число!");
            }

            Console.WriteLine("\n8. Проверка возраста");
            try
            {
                CheckAge(150);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.WriteLine("\n9. Границы массива");
            try
            {
                int[] arr = new int[5];
                Console.WriteLine(arr[9]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ошибка: выход за пределы массива!");
            }

            Console.WriteLine("\n10. Работа с ресурсом");
            try
            {
                Console.WriteLine("Работа с файлом...");
                throw new Exception("Ошибка при работе");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Связь с ресурсом закрыта");
            }
        }
    }
}