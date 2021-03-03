using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTubers.Web.Utility
{
    public static class RoleNames
    {
        public const  string User = "User";
        public static readonly string YouTuber = "User";
        public const string Admin = "Admin";
        public static readonly string Mod = "Mod";
        public static readonly string[] Roles = { User, Admin, Mod,YouTuber };
    }
}
