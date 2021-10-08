using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptAndDecrypt
{
    public class AESService
    {
        FileService fileService = new();
        public void Encryptor(string filepath, string password)
        {
            string text = fileService.GetStringFromTextFile(filepath);

            using (FileStream fileStream = new(fileService.GetLocationForEncFileOutTextfile(filepath), FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key = new byte[32];
                    Encoding.UTF8.GetBytes(password).CopyTo(key, 0);

                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new(
                        fileStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new(cryptoStream))
                        {
                            encryptWriter.WriteLine(text);
                        }
                    }
                }
            }

            Console.WriteLine("The file was encrypted.");
        }

        public async Task<string> Decryptor(string filepath, string password)
        {
            string text = fileService.GetStringFromTextFile(filepath);

            try
            {
                using (FileStream fileStream = new(filepath, FileMode.Open))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] iv = new byte[aes.IV.Length];
                        int numBytesToRead = aes.IV.Length;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                            if (n == 0) break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }

                        byte[] key = new byte[32];
                        Encoding.UTF8.GetBytes(password).CopyTo(key, 0);

                        using (CryptoStream cryptoStream = new(
                           fileStream,
                           aes.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                        {
                            using (StreamReader decryptReader = new(cryptoStream))
                            {
                                string decryptedMessage = await decryptReader.ReadToEndAsync();
                                return $"The decrypted original message: {decryptedMessage}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"The decryption failed. {ex}";

            }
        }
    }
}
