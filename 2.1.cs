using System;
using System.IO;

class FileEncryption
{
    static byte[] ReadFile(string filePath)
    {
        return File.ReadAllBytes(filePath);
    }

    static void WriteFile(string filePath, byte[] data)
    {
        File.WriteAllBytes(filePath, data);
    }

    static void EncryptFile(string filePath, string encryptedFilePath)
    {
        byte[] data = ReadFile(filePath);

        // Ключ для XOR-шифрування
        byte[] key = { 0x11, 0x22, 0x33, 0x44 };

        // Виконуємо операцію XOR над кожним байтом у файлі з ключем
        for (int i = 0; i < data.Length; i++)
        {
            data[i] ^= key[i % key.Length];
        }

        // Записуємо зашифровані дані у новий файл
        WriteFile(encryptedFilePath, data);
    }

    static void DecryptFile(string encryptedFilePath, string decryptedFilePath)
    {
        byte[] data = ReadFile(encryptedFilePath);

        // Ключ для XOR-розшифрування
        byte[] key = { 0x11, 0x22, 0x33, 0x44 };

        // Виконуємо знову операцію XOR над кожним байтом у файлі з ключем
        for (int i = 0; i < data.Length; i++)
        {
            data[i] ^= key[i % key.Length];
        }

        // Записуємо розшифровані дані у новий файл
        WriteFile(decryptedFilePath, data);
    }

    static void Main()
    {
        string filePath = "C:/Users/gomel/Documents/Course Work HandBook2023.pdf";
        string encryptedFilePath = filePath + ".encrypted";
        string decryptedFilePath = filePath.Replace(".pdf", "_decrypted.pdf");

        // Шифруємо файл
        EncryptFile(filePath, encryptedFilePath);

        // Розшифровуємо файл
        DecryptFile(encryptedFilePath, decryptedFilePath);

        Console.WriteLine("Файл зашифровано та розшифровано успішно.");
    }
}

