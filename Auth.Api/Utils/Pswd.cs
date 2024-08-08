
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Utils;

public static class Pswd
{
    private static readonly PasswordHasher<string> m_PasswordHasher = new PasswordHasher<string>();

    public static string Hash(string username, string password) => m_PasswordHasher.HashPassword(username, password);

    public static bool Verify(string username, string hashedPassword, string providedPassword)
    {
        PasswordVerificationResult pvr = m_PasswordHasher.VerifyHashedPassword(username, hashedPassword, providedPassword);
        return pvr is not PasswordVerificationResult.Failed;
    }
}
