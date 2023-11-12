using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lz5_3
{
    internal class MyHashManager
    {
        public const int SaltLength = 32;
        public const int Iterations = 60000;

        struct UserData
        {
            public byte[] Hash;
            public byte[] Salt;
        }

        static Dictionary<string, UserData> UserDatabase = new Dictionary<string, UserData>();

        static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(SaltLength);
        }

        static byte[] ComputeHash(byte[] password, byte[] salt)
        {
            using (var rfc = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return rfc.GetBytes(256 / 8); // Используем HashSize / 8 для размера хэша в байтах
            }
        }

        public static void ManageUser(string username, char action)
        {
            byte[] userPassword;
            UserData userData;

            switch (action)
            {
                case 'l':
                    try
                    {
                        userData = UserDatabase[username];
                        Console.Write("Enter password: ");
                        userPassword = Encoding.UTF8.GetBytes(Console.ReadLine());

                        if (Enumerable.SequenceEqual(ComputeHash(userPassword, userData.Salt), userData.Hash))
                        {
                            Console.WriteLine("\n\n\n\nLogin successful!\n\n\n\n");
                        }
                        else
                        {
                            Console.WriteLine("\n\nWrong password!!!\n\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\n\nNo such user!\n\n");
                    }
                    break;
                case 'r':
                    try
                    {
                        userData = UserDatabase[username];
                        Console.WriteLine("\n\nUser already exists!!!\n\n");
                    }
                    catch
                    {
                        userData = new UserData();

                        Console.Write("Enter password: ");
                        userPassword = Encoding.UTF8.GetBytes(Console.ReadLine());

                        userData.Salt = GenerateSalt();
                        userData.Hash = ComputeHash(userPassword, userData.Salt);

                        UserDatabase.Add(username, userData);
                        Console.WriteLine("\n\n\n\nUser created successfully!\n\n\n\n");
                    }
                    break;
                default:
                    Console.WriteLine("Error! Unrecognized command");
                    return;
            }
        }
    }
}
