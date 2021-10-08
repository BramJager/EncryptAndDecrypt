using EncryptAndDecrypt;
using System;

namespace EncryptAndDecryptConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var AESService = new AESService();

        MenuStart:
            Console.WriteLine("Do you want to encrypt a text or decrypt an enc file.");
            Console.WriteLine("e for encrypt and d for decrypt.");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.E)
            {
                Console.WriteLine();
                Console.WriteLine("Enter a filepath to a file you want to encrypt.");
                string filepath = Console.ReadLine();
                Console.WriteLine("Enter a password to encrypt the file with");
                string password = Console.ReadLine();

                AESService.Encryptor(filepath, password);
            }
            else if (key.Key == ConsoleKey.D)
            {
                Console.WriteLine();
                Console.WriteLine("Enter a filepath to a file you want to decrypt");
                string filepath = Console.ReadLine();
                Console.WriteLine("Enter a password to decrypt the file");
                string password = Console.ReadLine();

                Console.WriteLine(AESService.Decryptor(filepath, password).Result);
            }
            else 
            {
                goto MenuStart;
            }

        }
    }
}
