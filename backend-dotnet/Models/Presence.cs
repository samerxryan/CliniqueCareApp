using System.ComponentModel.DataAnnotations;

namespace backend_dotnet.Models
{
    public class Presence
    {
        [Key]
        public long Id { get; set; }
        public string Employe { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string Status { get; set; } = string.Empty; // Présent / Absent
    }
}
