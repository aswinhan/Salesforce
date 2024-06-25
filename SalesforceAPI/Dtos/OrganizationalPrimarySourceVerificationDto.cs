using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SalesforceAPI.Dtos
{
    public class OrganizationalPrimarySourceVerificationDto
    {
        public string? Credentialing_Profile__c { get; set; }
        public string? Primary_Source_Verifier__c { get; set; }
        public bool? CVO__c { get; set; }
        public string? Other_Accred__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VerifierSCredentialingOrganizationCEnum? Verifier_s_Credentialing_Organization__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LARALicenseCEnum? LARA_License__c { get; set; }
        public bool? LARA_Uploaded__c { get; set; }
        public bool? Proof_of_Accreditation__c { get; set; }
        public bool? Disciplinary_status_with_regulatory_boar__c { get; set; }
        public bool? MDHHS_Sanctioned_Provider_Check__c { get; set; }
        public bool? At_least_five_year_history_of_organizati__c { get; set; }
        public bool? On_Site_Quality_Assessment_Recredential__c { get; set; }
        public bool? Office_of_Inspector_General_Check__c { get; set; }
        public bool? SAM_gov_Check__c { get; set; }
        public enum VerifierSCredentialingOrganizationCEnum
        {
            [EnumMember(Value = "COA")]
            COAEnum = 0,
            [EnumMember(Value = "NCQA")]
            NCQAEnum = 1,
            [EnumMember(Value = "Joint Commission")]
            JointCommissionEnum = 2,
            [EnumMember(Value = "URAC")]
            URACEnum = 3,
            [EnumMember(Value = "Other")]
            OtherEnum = 4,
            [EnumMember(Value = "CARF")]
            CARFEnum = 5,
            [EnumMember(Value = "None")]
            NoneEnum = 6
        }
        public enum LARALicenseCEnum
        {
            [EnumMember(Value = "Acupuncture")]
            AcupunctureEnum = 0,
            [EnumMember(Value = "Audiology")]
            AudiologyEnum = 1,
            [EnumMember(Value = "Addiction Medicine")]
            AddictionMedicineEnum = 2,
            [EnumMember(Value = "Behavior Analysts")]
            BehaviorAnalystsEnum = 3,
            [EnumMember(Value = "Counseling")]
            CounselingEnum = 4,
            [EnumMember(Value = "Genetic Counseling")]
            GeneticCounselingEnum = 5,
            [EnumMember(Value = "Marriage and Family Therapy")]
            MarriageAndFamilyTherapyEnum = 6,
            [EnumMember(Value = "Massage Therapy")]
            MassageTherapyEnum = 7,
            [EnumMember(Value = "Medicine")]
            MedicineEnum = 8,
            [EnumMember(Value = "Nursing")]
            NursingEnum = 9,
            [EnumMember(Value = "Nursing Home Administrator")]
            NursingHomeAdministratorEnum = 10,
            [EnumMember(Value = "Occupational Therapy")]
            OccupationalTherapyEnum = 11,
            [EnumMember(Value = "Osteopathic Medicine & Surgery")]
            OsteopathicMedicineSurgeryEnum = 12,
            [EnumMember(Value = "Pharmacy")]
            PharmacyEnum = 13,
            [EnumMember(Value = "Physical Therapy")]
            PhysicalTherapyEnum = 14,
            [EnumMember(Value = "Physician Assistant (PA)")]
            PhysicianAssistantPAEnum = 15,
            [EnumMember(Value = "Psychology")]
            PsychologyEnum = 16,
            [EnumMember(Value = "Respiratory Care")]
            RespiratoryCareEnum = 17,
            [EnumMember(Value = "Social Worker")]
            SocialWorkerEnum = 18,
            [EnumMember(Value = "Speech-Language Pathology")]
            SpeechLanguagePathologyEnum = 19,
            [EnumMember(Value = "Nurse Practitioner (NP)")]
            NursePractitionerNPEnum = 20,
            [EnumMember(Value = "Other")]
            OtherEnum = 21
        }
    }
}
