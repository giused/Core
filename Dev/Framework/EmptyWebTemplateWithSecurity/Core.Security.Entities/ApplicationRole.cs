using Microsoft.AspNet.Identity.EntityFramework;

namespace Core.Security.Entities
{
    /// <summary>
    /// Application Role.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        public ApplicationRole() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApplicationRole(string name) : base(name) { }

        #region Extra Properties
        #endregion
    }
}
