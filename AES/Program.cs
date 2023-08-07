using AES.Encryption;
using AES.Post;

Console.WriteLine();
/*var passwordDesHashAes = CriptyAES.descifrarTextoAES(passwordHashAes, key, saltText);
Console.WriteLine(passwordDesHashAes);*/

var password = "Vamos encripitar alguns dados ai";
var key = "chaveparaencriptar";
var saltText = "SaLtParAjudaREncRipitAr";

Console.WriteLine(password + "\n");

var passwordHashAes = EncryptionAES.cifrarTextoAES(password, key, saltText);
Console.WriteLine(passwordHashAes+"\n");

var decrypt = PostMessageEncrypt.PostAES(passwordHashAes);
Console.WriteLine(decrypt+"\n");


Console.WriteLine("Strings iguais: " + string.Equals(decrypt, password));

Console.ReadLine();
