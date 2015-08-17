using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS
{
    public static class TreeExtensions
    {
        public static IList<T> ToTreeList<T>(this IList<T> list) where T : ITreeNode<T>
        {
            var result = new List<T>();
            var parents = list.Where(i => i.ParentId == null);
            foreach (var item in parents)
            {
                result.AddRange(BuildItems(item, list));
            }

            return result;
        }

        public static IList<T> ToTreeList<T>(this IQueryable<T> list) where T : ITreeNode<T>
        {
            var result = new List<T>();
            var newList = list.ToList();
            var parents = newList.Where(i => i.ParentId == null);
            foreach (var item in parents)
            {
                result.AddRange(BuildItems(item, newList));
            }

            return result;
        }

        static IList<T> BuildItems<T>(T node, IList<T> list) where T : ITreeNode<T>
        {
            var result = new List<T>();
            result.Add(node);
            var children = list.Where(i => i.ParentId == node.Id);
            foreach (var item in children)
            {
                result.AddRange(BuildItems(item, list));
            }

            return result;
        }

        public static void RemoveOffspring<T>(this IList<T> list, T node) where T : ITreeNode<T>
        {
            RemoveOffspring(node, list);
        }

        static void RemoveOffspring<T>(T node, IList<T> list) where T : ITreeNode<T>
        {
            var children = list.Where(i => i.ParentId == node.Id).ToArray();
            foreach (var item in children)
            {
                RemoveOffspring(item, list);
                list.Remove(item);
            }
        }
    }
}
