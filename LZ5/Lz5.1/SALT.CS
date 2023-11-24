using System;
using System.Security.Cryptography;
using System.Text;

namespace LAB5_2
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            for (int i = 1; i <= 10; i++)
            {
                int iterations = i * 50000;
                string hashedPassword = HashPassword(password, iterations);
                Console.WriteLine($"Iterations: {iterations}, Hashed Password: {hashedPassword}");
            }

            Console.ReadLine();
        }

        public static string HashPassword(string password, int iterations)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(20); // 20 bytes for SHA256
                byte[] hashWithSalt = new byte[36]; // 16 bytes salt + 20 bytes hash

                Array.Copy(salt, 0, hashWithSalt, 0, 16);
                Array.Copy(hash, 0, hashWithSalt, 16, 20);

                return Convert.ToBase64String(hashWithSalt);
            }
        }
    }
}




