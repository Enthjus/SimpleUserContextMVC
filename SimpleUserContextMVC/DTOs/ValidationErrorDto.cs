namespace SimpleUser.MVC.DTOs
{
    public class ValidationErrorDto
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public ErrorInfoDto errors { get; set; }
        public int? Id { get; set; }
    }
}
