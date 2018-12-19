using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.Application.Interfaces
{
   public interface IEncryption
    {
        Task<string> Encrypt(string plainText);
    }
}
