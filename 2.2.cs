using System.Security.Cryptography;
using System.Text;

namespace LAB2_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to the .txt or .dat file to encrypt/decrypt:");
            var path = Console.ReadLine();
            //extracting path to save files into the same folder
            string savepath = path.Remove(path.LastIndexOf(@"\"));

            byte[] msgbytes = File.ReadAllBytes(path);
            string msg = Encoding.UTF8.GetString(msgbytes);
            byte[] key;
            byte[] encryptbytes;

            //switch case for encrypting / dectypting choice
            char choice;
            Console.WriteLine("Do you want to encrypt the file (e) or decrypt (d) it?");
            choice = Console.ReadKey().KeyChar;

            switch (choice)
            {
                case 'e' or 'E':
                    //encryption
                    key = KeyGen(msgbytes.Length);
                    encryptbytes = EncodeFunc(msgbytes, key);

                    File.WriteAllBytes(savepath + @"\encrypted.dat", encryptbytes);
                    File.WriteAllBytes(savepath + @"\key.dat", key);

                    Console.WriteLine("\n\nMessage: " + msg);
                    Console.WriteLine("\n\nEncrypted message: " + Encoding.UTF8.GetString(encryptbytes));
                    Console.WriteLine("\n\nKey: " + Encoding.UTF8.GetString(key));
                    break;

                case 'd' or 'D':
                    //decryption
                    Console.WriteLine("\nEnter path to the key:");
                    key = File.ReadAllBytes(Console.ReadLine());
                    encryptbytes = EncodeFunc(msgbytes, key);

                    File.WriteAllText(savepath + @"\decrypted.txt", Encoding.UTF8.GetString(encryptbytes));
                    Console.WriteLine("\n\nDecrypted message: " + Encoding.UTF8.GetString(encryptbytes));

                    break;

                default:
                    Console.WriteLine("Error: Unrecognized command!");
                    break;
            }
        }
        //encoding using two byte arrays with the same length
        static public byte[] EncodeFunc(byte[] msg, byte[] key)
        {
            var encrypted = new byte[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                encrypted[i] = (byte)(msg[i] ^ key[i]);
            }
            return encrypted;
        }
        //generating keys from random bytes for encryption
        static public byte[] KeyGen(int bytelength)
        {
            var key = new byte[bytelength];
            key = RandomNumberGenerator.GetBytes(key.Length);
            return key;
        }
    }
}