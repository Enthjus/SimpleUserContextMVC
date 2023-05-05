using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Models
{
    public class CustomerViewModel
    {
        public PaginatedList<CustomerDto> Customers { get; set; }
        public IndexViewModel Index { get; set; }
    }
}
