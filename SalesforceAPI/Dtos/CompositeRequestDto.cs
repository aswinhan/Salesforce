namespace SalesforceAPI.Models
{
    public class CompositeRequest
    {
        public bool AllOrNone { get; set; }
        public required List<CompositeSubRequestDto> CompositeSubRequestList { get; set; }
    }
}
