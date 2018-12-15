using LayrCake.StaticModel.ViewModelObjects.Implementation;
using MailClient.APIRepositories;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MailClient.Models
{
    public partial class AspNetUserExtVwm : AspNetUserVwm, IUser<string>
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public AspNetUserExtVwm()
        { 
            UserGUID = String.IsNullOrEmpty(UserGUID) ? Guid.NewGuid().ToString() : UserGUID;
        }

        public string Id
        {
            get { return base.UserGUID; }
        }

        public new string UserName
        {
            get
            {
                return base.UserName;
            }
            set
            {
                base.UserName = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(LayrCakeUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(LayrCakeUserManager manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
