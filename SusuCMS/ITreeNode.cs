using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS
{
    public interface ITreeNode<T>
    {
        int Id { get; set; }

        int? ParentId { get; set; }

        T Parent { get; set; }
    }
}
