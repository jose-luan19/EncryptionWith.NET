using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RSAClass
{
    public static class EncryptionRSA
    {
        public static string cifrarTextoRSA(string text)
        {

            var certificate = new X509Certificate2("C:\\Users\\luan_costa\\source\\repos\\Hashcoder\\RSA\\certificate.cer");

            string cipher;
            try
            {
                var textBytes = Encoding.Unicode.GetBytes(text);

                var rsaCertPublic = certificate.GetRSAPublicKey();

                var encryptedData = rsaCertPublic.Encrypt(textBytes, RSAEncryptionPadding.OaepSHA256);

                cipher = Convert.ToBase64String(encryptedData);

                return cipher;

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                throw new CryptographicException();
            }
        }

        public static string descifrarTextoRSA(string cipher)
        {
            string text;
            try
            {
                var cipherBytes = Convert.FromBase64String(cipher);

                var path = File.ReadAllText("C:\\Users\\luan_costa\\keys\\privateKeyGenerateKeySecrets.pem");

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
