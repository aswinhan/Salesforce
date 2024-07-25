using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SalesforceWeb.Dtos
{
    public class OrganizationalCPDto
    {
        public DateTime? Accreditation_End__c { get; set; }
        public DateTime? Accreditation_Start__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccreditationBodyCEnum? Accrediting_Body__c { get; set; }
        public bool? Proof_of_Accreditation_Status__c { get; set; }
        public DateTime? When_was_the_last_accreditation_survey__c { get; set; }
        public DateTime? Date_of_Birth__c { get; set; }
        public long? Tax_ID__c { get; set; }
        public bool? Medicare_Provider__c { get; set; }
        public long? Medicare_Number__c { get; set; }
        public string Practitioner_NPI__c { get; set; }
        public string Office_Address_Street_1__c { get; set; }
        public string Office_Address_City__c { get; set; }
        public string Office_Address_Zipcode__c { get; set; }
        public string Office_Address_State__c { get; set; }
        public string Office_Address_County__c { get; set; }
        public string Home_Address_Street_1__c { get; set; }
        public string Home_Address_City__c { get; set; }
        public string Home_Address_Zipcode__c { get; set; }
        public string Home_Address_State__c { get; set; }
        public string Home_Address_County__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SpecialtiesCEnum? Specialties__c { get; set; }
        public string Specialty_Other__c { get; set; }
        public bool? Certificate_of_Liability__c { get; set; }
        public DateTime? Certificate_of_Liability_Expiration_Date__c { get; set; }
        public string Languages_Spoken__c { get; set; }
        public string Cultural_Competences__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public X6MonthGapInEmploymentSinceProfessCEnum? X6_month_gap_in_employment_since_profess__c { get; set; }
        public DateTime? X6_Month_Gap_Start_Date__c { get; set; }
        public DateTime? X6_Month_Gap_End_Date__c { get; set; }
        public string X6_Month_Gap_Activity__c { get; set; }
        public string X6_Month_Gap_Reason__c { get; set; }
        public bool? Please_acknowledge_If_denied_credential__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CanYouPerformTheEssentialDutiesOfCEnum? Can_you_perform_the_essential_duties_of__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LackOfPresentIllegalDrugUseCEnum? Lack_of_present_illegal_drug_use__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HistoryOfLossOfLicenseCEnum? History_of_loss_of_license__c { get; set; }
        public string History_of_felony_convictions__c { get; set; }
        public string History_of_loss_or_limitations_of_privil__c { get; set; }
        public string ExplanationHistory_of_felony_convictions__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CurrentMalpracticeInsuranceCoverageCEnum? Current_Malpractice_Insurance_Coverage__c { get; set; }
        public string Attestation_by_the_applicant_of_the_corr__c { get; set; }
        public string Office_Phone_Number__c { get; set; }
        public string Email__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CulturalCompetenciesPicklistCEnum? Cultural_Competencies_Picklist__c { get; set; }
        public DateTime? W9_Expiration__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public W9StatusCEnum? W9_Status__c { get; set; }
        public DateTime? Disclosure_Form_Expiration__c { get; set; }
        public DateTime? Professional_Liability_Expiration__c { get; set; }
        public DateTime? Commercial_Liability_Expiration__c { get; set; }        
        public DateTime? Workers_Compensation_Expiration__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FiveYearHistoryOrgLiabilityCEnum? Five_year_history_org_liability__c { get; set; }
        public string Explanation_five_year_history__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ActionPendingOrgLicenseCEnum? Action_pending_org_license__c { get; set; }
        public string Explanation_org_license__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrgAccreditationLimitedCEnum? Org_accreditation_limited__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ExplanationOrgAccreditationLimitedCEnum? Explanation_org_accreditation_limited__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ActionPendingCurrentAccreditationCEnum? Action_pending_current_accreditation__c { get; set; }
        public string Explanation_current_accreditation__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrgMedicaidSanctionsCEnum? Org_medicaid_sanctions__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrgMedicareSanctionsCEnum? Org_medicare_sanctions__c { get; set; }
        public string Explanation_org_medicaid_sanctions__c { get; set; }
        public string Explanation_org_medicare_sanctions__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrgInsuranceInitiallyRefusedCEnum? Org_insurance_initially_refused__c { get; set; }
        public string Explanation_org_insurance_initially_refu__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrgDefendantSUDPaymentCEnum? Org_defendant_SUD_payment__c { get; set; }
        public string Explanation_org_defendant_SUD_payment__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrgMalPracticeClaimsSUDCEnum? Org_malpractice_claims_SUD__c { get; set; }
        public string Explanation_org_malpractice_claims_SUD__c { get; set; }
        public bool? Please_indicate_if_you_have_a_speciality__c { get; set; }
        public bool? Proof_of_Accreditation__c { get; set; }
        public bool? Cyber_Liability_if_applicable__c { get; set; }
        public bool? Commercial_Liability__c { get; set; }
        public DateTime? Proof_of_Accreditation_Expiration__c { get; set; }
        public DateTime? Cyber_Liability_Expiration_if_applicabl__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CyberLiabilityStatusifApplicableCEnum? Cyber_Liability_Status_if_applicable__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DisclosureFormStatusCEnum? Disclosure_Form_Status__c { get; set; }
        public string Summary_of_Changes__c { get; set; }

        public enum AccreditationBodyCEnum
        {
            [EnumMember(Value = "MDCH")]
            MDCHEnum = 0,
            [EnumMember(Value = "CARF")]
            CARFEnum = 1,
            [EnumMember(Value = "TJC")]
            TJCEnum = 2,
            [EnumMember(Value = "NCQA")]
            NCQAEnum = 3,
            [EnumMember(Value = "URAC")]
            URACEnum = 4,
            [EnumMember(Value = "CHAP")]
            CHAPEnum = 5,
            [EnumMember(Value = "COA")]
            COAEnum = 6,
            [EnumMember(Value = "AOA")]
            AOAEnum = 7,
            [EnumMember(Value = "Other")]
            OtherEnum = 8
        }
        public enum SpecialtiesCEnum
        {
            [EnumMember(Value = "Adult Psych")]
            AdultPsychEnum = 0,
            [EnumMember(Value = "Child Psych")]
            ChildPsychEnum = 1,
            [EnumMember(Value = "Co-occuring (MH & SUD)")]
            CoOccuringMHSUDEnum = 2,
            [EnumMember(Value = "Eating Disorders")]
            EatingDisordersEnum = 3,
            [EnumMember(Value = "Eye Movement Desensitization and Reprocessing (EMDR)")]
            EyeMovementDesensitizationAndReprocessingEMDREnum = 4,
            [EnumMember(Value = "Infant Mental Health")]
            InfantMentalHealthEnum = 5,
            [EnumMember(Value = "Prolonged Exposure Therapy")]
            ProlongedExposureTherapyEnum = 6,
            [EnumMember(Value = "Women's Specialty (SUD)")]
            WomensSpecialtySUDEnum = 7,
            [EnumMember(Value = "SUD Detox")]
            SUDDetoxEnum = 8,
            [EnumMember(Value = "Opiod/Methadone Treatment Program (OTP)")]
            OpiodMethadoneTreatmentProgramOTPEnum = 9,
            [EnumMember(Value = "Other")]
            OtherEnum = 10
        }
        public enum CurrentMalpracticeInsuranceCoverageCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum X6MonthGapInEmploymentSinceProfessCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum CanYouPerformTheEssentialDutiesOfCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum LackOfPresentIllegalDrugUseCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum HistoryOfLossOfLicenseCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum CulturalCompetenciesPicklistCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum W9StatusCEnum
        {
            [EnumMember(Value = "Active")]
            ActiveEnum = 0,
            [EnumMember(Value = "Expired")]
            ExpiredEnum = 1
        }
        public enum FiveYearHistoryOrgLiabilityCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1,
            [EnumMember(Value = "N/A")]
            NAEnum = 2
        }
        public enum ActionPendingOrgLicenseCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum OrgAccreditationLimitedCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1,
            [EnumMember(Value = "N/A")]
            NAEnum = 2
        }
        public enum ExplanationOrgAccreditationLimitedCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1,
            [EnumMember(Value = "N/A")]
            NAEnum = 2
        }
        public enum ActionPendingCurrentAccreditationCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1,
            [EnumMember(Value = "N/A")]
            NAEnum = 2
        }
        public enum OrgMedicaidSanctionsCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum OrgMedicareSanctionsCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum OrgInsuranceInitiallyRefusedCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum OrgDefendantSUDPaymentCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum OrgMalPracticeClaimsSUDCEnum
        {
            [EnumMember(Value = "Yes")]
            YesEnum = 0,
            [EnumMember(Value = "No")]
            NoEnum = 1
        }
        public enum CyberLiabilityStatusifApplicableCEnum
        {
            [EnumMember(Value = "Active")]
            ActiveEnum = 0,
            [EnumMember(Value = "Expired")]
            ExpiredEnum = 1
        }
        public enum DisclosureFormStatusCEnum
        {
            [EnumMember(Value = "Active")]
            ActiveEnum = 0,
            [EnumMember(Value = "Expired")]
            ExpiredEnum = 1
        }
    }
}
