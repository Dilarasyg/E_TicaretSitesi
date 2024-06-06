using System;
using System.Collections.Generic;

namespace ETicaret.Models;

public partial class Resim
{
    public int ResimId { get; set; }

    public byte[]? Resim1 { get; set; }

    public int? UrunId { get; set; }

    public virtual Urun? Urun { get; set; }
}
