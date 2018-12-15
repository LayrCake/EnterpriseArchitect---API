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
    /// Class that represents the UserRoles table in the MySQL Database
    /// </summary>
    public class UserRolesTable
    {
        private IAspNetUserRoleRepository _aspNetUserRoleRepository;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="aspNetUserRepository"></param>
        public UserRolesTable()
        {
            _aspNetUserRoleRepository = new AspNetUserRoleRepository();
        }

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesTable(IAspNetUserRoleRepository aspNetUserRoleRepository)
        {
            _aspNetUserRoleRepository = aspNetUserRoleRepository ?? new AspNetUserRoleRepository();
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(int userId)
        {
            List<string> roles = new List<string>();
            var criteria = new AspNetUserRoleVwmCriteria { 
                Specification = new Specification<AspNetUserRoleVwm>(i => i.AspNetUser_Ref == userId),
                IncludeAspNetRoleRecord = true    
            };
            var userRoles = _aspNetUserRoleRepository.GetList(criteria);

            foreach (var userRole in userRoles)
            {
                roles.Add(userRole.AspNetRole.Name);
            }

            return roles;
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(int userId)
        {
            //Todo: Error is going to happen here ... this won't delete all records
            var userRoles = _aspNetUserRoleRepository.Delete(userId);

            return 1;
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, int roleId)
        {
            var userMapped = AutoMapper.Mapper.Map<AspNetUserVwm>(user);
            var userRole = new AspNetUserRoleVwm
            {
                AspNetRole_Ref = roleId,
                AspNetUser_Ref = userMapped.AspNetUserID
            };
            var userRoles = _aspNetUserRoleRepository.Insert(userRole);

            return 1;
        }
    }
}
