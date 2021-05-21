using System;
using System.Security.Cryptography;
using System.Text;

namespace Util
{
    public static class ExtensaoString
    {
        public static string HashSHA256(this string valor)
        {
            var algorithm = new SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(valor);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
