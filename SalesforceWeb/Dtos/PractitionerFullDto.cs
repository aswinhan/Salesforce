namespace SalesforceWeb.Dtos
{
    public class PractitionerFullDto
    {
        public PractitionerCPDto PractitionerCPDtos { get; set; }
        public EducationDto EducationDtos { get; set; }
        public PGMedicalTrainingDto PGMedicalTrainingDtos { get; set; }
        public HospitalAffiliationDto HospitalAffiliationDtos { get; set; }
        public PractitionerLCDto PractitionerLCDtos { get; set; }
        public PractitionerPSVDto practitionerPSVDtos { get; set; }
    }
}
