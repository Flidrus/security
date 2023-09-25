using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lAB2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Зчитуємо повний шлях до файлу, що потрібно зашифрувати
            Console.Write("Введіть шлях до файлу: ");
            string path = Console.ReadLine();

            // Зчитуємо вміст файлу в байтовий масив   
            byte[] fileBytes = File.ReadAllBytes(path);

            // Шифруємо вміст файлу
            byte[] encryptedBytes = Encrypt(fileBytes);

            // Витягуємо ім'я файлу з шляху  
            string filename = Path.GetFileNameWithoutExtension(path);

            // Формуємо шлях до зашифрованого файлу з розширенням .dat
            string encryptedPath = Path.ChangeExtension(path, "dat");

            // Записуємо зашифрований файл
            File.WriteAllBytes(encryptedPath, encryptedBytes);

            Console.WriteLine("Файл зашифровано!");
        }

        // Метод шифрування
        static byte[] Encrypt(byte[] plainText)
        {
            // Шифрування тут за допомогою AES наприклад
            return plainText;
        }
    }
}
