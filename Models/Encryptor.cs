using System.Security.Cryptography;
using System.Text;

namespace BankSystem.Models
{
    public class Encryptor
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = MD5.Create();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // Change to hexadecimal for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
