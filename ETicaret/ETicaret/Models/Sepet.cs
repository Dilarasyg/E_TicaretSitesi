using System;
using System.Collections.Generic;

namespace ETicaret.Models;

public partial class Sepet
{
    public int SepetId { get; set; }

    public int? UyeId { get; set; }

    public int? UrunId { get; set; }

    public int? Adet { get; set; }

    public DateTime? EklenmeTarihi { get; set; }

    public virtual Urun? Urun { get; set; }

    public virtual Uye? Uye { get; set; }
}
