using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.ModelMapper;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using MailClient.Mapping;
using MailClient.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NCommon.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mapper = AutoMapper;

namespace MailClient.APIRepositories
{
    public class LayrCakeUserStore : IUserLoginStore<AspNetUserExtVwm>,
        IUserClaimStore<AspNetUserExtVwm>,
        IUserRoleStore<AspNetUserExtVwm>,
        IUserPasswordStore<AspNetUserExtVwm>,
        IUserSecurityStampStore<AspNetUserExtVwm>,
        IQueryableUserStore<AspNetUserExtVwm>,
        IUserEmailStore<AspNetUserExtVwm>,
        IUserPhoneNumberStore<AspNetUserExtVwm>,
        IUserTwoFactorStore<AspNetUserExtVwm, string>,
        IUserLockoutStore<AspNetUserExtVwm, string>,
        IUserStore<AspNetUserExtVwm>
        //where TUser : AspNetUserExtVwm
    {
        public IAspNetRoleRepository _aspNetRoleRepository;
        public IAspNetUserClaimRepository _aspNetUserClaimRepository;
        public IAspNetUserLoginRepository _aspNetUserLoginRepository;
        public IAspNetUserRepository _aspNetUserRepository;
        public AspNetUserRoleRepository _aspNetUserRoleRepository;

        public LayrCakeUserStore()
        {
            _aspNetUserRepository = new AspNetUserRepository();
            _aspNetUserLoginRepository = new AspNetUserLoginRepository();
            _aspNetRoleRepository = new AspNetRoleRepository();
            _aspNetUserClaimRepository = new AspNetUserClaimRepository();
            _aspNetUserRoleRepository = new AspNetUserRoleRepository();
            _cacheUser = null;
            MailClientSetup.RegisterMappings();
        }

        public LayrCakeUserStore(IAspNetUserRepository aspNetUserRepository, IAspNetUserLoginRepository aspNetUserLoginRepository,
            IAspNetUserClaimRepository aspNetUserClaimRepository, IAspNetRoleRepository aspNetRoleRepository,
            AspNetUserRoleRepository aspNetUserRoleRepository)
        {
            _aspNetUserRepository = aspNetUserRepository ?? new AspNetUserRepository();
            _aspNetUserLoginRepository = aspNetUserLoginRepository ?? new AspNetUserLoginRepository();
            _aspNetRoleRepository = _aspNetRoleRepository ?? new AspNetRoleRepository();
            _aspNetUserClaimRepository = _aspNetUserClaimRepository ?? new AspNetUserClaimRepository();
            _aspNetUserRoleRepository = aspNetUserRoleRepository ?? new AspNetUserRoleRepository();
            _cacheUser = null;
            MailClientSetup.RegisterMappings();
        }

        private AspNetUserExtVwm _cacheUser = null;

        public Task CreateAsync(AspNetUserExtVwm user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            // Set Default Values for a User Insert
            user = UserCreateSetDefaults(user);

            var returnValue = _aspNetUserRepository.Insert(user);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        private Task<AspNetUserExtVwm> CreateUserAsync(AspNetUserExtVwm user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user = UserCreateSetDefaults(user);

            var returnValue = _aspNetUserRepository.Insert(user);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        private AspNetUserExtVwm UserCreateSetDefaults(AspNetUserExtVwm user)
        {
            user.EmailConfirmed = false;
            user.PhoneNumberConfirmed = false;
            user.TwoFactorEnabled = false;
            user.LockoutEnabled = true;
            user.AccessFailedCount = 0;
            var guid = Guid.NewGuid().ToString();
            user.SecurityStamp = guid;
            user.UserAuthCode = guid;
            user.UserGUID = guid;
            user.MobileId = guid;

            return user;
        }

        public Task<AspNetUserExtVwm> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Null or empty argument: userId");

            var criteria = new AspNetUserVwmCriteria {
                Specification = new Specification<AspNetUserVwm>(i => i.UserGUID == userId)
            };

            var returnValue = _aspNetUserRepository.Get(criteria);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        private Task<AspNetUserExtVwm> FindOrCreateAsync(AspNetUserExtVwm user)
        {
            if (user == null || user.AspNetUserID < 1 || string.IsNullOrEmpty(user.PasswordHash))
                return Task.FromResult(user);

            if (_cacheUser != null)
                return Task.FromResult(_cacheUser);

            var userFound = FindByNameAsync(user.UserName).Result;
            if (userFound != null && userFound.AspNetUserID > 0)
            {
                _cacheUser = userFound;
                return Task.FromResult(userFound);
            }
            if (userFound == null || userFound.AspNetUserID < 0)
                user = CreateUserAsync(user).Result;
            return Task.FromResult(user);
        }

        //public Task<AspNetUserExtVwm> FindByIdAsync(int userId)
        //{
        //    if (userId <= 0)
        //        throw new ArgumentException("Null or empty argument: userId");

        //    var criteria = new AspNetUserVwmCriteria {
        //        Specification = new Specification<AspNetUserVwm>(i => i.AspNetUserID == userId)
        //    };

        //    var returnValue = _aspNetUserRepository.Get(criteria);
        //    var aspNetUserExtVwm = new AspNetUserExtVwm();
        //    aspNetUserExtVwm = AutoMapper.Mapper.Map(returnValue, aspNetUserExtVwm);
        //    return Task.FromResult<AspNetUserExtVwm>(aspNetUserExtVwm);
        //}
        public Task<AspNetUserExtVwm> FindAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Null or empty argument: userName");
            
            var criteria = new AspNetUserVwmCriteria
            {
                Specification = new Specification<AspNetUserVwm>(i => i.UserName == userName && i.PasswordHash == password)
            };

            var returnValue = _aspNetUserRepository.Get(criteria);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        public Task<AspNetUserExtVwm> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Null or empty argument: userName");

            var criteria = new AspNetUserVwmCriteria {
                Specification = new Specification<AspNetUserVwm>(i => i.UserName == userName)
            };

            var returnValue = _aspNetUserRepository.Get(criteria);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        public Task UpdateAsync(AspNetUserExtVwm user)
        {
            return Task.FromResult(UpdateUserAsync(user));
        }

        public Task<AspNetUserExtVwm> UpdateUserAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var returnValue = _aspNetUserRepository.Update(user);
            var aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, new AspNetUserExtVwm());
            return Task.FromResult(aspNetUserExtVwm);
        }


        public Task DeleteAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var returnValue = _aspNetUserRepository.Delete(user);
            return Task.FromResult<object>(returnValue);
        }

