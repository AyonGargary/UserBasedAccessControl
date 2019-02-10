using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UserBasedAccessControl.Utils
{
    class Constants
    {
        public static readonly string LogPath = Path.Combine(Path.GetTempPath(),"UBAC.txt");
        public static readonly string UserJSONPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Users.json");
        public static readonly string ProductJSONPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Product.json");
        
    }
    
}
