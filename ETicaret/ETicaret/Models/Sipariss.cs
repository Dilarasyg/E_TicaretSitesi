using System;
using System.Collections.Generic;

namespace ETicaret.Models;

public partial class Sipariss
{
    public int SiparisId { get; set; }

    public int? UyeId { get; set; }

    public decimal? ToplamTutar { get; set; }

    public int? AdresId { get; set; }

    public string? ÖdemeDurumu { get; set; }

    public DateTime? SiparisTarihi { get; set; }

    public DateTime? TeslimTarihi { get; set; }

    public virtual Adre? Adres { get; set; }

    public virtual Uye? Uye { get; set; }
}