        public void Dispose()
        {
            if (_cacheUser != null)
            {
                _cacheUser = null;
            }
        }

        public Task AddLoginAsync(AspNetUserExtVwm user, UserLoginInfo login)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (login == null) throw new ArgumentNullException(nameof(login));

            user = FindOrCreateAsync(user).Result;
            var aspNetUserLogin = new AspNetUserLoginVwm
            {
                AspNetUser_Ref = user.AspNetUserID,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
            };
            AspNetUserLoginVwm returnValue;
            var loginProvider = _aspNetUserLoginRepository.Get(user.AspNetUserID);
            if (loginProvider != null)
                returnValue = loginProvider;
            else
                returnValue = _aspNetUserLoginRepository.Insert(aspNetUserLogin);
            return Task.FromResult(returnValue);
        }

        public Task<AspNetUserExtVwm> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var criteria = new AspNetUserLoginVwmCriteria {
                Specification = new Specification<AspNetUserLoginVwm>(i => i.LoginProvider == login.LoginProvider && i.ProviderKey == login.ProviderKey),
                // Todo: IncludeAspNetUserRecord = true
            };

            var returnValue = _aspNetUserLoginRepository.Get(criteria);
            if (returnValue == null)
                return Task.FromResult<AspNetUserExtVwm>(null);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue.AspNetUser, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var criteria = new AspNetUserLoginVwmCriteria {
                Specification = new Specification<AspNetUserLoginVwm>(i => i.AspNetUser_Ref == user.AspNetUserID)
            };
            var loginList = new List<UserLoginInfo>();
            var returnValue = _aspNetUserLoginRepository.GetList(criteria);
            foreach (var login in returnValue)
                loginList.Add(new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            return Task.FromResult<IList<UserLoginInfo>>(loginList);
        }

        public Task RemoveLoginAsync(AspNetUserExtVwm user, UserLoginInfo login)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (login == null)
                throw new ArgumentNullException("login");

            var criteria = new AspNetUserLoginVwmCriteria
            {
                Specification = new Specification<AspNetUserLoginVwm>(i => i.AspNetUser_Ref == user.AspNetUserID
                    && i.LoginProvider == login.LoginProvider && i.ProviderKey == login.ProviderKey)
            };

            var returnValue = _aspNetUserLoginRepository.Delete(criteria);
            return Task.FromResult<AspNetUserExtVwm>(null);
        }

        public Task<AspNetUserExtVwm> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Null or empty argument: email");

            var criteria = new AspNetUserVwmCriteria  {
                Specification = new Specification<AspNetUserVwm>(i => i.Email == email)
            };

            var returnValue = _aspNetUserRepository.Get(criteria);
            var aspNetUserExtVwm = new AspNetUserExtVwm();
            aspNetUserExtVwm = Mapper.Mapper.Map(returnValue, aspNetUserExtVwm);
            return Task.FromResult(aspNetUserExtVwm);
        }

        public Task<string> GetEmailAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(AspNetUserExtVwm user, string email)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
            user.Email = email;
            return Task.FromResult<object>(null);
        }

        public Task SetEmailConfirmedAsync(AspNetUserExtVwm user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (!confirmed) throw new ArgumentNullException(nameof(confirmed));
            user.EmailConfirmed = confirmed;
            return Task.FromResult<object>(null);
        }


        public Task AddClaimAsync(AspNetUserExtVwm user, Claim claim)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (claim == null)
                throw new ArgumentNullException("claim");
            user = FindOrCreateAsync(user).Result;

            var userClaim = new AspNetUserClaimVwm
            {
                Type = claim.Type,
                Value = claim.Value,
                ValueType = claim.ValueType,
                Issuer = claim.Issuer,
                OriginalIssuer = claim.OriginalIssuer,
                AspNetUser_Ref = user.AspNetUserID
            };

            var returnValue = _aspNetUserClaimRepository.Insert(userClaim);
            return Task.FromResult<object>(null);
        }

        public Task<IList<Claim>> GetClaimsAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var criteria = new AspNetUserClaimVwmCriteria {
                Specification = new Specification<AspNetUserClaimVwm>(i => i.AspNetUser_Ref == user.AspNetUserID)
            };

            user = FindOrCreateAsync(user).Result;

            var returnValue = _aspNetUserClaimRepository.GetList(criteria);
            var claimList = new List<Claim>();

            if (returnValue == null)
                return Task.FromResult<IList<Claim>>(claimList);

            foreach(var claim in returnValue) {
                var userClaim = new Claim(claim.Type, claim.Value, claim.ValueType, claim.Issuer, claim.OriginalIssuer);
                claimList.Add(userClaim);
            }

            return Task.FromResult<IList<Claim>>(claimList);
        }

        public Task RemoveClaimAsync(AspNetUserExtVwm user, Claim claim)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (claim == null)
                throw new ArgumentNullException("claim");

            var criteria = new AspNetUserClaimVwmCriteria
            {
                Specification = new Specification<AspNetUserClaimVwm>(i => i.AspNetUser_Ref == user.AspNetUserID
                    && i.Type == claim.Type && i.Value == claim.Value && i.Issuer == claim.Issuer)
            };
            var returnGet = _aspNetUserClaimRepository.Get(criteria);
            var returnValue = _aspNetUserClaimRepository.Delete(returnGet);
            if (returnValue != null)
                throw new ArgumentNullException("delete");
            return Task.FromResult<object>(returnValue);
        }

        public Task AddToRoleAsync(AspNetUserExtVwm user, string roleName)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var criteria = new AspNetUserClaimVwmCriteria
            {
                Specification = new Specification<AspNetUserClaimVwm>(i => i.AspNetUser_Ref == user.AspNetUserID)
            };

            user = FindOrCreateAsync(user).Result;

            var returnValue = _aspNetUserClaimRepository.GetList(criteria);
            var claimList = new List<Claim>();

            if (returnValue == null)
                return Task.FromResult<IList<Claim>>(claimList);

            foreach (var claim in returnValue)
            {
                var userClaim = new Claim(claim.Type, claim.Value, claim.ValueType, claim.Issuer, claim.OriginalIssuer);
                claimList.Add(userClaim);
            }

            return Task.FromResult<IList<Claim>>(claimList);
        }

        public Task<IList<string>> GetRolesAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var returnValue = GetUserRolesAsync(user).Result;
            var roleList = new List<string>();

            if (returnValue == null)
                return Task.FromResult<IList<string>>(roleList);

            foreach (var role in returnValue)
            {
                //Todo:  if (role.AspNetRole != null)
                //Todo:     roleList.Add(role.AspNetRole.Name);
            }

            return Task.FromResult<IList<string>>(roleList);
        }

        private Task<List<AspNetUserRoleVwm>> GetUserRolesAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user = FindOrCreateAsync(user).Result;

            var criteria = new AspNetUserRoleVwmCriteria {
                Specification = new Specification<AspNetUserRoleVwm>(i => i.AspNetUser_Ref == user.AspNetUserID),
                //Todo: IncludeAspNetRoleRecord = true
            };

            var returnValue = _aspNetUserRoleRepository.GetList(criteria);

            return Task.FromResult<List<AspNetUserRoleVwm>>(returnValue);
        }

        public Task<bool> IsInRoleAsync(AspNetUserExtVwm user, string roleName)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var roles = GetRolesAsync(user).Result;
            if (roles == null || roles.Count < 1 || roles.Contains(roleName))
                return Task.FromResult<bool>(false);
            return Task.FromResult<bool>(true);
        }

        public Task RemoveFromRoleAsync(AspNetUserExtVwm user, string roleName)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var getRoles = GetUserRolesAsync(user).Result;
            var roleDelete = -1;
            if (getRoles.Any())
                foreach (var role in getRoles) { }
                    //Todo: if (role.AspNetRole != null)
                    //    if (role.AspNetRole.Name == roleName)
                    //        roleDelete = role.AspNetRole.AspNetRoleID;
            var returnValue = _aspNetUserRoleRepository.Delete(roleDelete);
            return Task.FromResult<Object>(null);
        }

        public Task<string> GetPasswordHashAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(AspNetUserExtVwm user, string passwordHash)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(passwordHash)) throw new ArgumentNullException(nameof(passwordHash));
            user.PasswordHash = passwordHash;
            return Task.FromResult<Object>(null);
        }

        public Task<string> GetSecurityStampAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(AspNetUserExtVwm user, string stamp)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(stamp)) throw new ArgumentNullException(nameof(stamp));
            return Task.FromResult<Object>(null);
        }

        public IQueryable<AspNetUserExtVwm> Users
        {
            get {
                var returnValue = _aspNetUserRepository.GetList();
                var usersList = new List<AspNetUserExtVwm>();
                foreach (var user in returnValue)
                {
                    var aspNetUserExtVwm = new AspNetUserExtVwm();
                    usersList.Add(Mapper.Mapper.Map(user, aspNetUserExtVwm));
                }
                return usersList.AsQueryable();
            }
        }

        public Task<string> GetPhoneNumberAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(AspNetUserExtVwm user, string phoneNumber)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));
            user.PhoneNumber = phoneNumber;
            return Task.FromResult<Object>(null);
        }

        public Task SetPhoneNumberConfirmedAsync(AspNetUserExtVwm user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult<Object>(null);
        }

        public Task<bool> GetTwoFactorEnabledAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(AspNetUserExtVwm user, bool enabled)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<int> GetAccessFailedCountAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult((DateTimeOffset)user.LockoutEndDateUtc);
        }

        public Task<int> IncrementAccessFailedCountAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.AccessFailedCount = user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(AspNetUserExtVwm user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.AccessFailedCount = 0;
            return Task.FromResult<Object>(null);
        }

        public Task SetLockoutEnabledAsync(AspNetUserExtVwm user, bool enabled)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.LockoutEnabled = enabled;
            return Task.FromResult<Object>(null);
        }

        public Task SetLockoutEndDateAsync(AspNetUserExtVwm user, DateTimeOffset lockoutEnd)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.LockoutEndDateUtc = lockoutEnd.LocalDateTime;
            return Task.FromResult<Object>(null);
        }
    }
}
