using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Web.Admin.Models
{
   public class LoginModel
    {
       [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "UserNameRequired")]
       [Display(ResourceType = typeof(DisplayResource), Name = "UserName")]
       public string UserName { get; set; }

       [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "PasswordRequired")]
       [Display(ResourceType = typeof(DisplayResource), Name = "Password")]
       public string Password { get; set; }
    }
}
