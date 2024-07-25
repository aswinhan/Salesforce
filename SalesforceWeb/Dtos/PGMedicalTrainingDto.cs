using System.Runtime.Serialization;

namespace SalesforceWeb.Dtos
{
    public class PGMedicalTrainingDto
    {
        public string Credentialing_Profile_Id__c { get; set; }
        public string Medical_Training_Hospital_Address__c { get; set; }
        public string Medical_Training_Hospital_Name__c { get; set; }
        public MedicalTrainingTypeCEnum? Medical_Training_Type__c { get; set; }
        public string Specialty__c { get; set; }
        public DateTime? Training_End_Date__c { get; set; }
        public DateTime? Training_Start_Date__c { get; set; }

        public enum MedicalTrainingTypeCEnum
        {
            [EnumMember(Value = "Internship")]
            Internship = 0,
            [EnumMember(Value = "Residencies")]
            Residencies = 1,
            [EnumMember(Value = "Fellowships")]
            Fellowships = 2,
            [EnumMember(Value = "Preceptorships")]
            Preceptorships = 3
        }
    }
}
