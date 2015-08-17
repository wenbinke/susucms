using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using SusuCMS.Data.Enums;


namespace SusuCMS.Data
{
    public class AdminRole
    {
        public AdminRole()
        {
            Users = new List<AdminUser>();
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "Permissions")]
        public byte[] Permissions { get; set; }

        public bool IsRoot { get; set; }

        public virtual ICollection<AdminUser> Users { get; set; }

        public IList<Permission> GetPermissions()
        {
            var list = new List<Permission>();
            var permissions = Enum.GetValues(typeof(Permission));
            if (Permissions != null)
            {
                var bits = Permissions.ToBitArray();
                for (var i = 0; i < permissions.Length; i++)
                {
                    if (bits[i])
                    {
                        list.Add((Permission)i);
                    }
                }
            }

            return list;
        }

        public bool HasPermission(Permission permission)
        {
            if (Permissions != null)
            {
                var bits = Permissions.ToBitArray();
                return bits[(int)permission];
            }

            return false;
        }

        public void UpdatePermissions(IList<Permission> list)
        {
            var bits = new BitArray(new byte[Enum.GetValues(typeof(Permission)).Length]);
            foreach (var item in list)
            {
                bits[(int)item] = true;
            }
            Permissions = bits.ToByteArray();
        }
    }
}
