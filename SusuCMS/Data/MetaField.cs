using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Data
{
    [NotMapped]
    public class MetaField
    {
        public MetaField() { }

        public MetaField(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
