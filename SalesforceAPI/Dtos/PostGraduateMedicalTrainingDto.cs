namespace SalesforceAPI.Dtos
{
    public class PostGraduateMedicalTrainingDto
    {
        public string? Credentialing_Profile_Id__c { get; set; }
        public string? Medical_Training_Hospital_Address__c { get; set; }
        public string? Medical_Training_Hospital_Name__c { get; set; }
        public string? Medical_Training_Type__c { get; set; }
        public string? Specialty__c { get; set; }
        public DateTime? Training_End_Date__c { get; set; }
        public DateTime? Training_Start_Date__c { get; set; }
    }
}
