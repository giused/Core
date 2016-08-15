using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Core.Security.Repositories;
using Core.Security.Services;
using Core.Security.Entities;

namespace Web
{
    /// <summary>
    /// Startup class for website.
    /// </summary>
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(AdminDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            this.CreateRolesandUsers();
        }

        /// <summary>
        /// Creates the rolesand users.
        /// </summary>
        private void CreateRolesandUsers()
        {
            AdminDbContext context = new AdminDbContext();

            IdentityManager identityManager = new IdentityManager();
            
            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!identityManager.RoleManager.RoleExists(Core.Security.Roles.Admin))
            {
                // first we create Admin rool
                var role = new ApplicationRole();
                role.Name = Core.Security.Roles.Admin;
                identityManager.RoleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "admin@giuseware.com";
                user.Email = "admin@giuseware.com";

                string userPWD = "P@ssw0rd1";

                IdentityResult userCreationResult = identityManager.UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (userCreationResult.Succeeded)
                {
                    var result1 = identityManager.UserManager.AddToRole(user.Id, Core.Security.Roles.Admin);
                }
            }

            // creating Creating Manager role 
            if (!identityManager.RoleManager.RoleExists(Core.Security.Roles.Manager))
            {
                var role = new ApplicationRole();
                role.Name = Core.Security.Roles.Manager;
                identityManager.RoleManager.Create(role);

            }

            // creating Creating Employee role 
            if (!identityManager.RoleManager.RoleExists(Core.Security.Roles.Employee))
            {
                var role = new ApplicationRole();
                role.Name = Core.Security.Roles.Employee;
                identityManager.RoleManager.Create(role);
            }
        }
    }
}