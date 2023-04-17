using Microsoft.EntityFrameworkCore;

namespace SimpleUser.MVC.DTOs
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }
        public List<T> Users { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
}
