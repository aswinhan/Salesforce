using System.ComponentModel.DataAnnotations;

namespace SalesforceWeb.Models
{
    public class DirectService
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Operator { get; set; }
        public string Service { get; set; }
        public bool? IsCertification { get; set; }
    }
}
