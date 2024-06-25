namespace SalesforceAPI.Models
{
    public class CompositeSubRequestDto
    {
        public string? Method { get; set; }
        public string? Url { get; set; }
        public string? ReferenceId { get; set; }
        public object? Body { get; set; }
    }
}
