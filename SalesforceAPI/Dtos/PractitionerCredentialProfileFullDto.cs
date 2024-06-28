namespace SalesforceAPI.Dtos
{
    public class PractitionerCredentialProfileFullDto
    {
        public PractitionerCredentialingProfileDto? PractitionerCredentialingProfileDtos { get; set; }
        public EducationDto? EducationDtos { get; set; }
        public PostGraduateMedicalTrainingDto? PostGraduateMedicalTrainingDtos { get; set; }
        public HospitalAffiliationDto? HospitalAffiliationDtos { get; set; }
        public PractitionerLicensesCertificationDto? PractitionerLicensesCertificationDtos { get; set; }
        public PractitionerPrimarySourceVerificationDto? practitionerPrimarySourceVerificationDtos { get; set; }
    }
}
