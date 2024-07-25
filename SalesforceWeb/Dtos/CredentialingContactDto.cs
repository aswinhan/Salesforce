using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace SalesforceWeb.Dtos
{
    public class CredentialingContactDto
    {
        public string Contact_First_Name__c { get; set; }
        public string Contact_Last_Name__c { get; set; }
        public string Contact_Email__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ContactPersonRoleCEnum? Contact_Person_Role__c { get; set; }
        public string Contact_Phone__c { get; set; }
        public bool? Primary_Contact__c { get; set; }
        public string Credentialing_Profile_Id__c { get; set; }
        public enum ContactPersonRoleCEnum
        {
            [EnumMember(Value = "Primary Contact")]
            PrimaryContactEnum = 0,
            [EnumMember(Value = "Compliance Officer")]
            ComplianceOfficerEnum = 1,
            [EnumMember(Value = "Quality Improvement Officer")]
            QualityImprovementOfficerEnum = 2,
            [EnumMember(Value = "Credentialing Contact")]
            CredentialingContactEnum = 3,
            [EnumMember(Value = "Program Manager")]
            ProgramManagerEnum = 4,
            [EnumMember(Value = "Recipient Rights Officer")]
            RecipientRightsOfficerEnum = 5,
            [EnumMember(Value = "CEO")]
            CEOEnum = 6,
            [EnumMember(Value = "CFO")]
            CFOEnum = 7,
            [EnumMember(Value = "Customer Service")]
            CustomerServiceEnum = 8
        }
    }
}
