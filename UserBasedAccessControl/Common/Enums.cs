using System;
using System.Collections.Generic;
using System.Text;

namespace UserBasedAccessControl.Common
{
    public enum Permission
    {
        Read,
        Update,
        Add,
        Delete
    }

    public enum Role
    {
        Admin,
        Contributor,
        Viewer
    }

    public enum ResourceManagement
    {
        NoAction,
        Login,
        AddUser,
        UpdateUser,
        RemoveUser
    }
}
