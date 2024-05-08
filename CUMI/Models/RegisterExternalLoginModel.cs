using System;
using System.ComponentModel.DataAnnotations;

namespace CUMI.Models
{
    public class RegisterExternalLoginModel
    {
        [Display(Name = "User name"), Required]
        public string UserName
        {
            get;
            set;
        }
        public string ExternalLoginData
        {
            get;
            set;
        }
    }
}
