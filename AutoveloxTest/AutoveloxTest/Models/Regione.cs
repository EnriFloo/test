using System;
using System.Collections.Generic;

namespace AutoveloxTest.Models;

public partial class Regione
{
    public int Id { get; set; }

    public int IdRipartizioneGeografica { get; set; }

    public string Denominazione { get; set; } = null!;

    public virtual RipartizioneGeografica IdRipartizioneGeograficaNavigation { get; set; } = null!;

    public virtual ICollection<Provincium> Provincia { get; set; } = new List<Provincium>();
}
