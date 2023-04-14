namespace SimpleUser.API.DTOs
{
    public class PageInfoDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Filter { get; set; }
    }
}
