using System.Security.Cryptography;
using System.Text;

namespace GraduateProject.utils;

public class CryptUtils
{


    public static string? StringToSHA256(string? text)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256.Create()) {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(text));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }
}