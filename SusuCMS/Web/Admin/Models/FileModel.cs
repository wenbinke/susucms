using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SusuCMS.Web.Admin.Models
{
    public class FileModel
    {
        [Display(ResourceType = typeof(DisplayResource), Name = "FileName")]
        [Remote("FileAvailable", "Media", AdditionalFields = "Path", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "FileExist")]
        public string Name { get; set; }

        public string Path { get; set; }
    }
}