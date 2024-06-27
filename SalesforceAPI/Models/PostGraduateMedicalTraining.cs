using System.ComponentModel.DataAnnotations;

namespace SalesforceAPI.Models
{
    public class PostGraduateMedicalTraining
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? CredentialingProfileId { get; set; }
        public string? MedicalTrainingHospitalAddress { get; set; }
        public string? MedicalTrainingHospitalName { get; set; }
        public string? MedicalTrainingType { get; set; }
        public string? Specialty { get; set; }
        public DateTime? TrainingEndDate { get; set; }
        public DateTime? TrainingStartDate { get; set; }
    }
}
