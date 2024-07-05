using System.ComponentModel.DataAnnotations;
using static SalesforceAPI.Dtos.OrganizationalCPDto;

namespace SalesforceAPI.Models
{
    public class OrganizationalCP
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DateTime? AccreditationEnd { get; set; }
        public DateTime? AccreditationStart { get; set; }
        public AccreditationBodyCEnum? AccreditingBody { get; set; }
        public bool? ProofofAccreditationStatus { get; set; }
        public DateTime? Whenwasthelastaccreditationsurvey { get; set; }
        public DateTime? DateofBirth { get; set; }
        public long? TaxID { get; set; }
        public bool? MedicareProvider { get; set; }
        public long? MedicareNumber { get; set; }
        public string? NPI { get; set; }
        public string? OfficeAddressStreet1 { get; set; }
        public string? OfficeAddressCity { get; set; }
        public string? OfficeAddressZipcode { get; set; }
        public string? OfficeAddressState { get; set; }
        public string? OfficeAddressCounty { get; set; }
        public string? HomeAddressStreet1 { get; set; }
        public string? HomeAddressCity { get; set; }
        public string? HomeAddressZipcode { get; set; }
        public string? HomeAddressState { get; set; }
        public string? HomeAddressCounty { get; set; }
        public SpecialtiesCEnum? Specialties { get; set; }
        public string? OtherSpecialty { get; set; }
        public bool? CertificateofLiability { get; set; }
        public DateTime? CertificateofLiabilityExpirationDate { get; set; }
        public string? LanguagesSpoken { get; set; }
        public string? CulturalCompetences { get; set; }
        public X6MonthGapInEmploymentSinceProfessCEnum? X6monthgapinemploymentsinceprofess { get; set; }
        public DateTime? X6MonthGapStartDate { get; set; }
        public DateTime? X6MonthGapEndDate { get; set; }
        public string? X6MonthGapActivity { get; set; }
        public string? X6MonthGapReason { get; set; }
        public bool? PleaseacknowledgeIfdeniedcredential { get; set; }
        public CanYouPerformTheEssentialDutiesOfCEnum? Canyouperformtheessentialdutiesof { get; set; }
        public LackOfPresentIllegalDrugUseCEnum? Lackofpresentillegaldruguse { get; set; }
        public HistoryOfLossOfLicenseCEnum? Historyoflossoflicense { get; set; }
        public string? Historyoffelonyconvictions { get; set; }
        public string? Historyoflossorlimitationsofprivil { get; set; }
        public string? ExplanationHistoryoffelonyconvictions { get; set; }
        public CurrentMalpracticeInsuranceCoverageCEnum? CurrentMalpracticeInsuranceCoverage { get; set; }
        public string? Attestationbytheapplicantofthecorr { get; set; }
        public string? OfficePhoneNumber { get; set; }
        public string? Email { get; set; }
        public CulturalCompetenciesPicklistCEnum? CulturalCompetenciesPicklist { get; set; }
        public DateTime? W9Expiration { get; set; }
        public W9StatusCEnum? W9Status { get; set; }
        public DateTime? DisclosureFormExpiration { get; set; }
        public DateTime? ProfessionalLiabilityExpiration { get; set; }
        public DateTime? CommercialLiabilityExpiration { get; set; }
        public DateTime? WorkersCompensationExpiration { get; set; }
        public FiveYearHistoryOrgLiabilityCEnum? Fiveyearhistoryorgliability { get; set; }
        public string? Explanationfiveyearhistory { get; set; }
        public ActionPendingOrgLicenseCEnum? Actionpendingorglicense { get; set; }
        public string? Explanationorglicense { get; set; }
        public OrgAccreditationLimitedCEnum? Orgaccreditationlimited { get; set; }
        public ExplanationOrgAccreditationLimitedCEnum? Explanationorgaccreditationlimited { get; set; }
        public ActionPendingCurrentAccreditationCEnum? Actionpendingcurrentaccreditation { get; set; }
        public string? Explanationcurrentaccreditation { get; set; }
        public OrgMedicaidSanctionsCEnum? Orgmedicaidsanctions { get; set; }
        public OrgMedicareSanctionsCEnum? Orgmedicaresanctions { get; set; }
        public string? Explanationorgmedicaidsanctions { get; set; }
        public string? Explanationorgmedicaresanctions { get; set; }
        public OrgInsuranceInitiallyRefusedCEnum? Orginsuranceinitiallyrefused { get; set; }
        public string? Explanationorginsuranceinitiallyrefused { get; set; }
        public OrgDefendantSUDPaymentCEnum? OrgdefendantSUDpayment { get; set; }
        public string? ExplanationorgdefendantSUDpayment { get; set; }
        public OrgMalPracticeClaimsSUDCEnum? OrgmalpracticeclaimsSUD { get; set; }
        public string? ExplanationorgmalpracticeclaimsSUD { get; set; }
        public bool? Pleaseindicateifyouhaveaspeciality { get; set; }
        public bool? ProofofAccreditation { get; set; }
        public bool? CyberLiabilityifapplicable { get; set; }
        public bool? CommercialLiability { get; set; }
        public DateTime? ProofofAccreditationExpiration { get; set; }
        public DateTime? CyberLiabilityExpirationifapplicable { get; set; }
        public CyberLiabilityStatusifApplicableCEnum? CyberLiabilityStatusifapplicable { get; set; }
        public DisclosureFormStatusCEnum? DisclosureFormStatus { get; set; }
    }
}
