using System.Security.Cryptography;
using System.Text;

namespace Hashcoder
{
    public class EncryptionAES
    {
        const int iteraciones = 10;
        const int lenghtKey = 128;
        const string typeAlgotihm = "MD5";
        private static readonly byte[] bytesBase = new byte[16];

        public static string cifrarTextoAES(string text, string key, string Salt)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);

                byte[] dataSalt =  Encoding.UTF8.GetBytes(Salt);

                var desconvert = Encoding.UTF8.GetString(plainTextBytes);

                PasswordDeriveBytes password = new(key, dataSalt, typeAlgotihm, iteraciones);

                byte[] keyBytes = password.GetBytes(lenghtKey / 8);

                using var algorithm = Aes.Create("AesManaged");

                ICryptoTransform? encryptor = algorithm.CreateEncryptor(keyBytes, bytesBase);

                MemoryStream memoryStream = new();

                CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string textoCifradoFinal = Convert.ToBase64String(cipherTextBytes);

                return textoCifradoFinal;
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public static string descifrarTextoAES(string cipher, string key, string Salt)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipher);

                byte[] dataSalt = Encoding.UTF8.GetBytes(Salt);

                PasswordDeriveBytes password = new(key, dataSalt, typeAlgotihm, iteraciones);

                byte[] keyBytes = password.GetBytes(lenghtKey / 8);

                using var algorithm = Aes.Create("AesManaged");

                ICryptoTransform decryptor = algorithm.CreateDecryptor(keyBytes, bytesBase);

                MemoryStream memoryStream = new(cipherTextBytes);

                CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string textoDescifradoFinal = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return textoDescifradoFinal;
            }
            catch
            {
                throw new ArgumentException();
            }
        }
    }
}