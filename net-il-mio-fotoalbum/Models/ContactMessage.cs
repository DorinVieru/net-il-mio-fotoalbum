using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace net_il_mio_fotoalbum.Models
{
    public class ContactMessage
    {
        [Key] public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Il nome può avere un massimo di 50 caratteri.")]
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La mail è obbligatoria.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il messaggio è obbligatorio.")]
        [MinLength(10)]
        public string Message { get; set; }

        public ContactMessage() { }
    }
}
