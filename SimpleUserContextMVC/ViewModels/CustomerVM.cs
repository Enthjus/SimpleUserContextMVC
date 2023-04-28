using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.ViewModels
{
    public class CustomerVM
    {
        public PaginatedList<CustomerDto> Users { get; set; }
        public IndexVM Index { get; set; }
    }
}
