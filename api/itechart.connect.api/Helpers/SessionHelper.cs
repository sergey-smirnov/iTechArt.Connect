using System;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace itechart.connect.api.Helpers
{
    public class SessionHelper
    {
        public static string GetSessionIdFromOwinContext()
        {
            return GetClaimVlaueFromContext(ClaimTypes.Sid);
        }

        public static string GetUserNameFromOwinContext()
        {
            return GetClaimVlaueFromContext(ClaimTypes.Name);
        }
        public static Guid GetUserDepartmentIdFromOwinContext()
        {
            var currentDepartmentId = GetClaimVlaueFromContext(ClaimTypes.GroupSid);
            if (String.IsNullOrEmpty(currentDepartmentId))
            {
                return Guid.Empty;
            }

            Guid departmentId;
            return Guid.TryParse(currentDepartmentId, out departmentId) ? departmentId : Guid.Empty;
        }

        public static bool IsAuthenticated()
        {
            var owinContext = HttpContext.Current != null ? HttpContext.Current.GetOwinContext() : null;
            return owinContext != null && owinContext.Authentication.User != null &&
                   owinContext.Authentication.User.Identity.IsAuthenticated;
        }

        #region helpers

        private static string GetClaimVlaueFromContext(string claimType)
        {
            var owinContext = HttpContext.Current != null ? HttpContext.Current.GetOwinContext() : null;
            if (owinContext != null)
            {
                var currentUser = owinContext.Authentication.User;
                if (currentUser != null)
                {
                    Claim claim = currentUser.Claims.FirstOrDefault(x => x.Type == claimType);
                    if (claim != null && !String.IsNullOrEmpty(claim.Value))
                    {
                        return claim.Value;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}