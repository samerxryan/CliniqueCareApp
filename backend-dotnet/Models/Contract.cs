using System.ComponentModel.DataAnnotations;

namespace backend_dotnet.Models
{
    public class Contract
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; } = string.Empty; // CDD / CDI
        public string Employe { get; set; } = string.Empty;
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
    }
}
