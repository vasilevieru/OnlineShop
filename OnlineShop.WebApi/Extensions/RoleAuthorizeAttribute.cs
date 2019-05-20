using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace OnlineShop.WebApi.Extensions
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        /// <param name="roles">Roles which are allowed to access controller/action</param>
        public RoleAuthorizeAttribute(params string[] roles)
        {
            Roles = roles.Join(",");
        }
    }
}
