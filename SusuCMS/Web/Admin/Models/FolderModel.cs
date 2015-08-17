using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace SusuCMS.Web.Admin.Models
{
    public class FolderModel
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "FolderName")]
        [Remote("FolderAvailable", "Media", AdditionalFields = "Path,IsCreate", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "FolderExist")]
        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsCreate { get; set; }
    }
}