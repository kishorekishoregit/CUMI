﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CUMI.Models
{
    public class LoginModel
    {
        [Display(Name = "User name"), Required]
        public string UserName
        {
            get;
            set;
        }
        [DataType(DataType.Password), Display(Name = "Password"), Required]
        public string Password
        {
            get;
            set;
        }
        [Display(Name = "Remember me?")]
        public bool RememberMe
        {
            get;
            set;
        }
    }
}