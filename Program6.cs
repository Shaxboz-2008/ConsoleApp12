using System;
using System.Collections.Generic;

// ЗАДАНИЕ 1

public struct ComplexNumber
{
    public double A; 
    public double B;

    public ComplexNumber(double a, double b)
    {
        A = a;
        B = b;
    }

    public static ComplexNumber operator +(ComplexNumber x, ComplexNumber y)
        => new ComplexNumber(x.A + y.A, x.B + y.B);

    public static ComplexNumber operator -(ComplexNumber x, ComplexNumber y)
        => new ComplexNumber(x.A - y.A, x.B - y.B);

    public static ComplexNumber operator *(ComplexNumber x, ComplexNumber y)
        => new ComplexNumber(
            x.A * y.A - x.B * y.B,
            x.A * y.B + x.B * y.A);

    public static explicit operator double(ComplexNumber c)
        => Math.Sqrt(c.A * c.A + c.B * c.B);

    public override string ToString()
        => $"{A} + {B}i";
}


// ЗАДАНИЕ 2

public class Money
{
    public decimal Amount;
    public string Currency;

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money operator +(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw new InvalidOperationException("Разные валюты");

        return new Money(x.Amount + y.Amount, x.Currency);
    }

    public static Money operator -(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw new InvalidOperationException("Разные валюты");

        return new Money(x.Amount - y.Amount, x.Currency);
    }

    public static bool operator >(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw new InvalidOperationException("Разные валюты");

        return x.Amount > y.Amount;
    }

    public static bool operator <(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw new InvalidOperationException("Разные валюты");

        return x.Amount < y.Amount;
    }

    public static implicit operator Money(decimal amount)
        => new Money(amount, "USD");

    public override string ToString()
        => $"{Amount} {Currency}";
}


// ЗАДАНИЕ 3

public struct Vector2D
{
    public double X;
    public double Y;

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Vector2D operator +(Vector2D a, Vector2D b)
        => new Vector2D(a.X + b.X, a.Y + b.Y);

    public static Vector2D operator *(Vector2D v, double scalar)
        => new Vector2D(v.X * scalar, v.Y * scalar);

    public static bool operator true(Vector2D v)
        => Math.Sqrt(v.X * v.X + v.Y * v.Y) != 0;

    public static bool operator false(Vector2D v)
        => Math.Sqrt(v.X * v.X + v.Y * v.Y) == 0;

    public static bool operator ==(Vector2D a, Vector2D b)
        => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Vector2D a, Vector2D b)
        => !(a == b);

    public override bool Equals(object obj)
        => obj is Vector2D v && this == v;

    public override int GetHashCode()
        => X.GetHashCode() ^ Y.GetHashCode();

    public override string ToString()
        => $"({X}, {Y})";
}


// ЗАДАНИЕ 4

public class Fraction
{
    public int Numerator;
    public int Denominator;

    public Fraction(int n, int d)
    {
        if (d == 0) throw new DivideByZeroException();
        Numerator = n;
        Denominator = d;
        Simplify();
    }

    void Simplify()
    {
        int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
        Numerator /= gcd;
        Denominator /= gcd;
    }

    int GCD(int a, int b)
    {
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    public static Fraction operator +(Fraction a, Fraction b)
        => new Fraction(
            a.Numerator * b.Denominator + b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);

    public static Fraction operator -(Fraction a, Fraction b)
        => new Fraction(
            a.Numerator * b.Denominator - b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);

    public static Fraction operator *(Fraction a, Fraction b)
        => new Fraction(
            a.Numerator * b.Numerator,
            a.Denominator * b.Denominator);

    public static Fraction operator /(Fraction a, Fraction b)
        => new Fraction(
            a.Numerator * b.Denominator,
            a.Denominator * b.Numerator);

    public static implicit operator Fraction(int n)
        => new Fraction(n, 1);

    public static explicit operator double(Fraction f)
        => (double)f.Numerator / f.Denominator;

    public override string ToString()
        => $"{Numerator}/{Denominator}";
}


// ЗАДАНИЕ 5

public struct DataSize
{
    public long Bytes;

    public DataSize(long bytes)
    {
        Bytes = bytes;
    }

    public static DataSize operator ++(DataSize d)
    {
        d.Bytes += 1024;
        return d;
    }

    public static DataSize operator --(DataSize d)
    {
        d.Bytes -= 1024;
        return d;
    }

    public static bool operator >(DataSize a, DataSize b)
        => a.Bytes > b.Bytes;

    public static bool operator <(DataSize a, DataSize b)
        => a.Bytes < b.Bytes;

    public static explicit operator DataSize(string input)
    {
        string[] parts = input.Split(' ');
        long value = long.Parse(parts[0]);
        string unit = parts[1].ToUpper();

        if (unit == "KB") value *= 1024;
        if (unit == "MB") value *= 1024 * 1024;
        if (unit == "GB") value *= 1024 * 1024 * 1024;

        return new DataSize(value);
    }

    public override string ToString()
        => $"{Bytes} bytes";
}


class Program
{
    static void Main()
    {

        var c1 = new ComplexNumber(2, 3);
        var c2 = new ComplexNumber(1, 4);
        var c3 = c1 * c2;
        Console.WriteLine("Complex: " + c3);
        Console.WriteLine("Модуль: " + (double)c3);


        Money m1 = 100;
        Money m2 = new Money(50, "USD");
        Console.WriteLine("Money: " + (m1 + m2));


        var v1 = new Vector2D(3, 4);
        if (v1)
            Console.WriteLine("Vector не нулевой");


        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = 2;
        Console.WriteLine("Fraction: " + (f1 + f2));


        DataSize size = (DataSize)"100 MB";
        size++;
        Console.WriteLine("DataSize: " + size);
    }
}