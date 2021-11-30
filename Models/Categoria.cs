using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FinalLaboratorio4.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        [DisplayName("Descripción")]
        [Remote(
            "IsDescriptionValid",
            "Categorias",
            AdditionalFields = "Id",
            ErrorMessage = "La categoría ya existe."
        )]
        public string Descripcion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
