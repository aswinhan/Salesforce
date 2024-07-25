using System.ComponentModel.DataAnnotations;
using static SalesforceWeb.Dtos.CredentialingContactDto;

namespace SalesforceWeb.Models
{
    public class CredentialingContact
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public ContactPersonRoleCEnum? ContactPersonRole { get; set; }
        public string ContactPhone { get; set; }
        public bool? PrimaryContact { get; set; }
        public string CredentialingProfileId { get; set; }
    }
}
