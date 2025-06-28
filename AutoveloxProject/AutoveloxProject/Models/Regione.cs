using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxProject.Models;

[Table("Regione")]
public partial class Regione
{
    [Key]
    public int Id { get; set; }

    public int IdRipartizioneGeografica { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Denominazione { get; set; } = null!;

    [ForeignKey("IdRipartizioneGeografica")]
    [InverseProperty("Regiones")]
    public virtual RipartizioneGeografica IdRipartizioneGeograficaNavigation { get; set; } = null!;

    [InverseProperty("IdRegioneNavigation")]
    public virtual ICollection<Provincium> Provincia { get; set; } = new List<Provincium>();
}
