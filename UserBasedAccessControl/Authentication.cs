using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using UserBasedAccessControl.Common;
using UserBasedAccessControl.Logging;

namespace UserBasedAccessControl
{
    class Authentication
    {
        public static bool authenticateUser(Users user, ResourceManagement resourceManager = ResourceManagement.NoAction, string password = null)
        {
            Handlers.ResourceHandler resource = new Handlers.ResourceHandler();

            if(resourceManager == ResourceManagement.NoAction)
            {
                if(resource.GetUser(user.Name).Name == null)
                {
                    Logger.LogError("User Name not available");
                    return false;
                }
                if(user.Name == resource.GetUser(user.Name).Name && user.Pwd == password)
                {
                    Logger.LogInfo("Logged In Successfully");
                    return true;
                }
                return false;
            }

            Users currentUser = resource.GetUser(Login.CurrentUser);

            if (resourceManager == ResourceManagement.AddUser || resourceManager == ResourceManagement.UpdateUser)
            {
                if (currentUser.Roles == resource.getRoles(Role.Admin.ToString()))
                {
                    return true;
                }
                else if (currentUser.Roles == resource.getRoles(Role.Contributor.ToString()) && user.Roles != resource.getRoles(Role.Admin.ToString()))
                {
                    return true;
                }
                else if (currentUser.Roles == resource.getRoles(Role.Contributor.ToString()) && user.Roles == resource.getRoles(Role.Admin.ToString()))
                {
                    Logger.LogError("Contributors can't add admin");
                    return false;
                }
                else if (currentUser.Roles == resource.getRoles(Role.Viewer.ToString()))
                {
                    Logger.LogError("Current User doesn't have enough permission to add User");
                    return false;
                }
            }
            if(resourceManager == ResourceManagement.RemoveUser)
            {
                if(currentUser.Roles == resource.getRoles(Role.Admin.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        

    }

}
