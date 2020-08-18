using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blog.BLL.Models.Page
{
    public class Pager<TEntry>
    {
        private readonly IQueryable<TEntry> _list;
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly int _pagesQuantity;
        private readonly long _itemsQuantity;

        public Pager(IQueryable<TEntry> query, int pageIndex, int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("გვერდი არ შეიძლება იყოს 1 ჩანაწერზე ნაკლები");
            }

            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _list = query;
            _itemsQuantity = query.Count();
            _pagesQuantity = CountPages();
        }

        public int CountPages()
        {
            int pages = _itemsQuantity == 0 ? 1 : 0;
            pages += (int)(_itemsQuantity / _pageSize);
            pages += _itemsQuantity % _pageSize == 0 ? 0 : 1;

            return pages;
        }

        private IQueryable<TEntry> GetItems(int pageIndex)
        {
            if (pageIndex < 1 || pageIndex > _pagesQuantity)
                throw new IndexOutOfRangeException();

            return _list.Skip((pageIndex - 1) * _pageSize).Take(_pageSize);
        }

        public Page<TEntry> GetPage()
        {
            var list = GetItems(_pageIndex);

            var page = new Page<TEntry>(list, _itemsQuantity, _pagesQuantity, _pageIndex);
            return page;
        }
    }
}
