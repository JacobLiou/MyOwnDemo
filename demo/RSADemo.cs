using System.Text;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Demo;

public static class RSADemo
{
    public static void CallDemo()
    {
        RSACryptoServiceProvider rSACryptoService = new RSACryptoServiceProvider();
        var publicKey = rSACryptoService.ToXmlString(false);
        var privateKey = rSACryptoService.ToXmlString(true);

        //2. 加密
        string original = "hello world";
        byte[] originalBytes = Encoding.Default.GetBytes(original);
        byte[] encryptedBytes = rSACryptoService.Encrypt(originalBytes, false);
        string encrypted = Convert.ToBase64String(encryptedBytes);

        //3. 解密
        byte[] decryptedBytes = Convert.FromBase64String(encrypted);
        string decrypted = Encoding.Default.GetString(rSACryptoService.Decrypt(decryptedBytes, false));

        Console.WriteLine("Original: " + original);
        Console.WriteLine("Encrypted: " + encrypted);
        Console.WriteLine("Decrypted: " + decrypted);

    }
}