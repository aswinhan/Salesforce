using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SalesforceAPI.Dtos
{
    public class ServiceLocationLicenseDto
    {
        public string? Credentialing_Profile__c { get; set; }
        public string? Service_Locations__c { get; set; }
        public string? Service_Name__c { get; set; }
        public DateTime? Service_License_Expiration_if_applicabl__c { get; set; }
        public string? Service_License_Number_if_applicable__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ServiceLicenseStatusIfApplicableCEnum? Service_License_Status_if_applicable__c { get; set; }
        public enum ServiceLicenseStatusIfApplicableCEnum
        {
            [EnumMember(Value = "Active")]
            ActiveEnum = 0,
            [EnumMember(Value = "Expired")]
            ExpiredEnum = 1
        }
    }
}
