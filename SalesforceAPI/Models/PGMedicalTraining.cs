using System.ComponentModel.DataAnnotations;
using static SalesforceAPI.Dtos.PGMedicalTrainingDto;

namespace SalesforceAPI.Models
{
    public class PGMedicalTraining
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? CredentialingProfileId { get; set; }
        public string? MedicalTrainingHospitalAddress { get; set; }
        public string? MedicalTrainingHospitalName { get; set; }
        public MedicalTrainingTypeCEnum? MedicalTrainingType { get; set; }
        public string? Specialty { get; set; }
        public DateTime? TrainingEndDate { get; set; }
        public DateTime? TrainingStartDate { get; set; }
    }
}
