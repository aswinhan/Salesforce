using System.ComponentModel.DataAnnotations;

namespace SalesforceWeb.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string CollegeUniversityProgramAddress { get; set; }
        public string CollegeUniversityProgramName { get; set; }
        public string CredentialingProfileID { get; set; }
        public string Degree { get; set; }
        public DateTime? GraduationDate { get; set; }
    }
}
