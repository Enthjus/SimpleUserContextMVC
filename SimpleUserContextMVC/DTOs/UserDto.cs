using SimpleUserContext.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleUserContextMVC.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public UserDetailDto UserDetailDto { get; set; }
    }
}
