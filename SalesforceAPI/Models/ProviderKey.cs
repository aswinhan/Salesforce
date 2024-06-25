using System.ComponentModel.DataAnnotations;

namespace SalesforceAPI.Models
{
    public class ProviderKey
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public Guid? PractitionerGUID { get; set; }
        public string? EncompassID { get; set; }
        public string? CredentialingProfileId { get; set; }
    }
}
