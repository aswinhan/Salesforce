namespace SalesforceAPI.Models
{
    public class PractitionerFull
    {
        public PractitionerCP? PractitionerCPs { get; set; }
        public Education? Educations { get; set; }
        public PGMedicalTraining? PGMedicalTrainings { get; set; }
        public HospitalAffiliation? HospitalAffiliations { get; set; }
        public PractitionerLC? PractitionerLCs { get; set; }
        public PractitionerPSV? practitionerPSVs { get; set; }
    }
}
