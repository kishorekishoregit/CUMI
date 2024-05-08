using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUMI.Models
{
   // [Table("UserProfile")]
    public class UserProfile
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
    }
}
