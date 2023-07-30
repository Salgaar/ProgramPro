using System.Security.Claims;

namespace ProgramPro.Server.Helpers
{
    public static class UserHelper
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}
