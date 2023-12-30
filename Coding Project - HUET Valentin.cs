using System;

namespace HUET_Valentin___AES
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.IO;
    class Program
    {      
public class AESExample
        {
            public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }

            public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            public static void Main(string[] args)
            {
                string originalText = "Magic AES !";
                byte[] key = new byte[32]; // 256-bit key
                byte[] iv = new byte[16]; // 128-bit IV

                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(key);
                    rng.GetBytes(iv);
                }

                Console.WriteLine("Results obtained with the AES algorithm : ");
                Console.WriteLine();

                byte[] encrypted = Encrypt(originalText, key, iv);
                Console.WriteLine("Encrypted message : " + Convert.ToBase64String(encrypted));

                string decrypted = Decrypt(encrypted, key, iv);
                Console.WriteLine("Decrypted message : " + decrypted);
            }
        }
    }
}
