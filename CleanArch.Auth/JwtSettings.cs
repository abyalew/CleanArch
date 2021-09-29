using System.Text;

namespace CleanArch.Auth
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public byte[] GetSecretByte() { return Encoding.ASCII.GetBytes(Secret); }
    }
}
