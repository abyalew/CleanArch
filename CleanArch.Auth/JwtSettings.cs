using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Auth
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public byte[] GetSecretByte() { return Encoding.ASCII.GetBytes(Secret); }
    }
}
