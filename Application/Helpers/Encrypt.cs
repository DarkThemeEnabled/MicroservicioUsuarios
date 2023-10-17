using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers
{
    public class Encrypt
    {
        public static string GetSHA256(string input)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(enconding.GetBytes(input));

            for (int i = 0; i < stream.Length; i++) { sb.AppendFormat("{0:x2}", stream[i]); };

            return sb.ToString();
        }
    }
}