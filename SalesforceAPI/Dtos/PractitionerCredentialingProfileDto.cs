using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SalesforceAPI.Dtos
{
    public class PractitionerCredentialingProfileDto
    {
        public DateTime? Certificate_of_Liability_Expiration_Date__c { get; set; }
        public DateTime? MalpracticeInsurance_Coverage_Expiration__c { get; set; }
        public string? Summary_of_Changes__c { get; set; }
    }
}
