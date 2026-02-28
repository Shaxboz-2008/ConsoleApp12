using System;
using System.Collections.Generic;

// ЗАДАНИЕ 1

public class Sentence
{
    private string[] _words;

    public Sentence(string text)
    {
        _words = text.Split(' ');
    }

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _words.Length)
                throw new IndexOutOfRangeException();

            return _words[index];
        }
        set
        {
            if (index < 0 || index >= _words.Length)
                throw new IndexOutOfRangeException();

            _words[index] = value;
        }
    }

    public override string ToString()
    {
        return string.Join(" ", _words);
    }
}

// ЗАДАНИЕ 2

public class TemperatureGrid
{
    private double[,] _grid = new double[5, 5];

    public double this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= 5 || col < 0 || col >= 5)
                throw new IndexOutOfRangeException();

            return _grid[row, col];
        }
        set
        {
            if (row < 0 || row >= 5 || col < 0 || col >= 5)
                throw new IndexOutOfRangeException();

            _grid[row, col] = value;
        }
    }
}

// ЗАДАНИЕ 3

public class Contact
{
    public string Name;
    public string Phone;

    public Contact(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}

public class PhoneBook
{
    private List<Contact> _contacts = new List<Contact>();

    public void Add(Contact contact)
    {
        _contacts.Add(contact);
    }

    public string this[string name]
    {
        get
        {
            foreach (var contact in _contacts)
            {
                if (contact.Name == name)
                    return contact.Phone;
            }

            return "Контакт не найден";
        }
    }
}

// ЗАДАНИЕ 4

public class BitController
{
    private byte _data = 0;

    public int this[int bitIndex]
    {
        get
        {
            if (bitIndex < 0 || bitIndex > 7)
                throw new IndexOutOfRangeException();

            return (_data >> bitIndex) & 1;
        }
        set
        {
            if (bitIndex < 0 || bitIndex > 7)
                throw new IndexOutOfRangeException();

            if (value == 1)
                _data = (byte)(_data | (1 << bitIndex));
            else if (value == 0)
                _data = (byte)(_data & ~(1 << bitIndex));
            else
                throw new ArgumentException("Бит может быть только 0 или 1");
        }
    }

    public override string ToString()
    {
        return Convert.ToString(_data, 2).PadLeft(8, '0');
    }
}

// ЗАДАНИЕ 5

public class DataVault
{
    private string[] _items = new string[10];

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _items.Length)
                throw new IndexOutOfRangeException();

            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _items.Length)
                throw new IndexOutOfRangeException();

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Нельзя записывать пустые строки");

            _items[index] = value;
        }
    }

    public string this[string index]
    {
        get
        {
            int i = int.Parse(index);
            return this[i];
        }
        set
        {
            int i = int.Parse(index);
            this[i] = value;
        }
    }
}


class Program
{
    static void Main()
    {
        // Задание 1
        Sentence s = new Sentence("Я люблю программирование");
        Console.WriteLine(s[1]); // люблю
        s[1] = "обожаю";
        Console.WriteLine(s);

        // Задание 2
        TemperatureGrid grid = new TemperatureGrid();
        grid[2, 3] = 25.5;
        Console.WriteLine(grid[2, 3]);

        // Задание 3
        PhoneBook book = new PhoneBook();
        book.Add(new Contact("Иван", "12345"));
        Console.WriteLine(book["Иван"]);
        Console.WriteLine(book["Петр"]);

        // Задание 4
        BitController controller = new BitController();
        controller[0] = 1;
        controller[3] = 1;
        Console.WriteLine(controller);
        Console.WriteLine(controller[3]);

        // Задание 5
        DataVault vault = new DataVault();
        vault[0] = "Секрет";
        Console.WriteLine(vault["0"]);
    }
}