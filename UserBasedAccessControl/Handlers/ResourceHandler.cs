using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UserBasedAccessControl.Common;
using UserBasedAccessControl.Logging;
using UserBasedAccessControl.Utils;
using Newtonsoft.Json;

namespace UserBasedAccessControl.Handlers
{
    class ResourceHandler
    {
        /// <summary>
        /// Gets all Users from JSON
        /// </summary>
        /// <returns></returns>
        private List<Users> GetUsers()
        {
            List<Users> _users = new List<Users>();
            _users = JsonConvert.DeserializeObject<List<Users>>(File.ReadAllText(Constants.UserJSONPath));
            return _users;
        }

        /// <summary>
        /// Get User Object with respect to provided user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Users GetUser(string userName)
        {
            var users = GetUsers();
            foreach (Users user in users)
            {
                if (user.Name.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return user;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns next possible Id
        /// </summary>
        /// <returns></returns>
        private int GetId()
        {
            return GetUsers()[GetUsers().Count - 1].Id++;
        }
        

        #region Roles and Permissions
        // Different Permissions per role type
        private static List<Permission> _adminPermissions = new List<Permission>()
        {
            Permission.Add,
            Permission.Delete,
            Permission.Read,
            Permission.Update
        };

        private static List<Permission> _contributorPermissions = new List<Permission>()
        {
            Permission.Add,
            Permission.Read,
            Permission.Update
        };

        private static List<Permission> _viewerPermissions = new List<Permission>()
        {
            Permission.Read
        };


        /// <summary>
        /// Returns Role object as per user provided role
        /// Role Admin       -> ID = 0, Permissions = Admin Permissions (Add, Delete, Read, Update)
        /// Role Contributor -> ID = 1, Permissions = Contributor Permissions (Add, Read, Update)
        /// Role Viewer      -> ID = 3, Permissions = Viewer Permissions (Read)
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Roles getRoles(string role)
        {
            Roles newRole = new Roles();

            if (Role.Admin.ToString().Equals(role, StringComparison.InvariantCultureIgnoreCase))
            {
                newRole.Name = Role.Admin.ToString();
                newRole.Permissions = _adminPermissions;
            }

            else if (Role.Contributor.ToString().Equals(role, StringComparison.InvariantCultureIgnoreCase))
            {
                newRole.Name = Role.Contributor.ToString();
                newRole.Permissions = _contributorPermissions;
            }

            else if (Role.Viewer.ToString().Equals(role, StringComparison.InvariantCultureIgnoreCase))
            {
                newRole.Name = Role.Viewer.ToString();
                newRole.Permissions = _viewerPermissions;
            }

            else
            {
                Logger.LogError($"Undefined Role Input : {role}");
                return null;
            }

            return newRole;
        }
        #endregion
        
    }
}
