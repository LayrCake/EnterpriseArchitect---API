using CodeGenerator.StaticModel.Criteria.Implementation;
using CodeGenerator.StaticModel.Repositories.Abstract;
using CodeGenerator.StaticModel.Repositories.Implementation;
using CodeGenerator.StaticModel.ViewModelObjects.Implementation;
using NCommon.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace MailClient
{
    /// <summary>
    /// Class that represents the Users table in the MySQL Database
    /// </summary>
    public class UserTable<TUser>
        where TUser : IdentityUser
    {
        private IAspNetUserRepository _aspNetUserRepository;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="aspNetUserRepository"></param>
        public UserTable()
        {
            _aspNetUserRepository = new AspNetUserRepository();
        }

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="aspNetUserRepository"></param>
        public UserTable(IAspNetUserRepository aspNetUserRepository)
        {
            _aspNetUserRepository = aspNetUserRepository ?? new AspNetUserRepository();
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserId(string userId)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.UserID == userId) };
            var user = _aspNetUserRepository.Get(criteria);

            return user != null ? user.UserName : string.Empty;
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserName(string userName)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.UserName == userName) };
            var user = _aspNetUserRepository.Get(criteria);

            return user != null ? user.UserName : string.Empty;
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public TUser GetUserById(string userId)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.UserID == userId) };
            var user = _aspNetUserRepository.Get(criteria);
            var userMapped = AutoMapper.Mapper.Map<TUser>(user);

            return userMapped;
        }

        /// <summary>
        /// Returns a list of TUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string userName)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.UserName == userName) };
            var user = _aspNetUserRepository.GetList(criteria);
            var listUserMapped = new List<TUser>();
            var mapped = AutoMapper.Mapper.Map<List<TUser>>(user);

            foreach(var userItem in user)
                listUserMapped.Add(AutoMapper.Mapper.Map<TUser>(userItem));

            return listUserMapped;
        }

        public List<TUser> GetUserByEmail(string email)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.Email == email) };
            var user = _aspNetUserRepository.GetList(criteria);
            var listUserMapped = new List<TUser>();
            var mapped = AutoMapper.Mapper.Map<List<TUser>>(user);

            foreach (var userItem in user)
                listUserMapped.Add(AutoMapper.Mapper.Map<TUser>(userItem));

            return listUserMapped;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(int userId)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.AspNetUserID == userId) };
            var user = _aspNetUserRepository.GetList(criteria).FirstOrDefault();

            return user != null ? user.PasswordHash : null;
        }


        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public int SetPasswordHash(int userId, string passwordHash)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.AspNetUserID == userId) };
            var user = _aspNetUserRepository.Get(criteria);

            if (user == null)
                return -1;
            user.PasswordHash = passwordHash;
           var userUpdate =  _aspNetUserRepository.Update(user);

           return userUpdate.AspNetUserID;
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(int userId)
        {
            var criteria = new AspNetUserVwmCriteria { Specification = new Specification<AspNetUserVwm>(i => i.AspNetUserID == userId) };
            var user = _aspNetUserRepository.GetList(criteria).FirstOrDefault();

            return user != null ? user.SecurityStamp : null;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(TUser user)
        {
            var mappedInput = AutoMapper.Mapper.Map<AspNetUserVwm>(user);
            var output = _aspNetUserRepository.Insert(mappedInput);
            var mappedOutput = AutoMapper.Mapper.Map<TUser>(output);

            var mapped = AutoMapper.Mapper.Map<List<TUser>>(user);

            return 1;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private int Delete(int userId)
        {
            var output = _aspNetUserRepository.Delete(userId);

            return 1;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(TUser user)
        {
            var mappedInput = AutoMapper.Mapper.Map<AspNetUserVwm>(user);
            var output = _aspNetUserRepository.Delete(mappedInput);
            return 1;
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(TUser user)
        {
            var mappedInput = AutoMapper.Mapper.Map<AspNetUserVwm>(user);
            var output = _aspNetUserRepository.Update(mappedInput);
            var mappedOutput = AutoMapper.Mapper.Map<TUser>(output);

            return 1;
        }
    }
}
