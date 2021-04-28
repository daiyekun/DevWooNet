using Dev.WooNet.Model.DevDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.WooNet.WebAPI.Utility
{
    public interface ICustomJWTService
    {
        string GetToken(string UserName, string password, LoginUser user);
    }
}
