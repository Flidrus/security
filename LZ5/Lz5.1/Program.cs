using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter a password: ");
        string password = Console.ReadLine();

        // Генерация случайной соли
        byte[] salt = GenerateSalt();

        // Хеширование пароля с солью
        byte[] hash = ComputeHash(password, salt);

        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
        Console.WriteLine($"Hash: {Convert.ToBase64String(hash)}");

        Console.Write("Enter the password again: ");
        string newPassword = Console.ReadLine();

        // Проверка введенного пароля на соответствие сохраненному хешу
        bool passwordMatches = VerifyPassword(newPassword, salt, hash);

        if (passwordMatches)
        {
            Console.WriteLine("Password is correct!");
        }
        else
        {
            Console.WriteLine("Password is incorrect.");
        }
    }

    // Генерация случайной соли
    static byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    // Хеширование пароля с солью
    static byte[] ComputeHash(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(32); // 32 байта - длина хеша
        }
    }

    // Проверка введенного пароля на соответствие сохраненному хешу
    static bool VerifyPassword(string newPassword, byte[] salt, byte[] savedHash)
    {
        byte[] newHash = ComputeHash(newPassword, salt);
        return AreHashesEqual(newHash, savedHash);
    }

    // Проверка равенства двух хешей
    static bool AreHashesEqual(byte[] hash1, byte[] hash2)
    {
        if (hash1.Length != hash2.Length)
            return false;

        for (int i = 0; i < hash1.Length; i++)
        {
            if (hash1[i] != hash2[i])
                return false;
        }

        return true;
    }
}

