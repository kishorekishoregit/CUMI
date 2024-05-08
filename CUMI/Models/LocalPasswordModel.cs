﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CUMI.Models
{
    public class LocalPasswordModel
    {
        [Required, Display(Name = "Current password"), DataType(DataType.Password)]
        public string OldPassword
        {
            get;
            set;
        }
        [Required, Display(Name = "New password"), StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6), DataType(DataType.Password)]
        public string NewPassword
        {
            get;
            set;
        }
        [Display(Name = "Confirm new password"), System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match."), DataType(DataType.Password)]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
