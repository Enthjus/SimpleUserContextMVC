namespace SimpleUser.API.DTOs
{
    public class PaginatedListDto
    {
        public IList<UserDto> Users { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
