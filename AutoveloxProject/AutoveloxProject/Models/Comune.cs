using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxProject.Models;

[Table("Comune")]
public partial class Comune
{
    [Key]
    public int IdComune { get; set; }

    public int? IdProvincia { get; set; }

    [StringLength(255)]
    public string? CodiceComune { get; set; }

    [StringLength(255)]
    public string? Denominazione { get; set; }

    [StringLength(10)]
    public string? CodiceCatastale { get; set; }

    public int? CapoluogoProvincia { get; set; }

    [StringLength(50)]
    public string? ZonaAltimetrica { get; set; }

    public int? AltitudineCentro { get; set; }

    public int? ComuneLitoraneo { get; set; }

    [StringLength(50)]
    public string? ComuneMontano { get; set; }

    [StringLength(50)]
    public string? SuperficieTerritoriale { get; set; }

    public int? Popolazione2001 { get; set; }

    public int? Popolazione2011 { get; set; }

    [ForeignKey("IdProvincia")]
    [InverseProperty("Comunes")]
    public virtual Provincium? IdProvinciaNavigation { get; set; }

    [InverseProperty("IdComuneNavigation")]
    public virtual ICollection<Mappa> Mappas { get; set; } = new List<Mappa>();
}
