using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Controllers.Services;
using SalesforceAPI.Data;
using SalesforceAPI.Dtos;
using SalesforceAPI.Models;

namespace SalesforceAPI.Controllers
{
    [ApiController]
    public class OrgCredentialingProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly ILogger<OrgCredentialingProfileController> _logger;
        public OrgCredentialingProfileController(ApplicationDbContext context, ProviderService providerService, ILogger<OrgCredentialingProfileController> logger)
        {
            _context = context;
            _providerService = providerService;
            _logger = logger;
        }

        // GET: OrgCredentialingProfileFull/5
        [HttpGet("OrgCredentialingProfileFull/{credentialingProfileId}")]
        public async Task<ActionResult> GetOrgCredentialingProfileFull(string credentialingProfileId)
        {
            try
            {
                int? providerId = await _providerService.GetProviderIdAsync(credentialingProfileId);

                if (providerId.HasValue)
                {
                    var credentialingContact = await _context.CredentialingContacts.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var serviceLocation = await _context.ServiceLocations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var serviceLocationLicense = await _context.ServiceLocationLicenses.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var organizationalPSV = await _context.OrganizationalPrimarySourceVerifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var directService = await _context.DirectServices.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var orgCredentialingProfile = await _context.OrganizationalCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);

                    var credentialingContactBody = new CredentialingContactDto
                    {
                        Credentialing_Profile_Id__c = credentialingProfileId,
                        Contact_First_Name__c = credentialingContact.ContactFirstName,
                        Contact_Last_Name__c = credentialingContact.ContactLastName,
                        Contact_Email__c = credentialingContact.ContactEmail,
                        Contact_Person_Role__c = credentialingContact.ContactPersonRole,
                        Contact_Phone__c = credentialingContact.ContactPhone,
                        Primary_Contact__c = credentialingContact.PrimaryContact
                    };

                    var serviceLocationBody = new ServiceLocationDto
                    {
                        Account_Site__c = serviceLocation.AccountSite,
                        Account__c = serviceLocation.Account,
                        Credentialing_Profile__c = serviceLocation.CredentialingProfileId,
                        Facility_License_Expiration_if_applicab__c = serviceLocation.FacilityLicenseExpirationifapplicab,
                        Facility_License_if_applicable__c = serviceLocation.FacilityLicenseifapplicable,
                        Facility_License_Number_if_applicable__c = serviceLocation.FacilityLicenseNumberifapplicable,
                        Facility_License_Status_if_applicable__c = serviceLocation.FacilityLicenseStatusifapplicable,
                        Hours_of_Operation__c = serviceLocation.HoursofOperation,
                        Accomodations_Accessibility__c = serviceLocation.AccomodationsAccessibility,
                        Accomodations_Accessibility_Other__c = serviceLocation.AccomodationsAccessibilityOther,
                        Licensed_Facility__c = serviceLocation.LicensedFacility
                    };

                    var serviceLocationLicenseBody = new ServiceLocationLicenseDto
                    {
                        Service_Locations__c = serviceLocationLicense.ServiceLocations,
                        Service_Name__c = serviceLocationLicense.ServiceName,
                        Credentialing_Profile__c = serviceLocationLicense.CredentialingProfileId,
                        Service_License_Expiration_if_applicabl__c = serviceLocationLicense.ServiceLicenseExpirationifapplicabl,
                        Service_License_Number_if_applicable__c = serviceLocationLicense.ServiceLicenseNumberifapplicable,
                        Service_License_Status_if_applicable__c = serviceLocationLicense.ServiceLicenseStatusifapplicable
                    };

                    var organizationalPSVBody = new OrganizationalPrimarySourceVerificationDto
                    {
                        Credentialing_Profile__c = organizationalPSV.CredentialingProfile,
                        Primary_Source_Verifier__c = organizationalPSV.PrimarySourceVerifier,
                        CVO__c = organizationalPSV.CVO,
                        Other_Accred__c = organizationalPSV.OtherAccred,
                        Verifier_s_Credentialing_Organization__c = organizationalPSV.VerifiersCredentialingOrganization,
                        LARA_License__c = organizationalPSV.LARALicense,
                        LARA_Uploaded__c = organizationalPSV.LARAUploaded,
                        Proof_of_Accreditation__c = organizationalPSV.ProofofAccreditation,
                        Disciplinary_status_with_regulatory_boar__c = organizationalPSV.Disciplinarystatuswithregulatoryboar,
                        MDHHS_Sanctioned_Provider_Check__c = organizationalPSV.MDHHSSanctionedProviderCheck,
                        At_least_five_year_history_of_organizati__c = organizationalPSV.Atleastfiveyearhistoryoforganizati,
                        On_Site_Quality_Assessment_Recredential__c = organizationalPSV.OnSiteQualityAssessmentRecredential,
                        Office_of_Inspector_General_Check__c = organizationalPSV.OfficeofInspectorGeneralCheck,
                        SAM_gov_Check__c = organizationalPSV.SAMgovCheck
                    };

                    var directServiceBody = new DirectServiceDto
                    {
                        Operator__c = directService.Operator,
                        Service__c = directService.Service,
                        is_Certification__c = directService.IsCertification
                    };

                    var orgCredentialingProfileBody = new OrganizationalCredentialingProfileDto
                    {
                        Accreditation_End__c = orgCredentialingProfile.AccreditationEnd,
                        Accreditation_Start__c = orgCredentialingProfile.AccreditationStart,
                        Accrediting_Body__c = orgCredentialingProfile.AccreditingBody,
                        Proof_of_Accreditation_Status__c = orgCredentialingProfile.ProofofAccreditationStatus,
                        When_was_the_last_accreditation_survey__c = orgCredentialingProfile.Whenwasthelastaccreditationsurvey,
                        Date_of_Birth__c = orgCredentialingProfile.DateofBirth,
                        Tax_ID__c = orgCredentialingProfile.TaxID,
                        Medicare_Provider__c = orgCredentialingProfile.MedicareProvider,
                        Medicare_Number__c = orgCredentialingProfile.MedicareNumber,
                        Practitioner_NPI__c = orgCredentialingProfile.NPI,
                        Office_Address_Street_1__c = orgCredentialingProfile.OfficeAddressStreet1,
                        Office_Address_City__c = orgCredentialingProfile.OfficeAddressCity,
                        Office_Address_Zipcode__c = orgCredentialingProfile.OfficeAddressZipcode,
                        Office_Address_State__c = orgCredentialingProfile.OfficeAddressState,
                        Office_Address_County__c = orgCredentialingProfile.OfficeAddressCounty,
                        Home_Address_Street_1__c = orgCredentialingProfile.HomeAddressStreet1,
                        Home_Address_City__c = orgCredentialingProfile.HomeAddressCity,
                        Home_Address_Zipcode__c = orgCredentialingProfile.HomeAddressZipcode,
                        Home_Address_State__c = orgCredentialingProfile.HomeAddressState,
                        Home_Address_County__c = orgCredentialingProfile.HomeAddressCounty,
                        Specialties__c = orgCredentialingProfile.Specialties,
                        Specialty_Other__c = orgCredentialingProfile.OtherSpecialty,
                        Certificate_of_Liability__c = orgCredentialingProfile.CertificateofLiability,
                        Certificate_of_Liability_Expiration_Date__c = orgCredentialingProfile.CertificateofLiabilityExpirationDate,
                        Languages_Spoken__c = orgCredentialingProfile.LanguagesSpoken,
                        Cultural_Competences__c = orgCredentialingProfile.CulturalCompetences,
                        X6_month_gap_in_employment_since_profess__c = orgCredentialingProfile.X6monthgapinemploymentsinceprofess,
                        X6_Month_Gap_Start_Date__c = orgCredentialingProfile.X6MonthGapStartDate,
                        X6_Month_Gap_End_Date__c = orgCredentialingProfile.X6MonthGapEndDate,
                        X6_Month_Gap_Activity__c = orgCredentialingProfile.X6MonthGapActivity,
                        X6_Month_Gap_Reason__c = orgCredentialingProfile.X6MonthGapReason,
                        Please_acknowledge_If_denied_credential__c = orgCredentialingProfile.PleaseacknowledgeIfdeniedcredential,
                        Can_you_perform_the_essential_duties_of__c = orgCredentialingProfile.Canyouperformtheessentialdutiesof,
                        Lack_of_present_illegal_drug_use__c = orgCredentialingProfile.Lackofpresentillegaldruguse,
                        History_of_loss_of_license__c = orgCredentialingProfile.Historyoflossoflicense,
                        History_of_felony_convictions__c = orgCredentialingProfile.Historyoffelonyconvictions,
                        History_of_loss_or_limitations_of_privil__c = orgCredentialingProfile.Historyoflossorlimitationsofprivil,
                        ExplanationHistory_of_felony_convictions__c = orgCredentialingProfile.ExplanationHistoryoffelonyconvictions,
                        Current_Malpractice_Insurance_Coverage__c = orgCredentialingProfile.CurrentMalpracticeInsuranceCoverage,
                        Attestation_by_the_applicant_of_the_corr__c = orgCredentialingProfile.Attestationbytheapplicantofthecorr,
                        Office_Phone_Number__c = orgCredentialingProfile.OfficePhoneNumber,
                        Email__c = orgCredentialingProfile.Email,
                        Cultural_Competencies_Picklist__c = orgCredentialingProfile.CulturalCompetenciesPicklist,
                        W9_Expiration__c = orgCredentialingProfile.W9Expiration,
                        W9_Status__c = orgCredentialingProfile.W9Status,
                        Disclosure_Form_Expiration__c = orgCredentialingProfile.DisclosureFormExpiration,
                        Professional_Liability_Expiration__c = orgCredentialingProfile.ProfessionalLiabilityExpiration,
                        Commercial_Liability_Expiration__c = orgCredentialingProfile.CommercialLiabilityExpiration,
                        Workers_Compensation_Expiration__c = orgCredentialingProfile.WorkersCompensationExpiration,
                        Five_year_history_org_liability__c = orgCredentialingProfile.Fiveyearhistoryorgliability,
                        Explanation_five_year_history__c = orgCredentialingProfile.Explanationfiveyearhistory,
                        Action_pending_org_license__c = orgCredentialingProfile.Actionpendingorglicense,
                        Explanation_org_license__c = orgCredentialingProfile.Explanationorglicense,
                        Org_accreditation_limited__c = orgCredentialingProfile.Orgaccreditationlimited,
                        Explanation_org_accreditation_limited__c = orgCredentialingProfile.Explanationorgaccreditationlimited,
                        Action_pending_current_accreditation__c = orgCredentialingProfile.Actionpendingcurrentaccreditation,
                        Explanation_current_accreditation__c = orgCredentialingProfile.Explanationcurrentaccreditation,
                        Org_medicaid_sanctions__c = orgCredentialingProfile.Orgmedicaidsanctions,
                        Org_medicare_sanctions__c = orgCredentialingProfile.Orgmedicaresanctions,
                        Explanation_org_medicaid_sanctions__c = orgCredentialingProfile.Explanationorgmedicaidsanctions,
                        Explanation_org_medicare_sanctions__c = orgCredentialingProfile.Explanationorgmedicaresanctions,
                        Org_insurance_initially_refused__c = orgCredentialingProfile.Orginsuranceinitiallyrefused,
                        Explanation_org_insurance_initially_refu__c = orgCredentialingProfile.Explanationorginsuranceinitiallyrefused,
                        Org_defendant_SUD_payment__c = orgCredentialingProfile.OrgdefendantSUDpayment,
                        Explanation_org_defendant_SUD_payment__c = orgCredentialingProfile.ExplanationorgdefendantSUDpayment,
                        Org_malpractice_claims_SUD__c = orgCredentialingProfile.OrgmalpracticeclaimsSUD,
                        Explanation_org_malpractice_claims_SUD__c = orgCredentialingProfile.ExplanationorgmalpracticeclaimsSUD,
                        Please_indicate_if_you_have_a_speciality__c = orgCredentialingProfile.Pleaseindicateifyouhaveaspeciality,
                        Proof_of_Accreditation__c = orgCredentialingProfile.ProofofAccreditation,
                        Cyber_Liability_if_applicable__c = orgCredentialingProfile.CyberLiabilityifapplicable,
                        Commercial_Liability__c = orgCredentialingProfile.CommercialLiability,
                        Proof_of_Accreditation_Expiration__c = orgCredentialingProfile.ProofofAccreditationExpiration,
                        Cyber_Liability_Expiration_if_applicabl__c = orgCredentialingProfile.CyberLiabilityExpirationifapplicable,
                        Cyber_Liability_Status_if_applicable__c = orgCredentialingProfile.CyberLiabilityStatusifapplicable,
                        Disclosure_Form_Status__c = orgCredentialingProfile.DisclosureFormStatus
                    };

                    var orgCredentialingProfileFullBody = new OrgCredentialProfileFullDto
                    {
                        CredentialingContactDtos = credentialingContactBody,
                        ServiceLocationDtos = serviceLocationBody,
                        ServiceLocationLicenseDtos = serviceLocationLicenseBody,
                        OrganizationalPrimarySourceVerificationDtos = organizationalPSVBody,
                        DirectServiceDtos = directServiceBody,
                        OrganizationalCredentialingProfileDtos = orgCredentialingProfileBody
                    };

                    return new JsonResult(orgCredentialingProfileFullBody);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the education record.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
