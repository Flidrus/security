using System;
using System.Security.Cryptography;
using System.Text;

namespace Lz3_4_4
{

    class Program
    {
        static string CalculateHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static bool VerifyPassword(string storedHash, string userInput)
        {
            string hashedInput = CalculateHash(userInput);
            return storedHash.Equals(hashedInput, StringComparison.OrdinalIgnoreCase);
        }

        static void Main()
        {
            // Приклад реєстрації користувача
            string username = "user123";
            string password = "SecretPassword";

            // Зберігаємо хеш пароля в базі даних (зазвичай вона буде представлена)
            string storedPasswordHash = CalculateHash(password);

            // Приклад авторизації
            Console.WriteLine("Enter username:");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string inputPassword = Console.ReadLine();

            if (inputUsername == username && VerifyPassword(storedPasswordHash, inputPassword))
            {
                Console.WriteLine("Authentication successful!");
            }
            else
            {
                Console.WriteLine("Authentication failed!");
            }
        }
    }
}
