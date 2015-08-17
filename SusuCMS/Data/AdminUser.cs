using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SusuCMS.Data.Enums;


namespace SusuCMS.Data
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        public string Salt { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "InvalidEmail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "CreationTime")]
        public DateTime CreationTime { get; set; }

        public bool IsRoot { get; set; }

        public int Status { get; set; }

        public int RoleId { get; set; }

        public virtual AdminRole Role { get; set; }

        public bool HasPermission(Permission permission)
        {
            return Role.HasPermission(permission);
        }
    }
}
