using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FinalLaboratorio4.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [MaxLength(20)]
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
