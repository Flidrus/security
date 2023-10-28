using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz5_1
{
	internal static class SaltedHash
	{
		public static byte[] SaltGenerator()
		{
			return RandomNumberGenerator.GetBytes(32);
		}

		public static byte[] Hash(byte[] password, byte[] salt)
		{
			return SHA256.HashData(password.Concat(salt).ToArray());
		}
	}
}


