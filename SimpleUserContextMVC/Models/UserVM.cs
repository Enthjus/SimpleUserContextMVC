using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Models
{
    public class UserVM
    {
        public IList<UserDto> Users { get; set; }
        public string SearchString { get; set; }
        public int PageIndex { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
