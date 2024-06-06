using System;
using System.Collections.Generic;

namespace ETicaret.Models;

public partial class Urun
{
    public int UrunId { get; set; }

    public string? Adi { get; set; }

    public string? Aciklama { get; set; }

    public int? Fiyat { get; set; }

    public string? Anasayfa { get; set; }

    public string? Stok { get; set; }

    public int KategoriId { get; set; }

    public virtual Kategori Kategori { get; set; } = null!;

    public virtual ICollection<Resim> Resims { get; set; } = new List<Resim>();

    public virtual ICollection<Sepet> Sepets { get; set; } = new List<Sepet>();

    public virtual ICollection<SoruYorum> SoruYorums { get; set; } = new List<SoruYorum>();
}
