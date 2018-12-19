using Bit.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bit.Application.Managers
{
   public class Encryption : IEncryption
    {
        public async Task<string> Encrypt(string plainText)
        {
            string result = string.Empty;

            await Task.Run(() =>
            {
                using (MD5 md5 = MD5.Create())
                {
                    result = md5.ComputeHash(Encoding.ASCII.GetBytes(plainText))
                              .Select(x => x.ToString("X2"))
                              .Aggregate((ag, s) => ag + s);
                }
            });

            return result;
        }
    }
}
