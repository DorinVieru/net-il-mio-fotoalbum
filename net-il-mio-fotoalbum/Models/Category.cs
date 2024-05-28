using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }

        [StringLength(200, ErrorMessage = "Il nome della categoria può avere un massimo di 200 caratteri.")]
        [Required(ErrorMessage = "Il nome della categoria è obbligatorio.")]
        public string Name { get; set; }
        public List<Photo>? photos { get; set; }

        public Category() { }
    }
}
