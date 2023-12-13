using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Security
{
    public class Crypto : ISecurity
    {
        public string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] senhaBytes = Encoding.UTF8.GetBytes(password);
                byte[] senhaHash = sha256.ComputeHash(senhaBytes);

                return BitConverter.ToString(senhaHash).Replace("-", "").ToLower();
            }
        }
    }
}
