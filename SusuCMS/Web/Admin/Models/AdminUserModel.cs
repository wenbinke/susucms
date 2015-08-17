using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Web.Admin.Models
{
    public abstract class AdminUserModel
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", 
            ErrorMessageResourceType=typeof(MessageResource), ErrorMessageResourceName="InvalidEmailAddress")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Role")]
        public int RoleId { get; set; }
    }

    public class CreateAdminUserModel : AdminUserModel
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "UserName")]
        [System.Web.Mvc.Remote("UserNameAvailable", "AdminUser", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "UserNameExist")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayResource), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "PasswordAndConfirmPasswordNotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class EditAdminUserModel : AdminUserModel
    {
        public bool IsRoot { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "UserName")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayResource), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "PasswordAndConfirmPasswordNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}