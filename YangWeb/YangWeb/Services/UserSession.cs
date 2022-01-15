using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangWeb.Models;

namespace YangWeb.Services
{
    public static class UserSession
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static ISession _session => _httpContextAccessor.HttpContext.Session;

   
        public static void SetAccessor(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public static IHttpContextAccessor GetAccesor()
        {
            return _httpContextAccessor;
        }

        public static void SetUserSession(string username)
        {
            _session.SetString("UserName", username);
        }

        public static string GetUserSession()
        {
            return _session.GetString("UserName");
        }
    }
}
