using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using FoolProof.Core;
using FinalLaboratorio4.Validators;

namespace FinalLaboratorio4.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        [Required]
        [Range(0, 999999999.99)]
        public decimal Precio { get; set; }

        [MaxLength(500)]
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        // requerida solo si UrlImagen no esta vacio, es decir cuando se crea y no cuando se edita
        [RequiredIfEmpty("UrlImagen", ErrorMessage = "Imagen requirida.")]
        [NotMapped]
        [AllowedExtensions(".png", ".jpeg", ".jpg")]
        [MinFileSize]
        [MaxFileSize(8 * 1024 * 1024)] // 8 mb
        public IFormFile Imagen { get; set; }

        [MaxLength(2000)]
        public string UrlImagen { get; set; }

        public bool Favorito { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        public ICollection<Proveedor> Proveedores { get; set; }
    }
}
