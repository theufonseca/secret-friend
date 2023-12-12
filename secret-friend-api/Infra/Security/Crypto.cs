using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Security
{
    public static class Crypto
    {
        public static string CriptografarSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
                byte[] senhaHash = sha256.ComputeHash(senhaBytes);

                return BitConverter.ToString(senhaHash).Replace("-", "").ToLower();
            }
        }
    }
}
