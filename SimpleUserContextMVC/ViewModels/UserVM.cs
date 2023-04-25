using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.ViewModels
{
    public class UserVM
    {
        public PaginatedList<UserDto> Users { get; set; }
        public IndexVM Index { get; set; }
    }
}
