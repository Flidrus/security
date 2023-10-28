using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz5_2
{
    static internal class PBKDF2
    {
        static int outputlength;
        static HashAlgorithmName hashtype;
        static byte[] password;
        static byte[] salt;
        static byte[] hash;

        static public string Password {
            set { password = Encoding.UTF8.GetBytes(value); } 
        }
        static public void GenerateSalt() 
        {
            salt = RandomNumberGenerator.GetBytes(32);
        }
        //hashing
        static public void HashData(int iterations)
        {
            using (var rfc = new Rfc2898DeriveBytes(password, salt, iterations, hashtype))
            {
                hash = rfc.GetBytes(outputlength);
            }
        }
        //initialize setting hash algorithm type
        static public void SetHashType()
        {
            Console.WriteLine("\nChoose hashing algorithm:\n" +
                "1. SHA1\n" +
                "2. SHA256\n" +
                "3. SHA384\n" +
                "4. SHA512");
            char a = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (a)
            {
                case '1':
                    hashtype = HashAlgorithmName.SHA1;
                    outputlength = 20;
                    break;
                case '2':
                    hashtype = HashAlgorithmName.SHA256;
                    outputlength = 32;
                    break;
                case '3':
                    hashtype = HashAlgorithmName.SHA384;
                    outputlength = 48;
                    break;
                case '4':
                    hashtype = HashAlgorithmName.SHA512;
                    outputlength = 64;
                    break;
                default:
                    hashtype = HashAlgorithmName.SHA1;
                    outputlength = 20;
                    Console.WriteLine("\nUnrecognized command! Using SHA1\n");
                    break;
            }
        }
        //print all data to console
        static public void PrintData()
        {
            Console.WriteLine($"Password: {Encoding.UTF8.GetString(password)}\n" +
                $"Hash: {Convert.ToBase64String(hash)}\n" +
                $"Salt: {Convert.ToBase64String(salt)}");
        }
    }