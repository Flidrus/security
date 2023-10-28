using System;
using System.Diagnostics;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter password: ");
        string password = Console.ReadLine();
        byte[] salt = GenerateSalt();

        int[] iterationsList = { 60000, 70000, 80000, 90000, 100000 };

        foreach (int iterations in iterationsList)
        {
            HashWithIterations(password, salt, iterations);
        }
    }

    static byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    static void HashWithIterations(string password, byte[] salt, int iterations)
    {
        var sw = new Stopwatch();
        sw.Start();

        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] hash = pbkdf2.GetBytes(32); // 32 байта - длина хеша
            // Здесь вы можете сохранить хеш в базе данных или в другом месте для будущей проверки
        }

        sw.Stop();
        Console.WriteLine($"Iterations: {iterations}, Elapsed time: {sw.ElapsedMilliseconds}ms");
    }
}

