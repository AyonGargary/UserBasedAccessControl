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
        public static bool AuthenticateUser(Users user, ResourceManagement resourceManager = ResourceManagement.NoAction, string password = null)
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

            
            return false;
        }

        

    }

}
