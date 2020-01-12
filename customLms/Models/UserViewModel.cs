using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace customLms.Models
{
    public class UserViewModel
    {

        [Display(Name="User Id")]
        public string id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "First name")]
          [StringLength(30,ErrorMessage ="the {0} must be atleast {1}  character long",MinimumLength =3)]
          public string Name { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        [Display(Name ="password")]
        public string password { get; set; }


    }
}