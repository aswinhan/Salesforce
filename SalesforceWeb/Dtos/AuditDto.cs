namespace SalesforceWeb.Dtos
{
    public class AuditDto
    {
        public string UserId { get; set; }
        public string TableName { get; set; }
        public string Type { get; set; }
        public string PrimaryKey { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public DateTime DateTime { get; set; }
        public UserDto UserDto { get; set; }
    }
}
