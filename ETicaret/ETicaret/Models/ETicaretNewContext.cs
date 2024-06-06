using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Models;

public partial class ETicaretNewContext : DbContext
{
    public ETicaretNewContext()
    {
    }

    public ETicaretNewContext(DbContextOptions<ETicaretNewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adre> Adres { get; set; }

    public virtual DbSet<Kategori> Kategoris { get; set; }

    public virtual DbSet<Resim> Resims { get; set; }

    public virtual DbSet<Sepet> Sepets { get; set; }

    public virtual DbSet<Sipariss> Siparisses { get; set; }

    public virtual DbSet<SoruYorum> SoruYorums { get; set; }

    public virtual DbSet<Urun> Uruns { get; set; }

    public virtual DbSet<Uye> Uyes { get; set; }

    public virtual DbSet<Yonetici> Yoneticis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Database=E-TicaretNew;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adre>(entity =>
        {
            entity.HasKey(e => e.AdresId);

            entity.Property(e => e.Adress)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Uye).WithMany(p => p.AdresNavigation)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_Adres_Uye");
        });

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity.ToTable("Kategori");

            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Resim>(entity =>
        {
            entity.HasKey(e => e.ResimId).HasName("PK_Galeri");

            entity.ToTable("Resim");

            entity.Property(e => e.Resim1)
                .HasColumnType("image")
                .HasColumnName("Resim");

            entity.HasOne(d => d.Urun).WithMany(p => p.Resims)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_Galeri_Urun");
        });

        modelBuilder.Entity<Sepet>(entity =>
        {
            entity.ToTable("Sepet");

            entity.Property(e => e.EklenmeTarihi).HasColumnType("datetime");

            entity.HasOne(d => d.Urun).WithMany(p => p.Sepets)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_Sepet_Urun");

            entity.HasOne(d => d.Uye).WithMany(p => p.Sepets)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_Sepet_Uye");
        });

        modelBuilder.Entity<Sipariss>(entity =>
        {
            entity.HasKey(e => e.SiparisId);

            entity.ToTable("Sipariss");

            entity.Property(e => e.SiparisTarihi).HasColumnType("datetime");
            entity.Property(e => e.TeslimTarihi).HasColumnType("datetime");
            entity.Property(e => e.ToplamTutar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ÖdemeDurumu)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Adres).WithMany(p => p.Siparisses)
                .HasForeignKey(d => d.AdresId)
                .HasConstraintName("FK_Sipariss_Adres");

            entity.HasOne(d => d.Uye).WithMany(p => p.Siparisses)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_Sipariss_Uye");
        });

        modelBuilder.Entity<SoruYorum>(entity =>
        {
            entity.HasKey(e => e.YorumId);

            entity.ToTable("SoruYorum");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.KontrolEdildiMi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Yorum).IsUnicode(false);
            entity.Property(e => e.YorumTarihSaati).HasColumnType("datetime");

            entity.HasOne(d => d.Urun).WithMany(p => p.SoruYorums)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_SoruYorum_Urun");

            entity.HasOne(d => d.Uye).WithMany(p => p.SoruYorums)
                .HasForeignKey(d => d.UyeId)
                .HasConstraintName("FK_SoruYorum_Uye");
        });

        modelBuilder.Entity<Urun>(entity =>
        {
            entity.ToTable("Urun");

            entity.Property(e => e.Aciklama).IsUnicode(false);
            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Anasayfa)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Stok)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Kategori).WithMany(p => p.Uruns)
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Urun_Kategori");
        });

        modelBuilder.Entity<Uye>(entity =>
        {
            entity.ToTable("Uye");

            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Adres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Il)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ilce)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PostaKodu)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sifre)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Soyadi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelefonNo).HasColumnType("decimal(11, 0)");
        });

        modelBuilder.Entity<Yonetici>(entity =>
        {
            entity.ToTable("Yonetici");

            entity.Property(e => e.Durum)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
