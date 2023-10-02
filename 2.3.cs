using System;
using System.Text;
using System.IO;

namespace Lz2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь до зашифрованного файла: ");
            string path = Console.ReadLine();

            byte[] encryptedFile = File.ReadAllBytes(path);

            byte[] key = GetKeyBytes("мит-21");

            byte[] decrypted = Decrypt(encryptedFile, key);

            Console.WriteLine("Расшифрованное сообщение:");
            Console.WriteLine(Encoding.UTF8.GetString(decrypted));
        }

        static byte[] GetKeyBytes(string keyString)
        {
            return Encoding.UTF8.GetBytes(keyString);
        }

        static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            byte[] decrypted = new byte[encrypted.Length];

            for (int i = 0; i < encrypted.Length; i += key.Length)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    decrypted[i + j] = (byte)(encrypted[i + j] ^ key[j]);
                }
            }

            return decrypted;
        }
    }
}