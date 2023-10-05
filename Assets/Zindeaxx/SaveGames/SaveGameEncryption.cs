using System.Security.Cryptography;
using System.Text;

namespace Zindeaxx.Saves
{
    public static class SaveGameEncryption
    {
        private const int BlockSize = 128;
        private static readonly byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        public static byte[] Encrypt(string input, string key)
        {
            using (var crypt = Aes.Create())
            {
                var hash = MD5.Create();
                crypt.BlockSize = BlockSize;
                crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
                crypt.IV = IV;

                var memoryStream = new System.IO.MemoryStream();

                using (var cryptoStream = new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] encryptedBytes = memoryStream.ToArray();
                    return encryptedBytes;
                }
            }
        }

        public static string Decrypt(byte[] input, string key)
        {
            using (var crypt = Aes.Create())
            {
                var hash = MD5.Create();
                crypt.BlockSize = BlockSize;
                crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
                crypt.IV = IV;

                var memoryStream = new System.IO.MemoryStream(input);

                using (var cryptoStream = new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] decryptedBytes = new byte[input.Length];
                    cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                    string decryptedString = Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
                    return decryptedString;
                }
            }
        }
    }
}
