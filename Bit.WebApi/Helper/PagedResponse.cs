using System.Collections.Generic;
using System.Linq;

namespace Bit.WebApi.Helper
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public ICollection<T> People { get; set; }
        public PagedResponse(IEnumerable<T> data, int pageIndex, int pageSize)
        {
            People = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            Total = data.Count();
        }
    }
}