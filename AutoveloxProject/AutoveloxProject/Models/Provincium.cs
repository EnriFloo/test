using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxProject.Models;

public partial class Provincium
{
    [Key]
    public int Id { get; set; }

    public int IdRegione { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Denominazione { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? Sigla { get; set; }

    [InverseProperty("IdProvinciaNavigation")]
    public virtual ICollection<Comune> Comunes { get; set; } = new List<Comune>();

    [ForeignKey("IdRegione")]
    [InverseProperty("Provincia")]
    public virtual Regione IdRegioneNavigation { get; set; } = null!;
}
