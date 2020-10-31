using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserIdSession(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.FindFirstValue("id");

        public static string UserIDSession(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.FindFirstValue("userID");

        public static string UserNameSession(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.FindFirstValue("userName");
    }
}