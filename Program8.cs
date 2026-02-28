using System;

// ЗАДАНИЕ 1 

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

    public static Vector2D operator -(Vector2D a, Vector2D b)
        => new Vector2D(a.X - b.X, a.Y - b.Y);

    public static Vector2D operator *(Vector2D v, double scalar)
        => new Vector2D(v.X * scalar, v.Y * scalar);

    public static Vector2D operator *(double scalar, Vector2D v)
        => new Vector2D(v.X * scalar, v.Y * scalar);

    public override string ToString()
        => $"({X}; {Y})";
}

// ЗАДАНИЕ 2 

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

    private void Simplify()
    {
        int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
        Numerator /= gcd;
        Denominator /= gcd;
    }

    private int GCD(int a, int b)
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

    public static bool operator ==(Fraction a, Fraction b)
        => a.Numerator == b.Numerator && a.Denominator == b.Denominator;

    public static bool operator !=(Fraction a, Fraction b)
        => !(a == b);

    public static bool operator <(Fraction a, Fraction b)
        => a.Numerator * b.Denominator < b.Numerator * a.Denominator;

    public static bool operator >(Fraction a, Fraction b)
        => a.Numerator * b.Denominator > b.Numerator * a.Denominator;

    public static bool operator <=(Fraction a, Fraction b)
        => a < b || a == b;

    public static bool operator >=(Fraction a, Fraction b)
        => a > b || a == b;

    public override bool Equals(object obj)
    {
        if (obj is Fraction f)
            return this == f;
        return false;
    }

    public override int GetHashCode()
        => Numerator.GetHashCode() ^ Denominator.GetHashCode();

    public override string ToString()
        => $"{Numerator}/{Denominator}";
}

// ЗАДАНИЕ 3

public class Money
{
    public decimal Amount;
    public string Currency;

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new ArgumentException("Разные валюты");

        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new ArgumentException("Разные валюты");

        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public static bool operator ==(Money a, Money b)
        => a.Amount == b.Amount && a.Currency == b.Currency;

    public static bool operator !=(Money a, Money b)
        => !(a == b);

    public static Money operator *(Money m, decimal factor)
        => new Money(m.Amount * factor, m.Currency);

    public override bool Equals(object obj)
    {
        if (obj is Money m)
            return this == m;
        return false;
    }

    public override int GetHashCode()
        => Amount.GetHashCode() ^ Currency.GetHashCode();

    public override string ToString()
        => $"{Amount} {Currency}";
}

// ЗАДАНИЕ 4

public class Matrix2x2
{
    public double A11, A12, A21, A22;

    public Matrix2x2(double a11, double a12, double a21, double a22)
    {
        A11 = a11;
        A12 = a12;
        A21 = a21;
        A22 = a22;
    }

    public static Matrix2x2 operator -(Matrix2x2 m)
        => new Matrix2x2(-m.A11, -m.A12, -m.A21, -m.A22);

    public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b)
        => new Matrix2x2(
            a.A11 + b.A11,
            a.A12 + b.A12,
            a.A21 + b.A21,
            a.A22 + b.A22);

    public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
        => new Matrix2x2(
            a.A11 * b.A11 + a.A12 * b.A21,
            a.A11 * b.A12 + a.A12 * b.A22,
            a.A21 * b.A11 + a.A22 * b.A21,
            a.A21 * b.A12 + a.A22 * b.A22);

    public override string ToString()
        => $"[{A11} {A12}]\n[{A21} {A22}]";
}

// ЗАДАНИЕ 5

public struct Complex
{
    public double A; 
    public double B; 

    public Complex(double a, double b)
    {
        A = a;
        B = b;
    }

    public static Complex operator +(Complex x, Complex y)
        => new Complex(x.A + y.A, x.B + y.B);

    public static Complex operator -(Complex x, Complex y)
        => new Complex(x.A - y.A, x.B - y.B);

    public static Complex operator *(Complex x, Complex y)
        => new Complex(
            x.A * y.A - x.B * y.B,
            x.A * y.B + x.B * y.A);

    public override string ToString()
        => $"{A} + {B}i";
}


class Program
{
    static void Main()
    {

        Vector2D v1 = new Vector2D(2, 3);
        Vector2D v2 = new Vector2D(1, 1);
        Console.WriteLine(v1 + v2);


        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(2, 3);
        Console.WriteLine(f1 + f2);
        Console.WriteLine(f1 > f2);


        Money m1 = new Money(100, "USD");
        Money m2 = new Money(50, "USD");
        Console.WriteLine(m1 + m2);
        Console.WriteLine(m1 * 1.2m);


        Matrix2x2 mA = new Matrix2x2(1, 2, 3, 4);
        Matrix2x2 mB = new Matrix2x2(2, 0, 1, 2);
        Console.WriteLine(mA * mB);


        Complex c1 = new Complex(2, 3);
        Complex c2 = new Complex(1, 4);
        Console.WriteLine(c1 * c2);
    }
}