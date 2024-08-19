using System.Security.Cryptography;
using System.Text;

namespace Mahali.Security
{
    public class MD5Hasher
    {
        public static string ComputeHash(string data)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes = md5.ComputeHash(dataBytes);
                
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
