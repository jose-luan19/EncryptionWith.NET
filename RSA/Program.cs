﻿using Post;
using RSAClass;

Console.WriteLine();
/*var text = RSAEncryption.descifrarTextoRSA(cipher);
Console.WriteLine(text + "\n");
*/

var password = "Vamos encripitar alguns dados ai";

Console.WriteLine(password + "\n");

var cipher = EncryptionRSA.cifrarTextoRSA(password);
Console.WriteLine(cipher + "\n");

var decrypt = PostMessageEncrypt.PostRSA(cipher);
Console.WriteLine(decrypt);

Console.WriteLine("\nStrings iguais: " + string.Equals(decrypt, password));

Console.ReadLine();


