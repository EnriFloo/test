using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxProject.Models;

[Table("RipartizioneGeografica")]
public partial class RipartizioneGeografica
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Denominazione { get; set; } = null!;

    [InverseProperty("IdRipartizioneGeograficaNavigation")]
    public virtual ICollection<Regione> Regiones { get; set; } = new List<Regione>();
}
