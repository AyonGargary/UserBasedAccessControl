using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace UserBasedAccessControl.Common
{
    [DataContract]
    class Users
    {
        [DataMember (Name ="Id")]
        public int Id {get; set;}

        [DataMember (Name ="Name")]
        public string Name { get; set; }

        [DataMember (Name ="Pwd")]
        public string Pwd { get; set; }

        [DataMember (Name="Roles")]
        public Roles Roles { get; set; }


        public Users(int id, string name, string pwd, Roles roles)
        {
            Id = id;
            Name = name;
            Pwd = pwd;
            Roles = roles;
        }
    }
}
