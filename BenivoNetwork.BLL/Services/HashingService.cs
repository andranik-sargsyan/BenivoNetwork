using System.Security.Cryptography;
using System.Text;

namespace BenivoNetwork.BLL.Services
{
    public class HashingService : IHashingService
    {
        public string GetHash(string input)
        {
            string inputHash = default;

            using (var alg = SHA256.Create())
            {
                var arr = alg.ComputeHash(Encoding.UTF8.GetBytes(input));

                var builder = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    builder.Append(arr[i].ToString("x2"));
                }

                inputHash = builder.ToString();
            }

            return inputHash;
        }
    }
}
