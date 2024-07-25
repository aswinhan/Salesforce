using System.Runtime.Serialization;
using static SalesforceWeb.Dtos.OrganizationalPSVDto;

namespace SalesforceWeb.Dtos
{
    public class PractitionerPSVDto
    {
        public string Credentialing_Profile__c { get; set; }
        public string Primary_Source_Verifier_c { get; set; }
        public bool? CVO__c { get; set; }
        public string Other_Accred__c { get; set; }
        public VerifiersCredentialingOrganizationEnum? Verifier_s_Credentialing_Organization__c { get; set; }
        public bool? Ichat_Background_Check__c { get; set; }
        public bool? Workforce_Background_Check__c { get; set; }
        public bool? Medicare_Base_Enrollment__c { get; set; }
        public bool? Medicare_Opt_Out__c { get; set; }
        public LARALicenseCEnum? LARA_License__c { get; set; }
        public bool? LARA_Uploaded__c { get; set; }
        public bool? Official_Transcript_from_Accredited_Scho__c { get; set; }
        public bool? Degree_Verification__c { get; set; }
        public bool? AMA_Verification__c { get; set; }
        public bool? AOA_Verification__c { get; set; }
        public bool? MDHHS_Sanctioned_Provider_Check__c { get; set; }
        public bool? MCBAP_Verification__c { get; set; }
        public bool? Office_of_Inspector_General_Check__c { get; set; }
        public bool? SAM_gov_Check__c { get; set; }
        public bool? National_Sex_Offender_Registry_Check__c { get; set; }
        public bool? MI_Public_Sex_Offender_Registry_Check__c { get; set; }
        public bool? ECFMG__c { get; set; }

        public enum VerifiersCredentialingOrganizationEnum
        {
            [EnumMember(Value = "COA")]
            COA = 0,
            [EnumMember(Value = "NCQA")]
            NCQA = 1,
            [EnumMember(Value = "Joint Commission")]
            JointCommission = 2,
            [EnumMember(Value = "URAC")]
            URAC = 3,
            [EnumMember(Value = "Other")]
            Other = 4,
            [EnumMember(Value = "CARF")]
            CARF = 5,
            [EnumMember(Value = "None")]
            None = 6
        }
    }
}
