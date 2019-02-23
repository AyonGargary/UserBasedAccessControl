using System;
using System.Collections.Generic;
using System.Text;
using UserBasedAccessControl.Handlers;

namespace UserBasedAccessControl
{
    class Login
    {
        public static string CurrentUser { get; set; }
        ResourceHandler resourceHandler = new ResourceHandler();
        public Login(string userName, string password)
        {
            if (Authentication.AuthenticateUser(resourceHandler.GetUser(userName), Common.ResourceManagement.NoAction, password))
                CurrentUser = userName;
            else
                CurrentUser = null;
        }
    }
}
