﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions
{
    public class IAuthRoleServiceSetRoleForUserPostNotSucceeded : Exception
    {
        public IAuthRoleServiceSetRoleForUserPostNotSucceeded(string? message) : base(message)
        {
        }
    }
}