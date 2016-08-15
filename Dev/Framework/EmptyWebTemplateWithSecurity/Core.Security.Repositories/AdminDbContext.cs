using Core.Security.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Core.Security.Repositories
{
    public class AdminDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdminDbContext() : base("AdminConnection", throwIfV1Schema: false)
        {
        }

        public static AdminDbContext Create()
        {
            return new AdminDbContext();
        }
    }
}
