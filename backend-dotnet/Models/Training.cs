using System.ComponentModel.DataAnnotations;

namespace backend_dotnet.Models
{
    public class Training
    {
        [Key]
        public long Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Employe { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
    }
}
