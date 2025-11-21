using System.ComponentModel.DataAnnotations;

namespace backend_dotnet.Models
{
    public class Leave
    {
        [Key]
        public long Id { get; set; }
        public string Employe { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Annuel / Maladie
        public DateTime? Debut { get; set; }
        public DateTime? Fin { get; set; }
    }
}
