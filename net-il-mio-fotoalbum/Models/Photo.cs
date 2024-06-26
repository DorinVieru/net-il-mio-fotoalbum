﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key] public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Il titolo può avere un massimo di 50 caratteri.")]
        [Required(ErrorMessage = "Il titolo è obbligatorio.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "La descrizione della foto può avere un massimo di 500 caratteri.")]
        [Required(ErrorMessage = "La descrizione della foto è obbligatoria.")]
        [MinLength(10)]
        public string Description { get; set; }
        public byte[]? ImgFile { get; set; }
        public string ImgSrc => ImgFile != null ? $"data:image/png;base64,{Convert.ToBase64String(ImgFile)}" : "";
        public bool Visible { get; set; } = true;
        public List<Category>? Categories { get; set; }

        [Required(ErrorMessage = "Indicare l'autore del contenuto è obbligatorio.")]
        public string AuthorId { get; set; }
        public IdentityUser? Author { get; set; }

        public Photo() { }
    }
}
