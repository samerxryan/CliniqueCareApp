using System.ComponentModel.DataAnnotations;

namespace backend_dotnet.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Cin { get; set; } = string.Empty;
        public DateTime? DateNaissance { get; set; }
        public string Poste { get; set; } = string.Empty;
        public string Salaire { get; set; } = string.Empty;
    }
}
