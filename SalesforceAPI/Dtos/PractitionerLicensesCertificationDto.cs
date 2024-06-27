namespace SalesforceAPI.Dtos
{
    public class PractitionerLicensesCertificationDto
    {
        public string? RecordTypeId { get; set; }
        public string? Credentialing_Profile_Id__c { get; set; }
        public DateTime? Expiration_Date__c { get; set; }
        public bool? File_Uploaded__c { get; set; }
        public string? LicenseCertification_Status__c { get; set; }
        public string? LicenseCertification_Type__c { get; set; }
        public string? License_Types_LARA__c { get; set; }
    }
}
