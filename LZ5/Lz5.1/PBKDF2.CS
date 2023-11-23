using System;
using System.Security.Cryptography;
using System.Text;

namespace MyPBKDF2
{
    public class PBKDF2Hasher
    {
        private byte[] _passwordBytes;
        private byte[] _saltBytes;
        private byte[] _hashBytes;
        private int _outputLength;
        private HashAlgorithmName _hashAlgorithm;

        public void SetPassword(string password)
        {
            _passwordBytes = Encoding.UTF8.GetBytes(password);
        }

        public void GenerateSalt()
        {
            _saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_saltBytes);
            }
        }

        public void SetHashAlgorithm(string algorithm)
        {
            switch (algorithm.ToLower())
            {
                case "sha1":
                    _hashAlgorithm = HashAlgorithmName.SHA1;
                    _outputLength = 20;
                    break;
                case "sha256":
                    _hashAlgorithm = HashAlgorithmName.SHA256;
                    _outputLength = 32;
                    break;
                case "sha384":
                    _hashAlgorithm = HashAlgorithmName.SHA384;
                    _outputLength = 48;
                    break;
                case "sha512":
                    _hashAlgorithm = HashAlgorithmName.SHA512;
                    _outputLength = 64;
                    break;
                default:
                    _hashAlgorithm = HashAlgorithmName.SHA256;
                    _outputLength = 32;
                    break;
            }
        }

        public void HashData(int iterations)
        {
            using (var rfc = new Rfc2898DeriveBytes(_passwordBytes, _saltBytes, iterations, _hashAlgorithm))
            {
                _hashBytes = rfc.GetBytes(_outputLength);
            }
        }

        public void PrintData()
        {
            Console.WriteLine($"Password: {Encoding.UTF8.GetString(_passwordBytes)}\n" +
                $"Hash: {Convert.ToBase64String(_hashBytes)}\n" +
                $"Salt: {Convert.ToBase64String(_saltBytes)}");
        }
    }
}
