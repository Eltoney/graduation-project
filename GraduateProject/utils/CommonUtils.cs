using System.Buffers.Text;
using System.Text;
using System.Text.Unicode;
using GraduateProject.models;

namespace GraduateProject.utils;

public static class CommonUtils
{
    private static readonly Random Random = new();

    //To Check the inputs if null or empty
    // @returns true if all values are matching requirements else false
    public static bool CheckStrings(params string?[] values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value)) return false;
        }

        return true;
    }

    public static string CreateTokenSession(User user)
    {
        long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        string s = currentTime + user.userID + user.UserName + CreateRandomString(5);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
    }


    public static string CreateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_asbdefghijklmnoprstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}