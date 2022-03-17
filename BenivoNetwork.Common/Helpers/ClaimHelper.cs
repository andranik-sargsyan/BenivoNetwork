using BenivoNetwork.Common.Enums;
using System;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BenivoNetwork.Common.Helpers
{
    public class ClaimHelper : DynamicObject
    {
        public static int ID => int.Parse(GetClaim("ID"));
        public static RoleEnum Role => (RoleEnum)Enum.Parse(typeof(RoleEnum), GetClaim("Role"));

        public static string GetClaim(string type)
        {
            if (HttpContext.Current.User == null)
            {
                return string.Empty;
            }

            var user = HttpContext.Current.User as ClaimsPrincipal;

            var claim = user.Claims.FirstOrDefault(c => c.Type == type);

            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }

        public string this[string type]
        {
            get
            {
                return GetClaim(type);
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetClaim(binder.Name);

            return result != null;
        }
    }
}