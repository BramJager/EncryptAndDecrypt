using System.IO;

namespace EncryptAndDecrypt
{
    public class FileService
    {
        public string GetStringFromTextFile(string filepath) 
        {
            using (var sr = new StreamReader(filepath))
            {
                return sr.ReadToEnd();
            }
        }

        public string GetLocationForEncFileOutTextfile(string originalFilepath) 
        {
            return originalFilepath.Substring(0, originalFilepath.IndexOf(".")) + "Encrypted.txt";
        }

        public void WriteStringToEncFile(string text, string originalFilepath)
        {
            string newFilepath = GetLocationForEncFileOutTextfile(originalFilepath);

            using (StreamWriter writer = new StreamWriter(newFilepath))
            {
                writer.Write(text);
            }
        }
    }
}
