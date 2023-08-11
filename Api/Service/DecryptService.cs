using System.Security.Cryptography;
using System.Text;
using configuration = Api.Configuration.Configuration;

namespace Api.Service
{
    public class DecryptService : IDecryptService
    {
        private static readonly byte[] bytesBase = new byte[16];

        public string DecryptAES(string message)
        {
            var config = configuration.GetConfigEncryptAES();
            var salt = Encoding.UTF8.GetBytes(config.Salt);
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(message);

                PasswordDeriveBytes password = new(config.Key, salt, config.TypeAlgotihm, config.Interactions);

                byte[] keyBytes = password.GetBytes(config.LenghtKey / 8);

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

        public string DecryptRSA(string message)
        {
            string text;
            try
            {
                var cipherBytes = Convert.FromBase64String(message);

                var path = File.ReadAllText("C:\\Users\\luan_costa\\source\\repos\\Hashcoder\\Api\\Configuration\\privateKeyGenerateKeySecrets.pem");
                //var path = File.ReadAllText("C:\\Users\\luan_costa\\source\\repos\\Hashcoder\\Api\\Configuration\\private_key.pem");
                
                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportFromPem(path);

                    var descryptedData = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);

                    text = Encoding.Unicode.GetString(descryptedData);
                }

                return text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                throw new CryptographicException();
            }
        }
    }
}
