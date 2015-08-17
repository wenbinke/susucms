using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;



namespace SusuCMS.Data
{
    public class SystemLog
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "LogType")]
        public int LogType { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "CreationTime")]
        public DateTime CreationTime { get; set; }

        [Required(ErrorMessageResourceType=typeof(MessageResource), ErrorMessageResourceName="Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Message")]
        public string Message { get; set; }
    }
}
