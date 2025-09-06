using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Carte
    {
        [Key]
        public int CarteID { get; set; }

        [Required]
        public string Titlu { get; set; }

        [Required]
        public string Autor { get; set; }

        public DateTime? DataPub { get; set; }

        public int? Volum { get; set; }
    }
}
