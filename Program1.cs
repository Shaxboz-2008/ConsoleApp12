using System;

namespace StaticAndEnumTasks
{
    // ЗАДАНИЕ 1
    class User
    {
        private static int _userCount = 0;
        public static int IdCounter = 0;

        public int Id { get; private set; }

        public User()
        {
            _userCount++;
            IdCounter++;
            Id = IdCounter;
        }

        public static int GetTotalUsers()
        {
            return _userCount;
        }
    }

    // ЗАДАНИЕ 2
    class Planet
    {
        private const double G = 6.67430e-11;

        public string Name { get; }
        public double Mass { get; }
        public double Radius { get; }

        private Planet(string name, double mass, double radius)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
        }

        public static readonly Planet Mercury =
            new Planet("Mercury", 3.30e23, 2.44e6);

        public static readonly Planet Venus =
            new Planet("Venus", 4.87e24, 6.05e6);

        public static readonly Planet Earth =
            new Planet("Earth", 5.97e24, 6.37e6);

        public static readonly Planet Mars =
            new Planet("Mars", 6.42e23, 3.39e6);

        public double CalculateGravity()
        {
            return (G * Mass) / (Radius * Radius);
        }
    }

    // ЗАДАНИЕ 3
    class LogLevel
    {
        public static readonly LogLevel Info = new LogLevel(ConsoleColor.White);
        public static readonly LogLevel Warning = new LogLevel(ConsoleColor.Yellow);
        public static readonly LogLevel Error = new LogLevel(ConsoleColor.Red);

        private ConsoleColor _color;

        private LogLevel(ConsoleColor color)
        {
            _color = color;
        }

        public ConsoleColor GetConsoleColor()
        {
            return _color;
        }
    }

    static class Logger
    {
        public static void Log(string message, LogLevel level)
        {
            Console.ForegroundColor = level.GetConsoleColor();
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАНИЕ 1 ===");

            User user1 = new User();
            User user2 = new User();
            User user3 = new User();

            Console.WriteLine($"User1 ID: {user1.Id}");
            Console.WriteLine($"User2 ID: {user2.Id}");
            Console.WriteLine($"User3 ID: {user3.Id}");

            Console.WriteLine("Всего пользователей: " + User.GetTotalUsers());

            Console.WriteLine("\n=== ЗАДАНИЕ 2 ===");

            Console.WriteLine($"Планета: {Planet.Earth.Name}");
            Console.WriteLine($"Гравитация: {Planet.Earth.CalculateGravity()}");

            Console.WriteLine($"Планета: {Planet.Mars.Name}");
            Console.WriteLine($"Гравитация: {Planet.Mars.CalculateGravity()}");

            Console.WriteLine("\n=== ЗАДАНИЕ 3 ===");

            Logger.Log("Система запущена", LogLevel.Info);
            Logger.Log("Память заканчивается", LogLevel.Warning);
            Logger.Log("Критический сбой!", LogLevel.Error);
        }
    }
}