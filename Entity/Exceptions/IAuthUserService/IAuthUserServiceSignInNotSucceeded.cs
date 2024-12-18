﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Exceptions.IAuthUserService
{
    public class IAuthUserServiceSignInNotSucceeded : Exception
    {


        public IEnumerable<IdentityError> Errors { get; set; }

        public IAuthUserServiceSignInNotSucceeded(string? message, IEnumerable<IdentityError>? errors = null) : base(message)
        {
            Errors = errors;
        }
    }
}
