using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Entities.ErrorModels;

public class UserLoginFaildException : AuthFaildException
{
    public UserLoginFaildException()
        : base($"Username/Password was incorrect.") { }
}