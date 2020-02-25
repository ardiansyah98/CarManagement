using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarManagement.Models
{
    public class SignInRequest
    {
        public string szEmail { get; set; }
        public string szPassword { get; set; }
    }

    public class RegisterModel
    {
        public string szEmail { get; set; }
        public string szFullName { get; set; }
        public string szPassword { get; set; }
        public string szConfirmPassword { get; set; }
    }
}
