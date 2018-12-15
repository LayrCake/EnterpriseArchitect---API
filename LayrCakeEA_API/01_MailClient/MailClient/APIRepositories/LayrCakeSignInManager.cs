using LayrCake.StaticModel.ViewModelObjects.Implementation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System;
using MailClient.Models;

namespace MailClient.APIRepositories
{
    public class LayrCakeUserManager : UserManager<AspNetUserExtVwm>
    {
        public LayrCakeUserManager(IUserStore<AspNetUserExtVwm> store)
            : base(store)
        { }

        public static LayrCakeUserManager Create(IdentityFactoryOptions<LayrCakeUserManager> options,
            IOwinContext context)
        {
            var manager = new LayrCakeUserManager(new LayrCakeUserStore());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AspNetUserExtVwm>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AspNetUserExtVwm>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

    }

    public class LayrCakeSignInManager : SignInManager<AspNetUserExtVwm, string>
    {

        public LayrCakeSignInManager(LayrCakeUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public async override Task<ClaimsIdentity> CreateUserIdentityAsync(AspNetUserExtVwm user)
        {
            var externalIdentity = await AuthenticationManager.GetExternalIdentityAsync(
                DefaultAuthenticationTypes.ExternalCookie);
            if (externalIdentity != null)
            {
                // ***
                // Copy the claim that our external authentication provider set (in Startup.Auth.cs) over
                // to the user's application identity.

                var googleUserId = externalIdentity.FindFirst(MyClaimTypes.GoogleUserId);

                await ReplaceClaims(user.Id, googleUserId);
            }

            var identity = await user.GenerateUserIdentityAsync((LayrCakeUserManager)UserManager);
            return identity;
        }

        public new Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        { 
            var user = base.UserManager.FindAsync(loginInfo.Login).Result;
            if (user != null)
            {
                SignInAsync(user, isPersistent, rememberBrowser: true);
                return Task.FromResult<SignInStatus>(SignInStatus.Success);
            }
            return base.ExternalSignInAsync(loginInfo, isPersistent);
        }

        public override Task SignInAsync(AspNetUserExtVwm user, bool isPersistent, bool rememberBrowser)
        {
            return base.SignInAsync(user, isPersistent, rememberBrowser);
        }

        private async Task ReplaceClaims(string userId, params Claim[] newClaims)
        {
            var oldClaims = await UserManager.GetClaimsAsync(userId);

            foreach (var newClaim in newClaims.Where(nc => nc != null))
            {
                if (oldClaims.Count > 0)
                    foreach (var oldClaim in oldClaims.Where(oc => oc.Type == newClaim.Type))
                    {
                        await UserManager.RemoveClaimAsync(userId, oldClaim);
                    }

                await UserManager.AddClaimAsync(userId, newClaim);
            }
        }

        public static LayrCakeSignInManager Create(IdentityFactoryOptions<LayrCakeSignInManager> options, IOwinContext context)
        {
            return new LayrCakeSignInManager(context.GetUserManager<LayrCakeUserManager>(), context.Authentication);
        }
    }
}
