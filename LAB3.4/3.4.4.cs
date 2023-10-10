using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static Dictionary<string, string> users = new Dictionary<string, string>();

    static void Main()
    {
        LoadUsers();

        while (true)
        {
            Console.WriteLine("1. Реєстрація");
            Console.WriteLine("2. Авторизація");
            Console.WriteLine("3. Вихід");
            Console.Write("Виберіть опцію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterUser();
                    break;
                case "2":
                    LoginUser();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void LoadUsers()
    {
        if (File.Exists("users.txt"))
        {
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    users[parts[0]] = parts[1];
                }
            }
        }
    }

    static void SaveUsers()
    {
        using (StreamWriter writer = new StreamWriter("users.txt"))
        {
            foreach (var user in users)
            {
                writer.WriteLine($"{user.Key}:{user.Value}");
            }
        }
    }

    static void RegisterUser()
    {
        Console.Write("Введіть логін: ");
        string login = Console.ReadLine();

        if (users.ContainsKey(login))
        {
            Console.WriteLine("Користувач з таким логіном вже існує.");
            return;
        }

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        // Хешуємо пароль перед збереженням
        string hashedPassword = HashPassword(password);

        users[login] = hashedPassword;
        SaveUsers();

        Console.WriteLine("Користувач зареєстрований успішно.");
    }

    static void LoginUser()
    {
        Console.Write("Введіть логін: ");
        string login = Console.ReadLine();

        if (!users.ContainsKey(login))
        {
            Console.WriteLine("Користувача з таким логіном не існує.");
            return;
        }

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        string storedHashedPassword = users[login];

        if (VerifyPassword(password, storedHashedPassword))
        {
            Console.WriteLine("Авторизація успішна.");
        }
        else
        {
            Console.WriteLine("Помилка авторизації. Перевірте логін та пароль.");
        }
    }

    static string HashPassword(string password)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    static bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
