using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Web.Admin.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceType=typeof(MessageResource), ErrorMessageResourceName="Required")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Remote("VerifyPassword", "Account", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "InvalidPassword")]
        [Display(ResourceType = typeof(DisplayResource), Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType=typeof(MessageResource), ErrorMessageResourceName="Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(DisplayResource), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType=typeof(MessageResource), ErrorMessageResourceName="Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "ConfirmPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "PasswordAndConfirmPasswordNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}