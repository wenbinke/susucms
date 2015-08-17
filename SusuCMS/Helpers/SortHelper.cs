using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS.Data;

namespace SusuCMS.Helpers
{
    public class SortHelper<T> where T : ISortable
    {
        private IList<T> _list;

        public SortHelper(IList<T> sortedlist)
        {
            _list = sortedlist;
        }

        private void Move(T item1, T item2)
        {
            var intervalList = _list.Where(i => i.DisplayOrder < item1.DisplayOrder && i.DisplayOrder > item2.DisplayOrder).ToList();
            intervalList.Insert(0, item2);
            intervalList.Insert(0, item1);

            var baseDisplayOrder = item2.DisplayOrder;
            foreach (var item in intervalList)
            {
                item.DisplayOrder = baseDisplayOrder++;
            }
        }

        public void MoveUp(T item)
        {
            var beforeItem = GetPreviousItem(item);
            if (beforeItem == null)
            {
                throw new InvalidOperationException("Cannot move, it is the first item.");
            }

            MoveUp(item, beforeItem);
        }

        public void MoveDown(T item)
        {
            var afterItem = GetNextItem(item);
            if (afterItem == null)
            {
                throw new InvalidOperationException("Cannot move, it is the last item.");
            }
            MoveDown(item, afterItem);
        }

        public void MoveUp(T item, T beforeItem)
        {
            Move(item, beforeItem);
        }

        public void MoveDown(T item, T afterItem)
        {
            Move(afterItem, item);
        }

        public bool IsFirstItem(T item)
        {
            return GetPreviousItem(item) == null;
        }

        public bool IsLastItem(T item)
        {
            return GetNextItem(item) == null;
        }

        private T GetNextItem(T item)
        {
            var index = _list.IndexOf(item);
            var nextIndex = index + 1;
            if (nextIndex < _list.Count)
            {
                return _list[nextIndex];
            }

            return default(T);
        }

        private T GetPreviousItem(T item)
        {
            var index = _list.IndexOf(item);
            var nextIndex = index - 1;
            if (nextIndex >= 0)
            {
                return _list[nextIndex];
            }

            return default(T);
        }
    }
}
