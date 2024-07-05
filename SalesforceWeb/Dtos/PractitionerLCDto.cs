using System.Runtime.Serialization;

namespace SalesforceWeb.Dtos
{
    public class PractitionerLCDto
    {
        public string? RecordTypeId { get; set; }
        public string? Credentialing_Profile_Id__c { get; set; }
        public DateTime? Expiration_Date__c { get; set; }
        public bool? File_Uploaded__c { get; set; }
        public LicenseCertificationStatusEnum? LicenseCertification_Status__c { get; set; }
        public string? LicenseCertification_Type__c { get; set; }
        public LicenseTypesLARAEnum? License_Types_LARA__c { get; set; }

        public enum LicenseTypesLARAEnum
        {
            [EnumMember(Value = "Acupuncture")]
            Acupuncture = 0,
            [EnumMember(Value = "Audiology")]
            Audiology = 1,
            [EnumMember(Value = "Addiction Medicine")]
            AddictionMedicine = 2,
            [EnumMember(Value = "Behavior Analysts")]
            BehaviorAnalysts = 3,
            [EnumMember(Value = "Counseling")]
            Counseling = 4,
            [EnumMember(Value = "Genetic Counseling")]
            GeneticCounseling = 5,
            [EnumMember(Value = "Marriage and Family Therapy")]
            MarriageAndFamilyTherapy = 6,
            [EnumMember(Value = "Massage Therapy")]
            MassageTherapy = 7,
            [EnumMember(Value = "Medicine")]
            Medicine = 8,
            [EnumMember(Value = "Nursing")]
            Nursing = 9,
            [EnumMember(Value = "Nursing Home Administrator")]
            NursingHomeAdministrator = 10,
            [EnumMember(Value = "Occupational Therapy")]
            OccupationalTherapy = 11,
            [EnumMember(Value = "Osteopathic Medicine & Surgery")]
            OsteopathicMedicineAndSurgery = 12,
            [EnumMember(Value = "Pharmacy")]
            Pharmacy = 13,
            [EnumMember(Value = "Physical Therapy")]
            PhysicalTherapy = 14,
            [EnumMember(Value = "Physician Assistant (PA)")]
            PhysicianAssistant = 15,
            [EnumMember(Value = "Psychology")]
            Psychology = 16,
            [EnumMember(Value = "Respiratory Care")]
            RespiratoryCare = 17,
            [EnumMember(Value = "Social Worker")]
            SocialWorker = 18,
            [EnumMember(Value = "Speech-Language Pathology")]
            SpeechLanguagePathology = 19,
            [EnumMember(Value = "Nurse Practitioner (NP)")]
            NursePractitioner = 20,
            [EnumMember(Value = "Other")]
            Other = 21,
            [EnumMember(Value = "Psychiatry")]
            Psychiatry = 22
        }

        public enum LicenseCertificationStatusEnum
        {
            [EnumMember(Value = "Active")]
            Active = 0,
            [EnumMember(Value = "Expired")]
            Expired = 1
        }
    }
}
