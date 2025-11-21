using System.ComponentModel.DataAnnotations;

namespace backend_dotnet.Models
{
    public class Payroll
    {
        [Key]
        public long Id { get; set; }
        public string Employe { get; set; } = string.Empty;
        public string Mois { get; set; } = string.Empty;
        public string Montant { get; set; } = string.Empty;
    }
}
