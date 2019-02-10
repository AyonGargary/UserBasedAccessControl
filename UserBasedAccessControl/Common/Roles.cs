using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Runtime.Serialization;

namespace UserBasedAccessControl.Common
{
    [DataContract]
    public class Roles
    {

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Permissions")]
        public List<Permission> Permissions { get; set; }

    }
}
