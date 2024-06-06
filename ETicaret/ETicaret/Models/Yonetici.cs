using System;
using System.Collections.Generic;

namespace ETicaret.Models;

public partial class Yonetici
{
    public int Id { get; set; }

    public string? KullaniciAdi { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Durum { get; set; }
}
