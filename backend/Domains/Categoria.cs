﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Classe responsável pelo modelo da categoria
/// </summary>
namespace backend.Domains
{
    public partial class Categoria
    {
        public Categoria()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [Column("Categoria_id")]
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
