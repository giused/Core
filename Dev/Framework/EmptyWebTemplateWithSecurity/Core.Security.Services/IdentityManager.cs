using Core.Security.Entities;
using Core.Security.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Core.Security.Services
{
    public class IdentityManager
    {
        /// <summary>
        /// Gets or sets the role manager.
        /// </summary>
        public RoleManager<IdentityRole> RoleManager { get; set; }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityManager"/> class.
        /// </summary>
        public IdentityManager()
        {
            this.RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AdminDbContext()));
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AdminDbContext()));
        }

        /// <summary>
        /// Used to detect redundant calls.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.RoleManager != null)
                    {
                        this.RoleManager.Dispose();
                    }

                    if (this.UserManager != null)
                    {
                        this.UserManager.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
    }
}
