using System.Security.Cryptography;
using System.Text;

namespace LPPA_Colaiacovo_Services
{
    public static class EncryptionService
    {
        public static string Encriptar(string input)
        {
            // Crear un objeto SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
