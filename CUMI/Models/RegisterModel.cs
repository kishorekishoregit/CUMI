using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace CUMI.Models
{
    public class RegisterModel
    {
        [Required, Display(Name = "User name")]
        public string UserName
        {
            get;
            set;
        }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6), Display(Name = "Password"), Required, DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        [DataType(DataType.Password), Display(Name = "Confirm password"), System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
