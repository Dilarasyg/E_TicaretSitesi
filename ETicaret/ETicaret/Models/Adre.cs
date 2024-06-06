using System;
using System.Collections.Generic;

namespace ETicaret.Models;

public partial class Adre
{
    public int AdresId { get; set; }

    public string? Adress { get; set; }

    public int? UyeId { get; set; }

    public virtual ICollection<Sipariss> Siparisses { get; set; } = new List<Sipariss>();

    public virtual Uye? Uye { get; set; }
}
