using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxProject.Models;

[Table("Mappa")]
public partial class Mappa
{
    [Key]
    public int Id { get; set; }

    public int? IdComune { get; set; }

    [StringLength(50)]
    public string? Nome { get; set; }

    public int AnnoInserimento { get; set; }

    public DateTime DataOraInserimento { get; set; }

    public double IdentificatoreOpenStreetMap { get; set; }

    public double Longitudine { get; set; }

    public double Latitudine { get; set; }

    [ForeignKey("IdComune")]
    [InverseProperty("Mappas")]
    public virtual Comune? IdComuneNavigation { get; set; }
}
