using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace rent.Models
{
    public partial class RentCarContext : DbContext
    {
        public RentCarContext()
        {
        }

        public RentCarContext(DbContextOptions<RentCarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arac> Aracs { get; set; }
        public virtual DbSet<KiralamaBilgi> KiralamaBilgis { get; set; }
        public virtual DbSet<Musteri> Musteris { get; set; }
        public virtual DbSet<MusteriSirket> MusteriSirkets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RentCar;uid=sa;pwd=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Arac>(entity =>
            {
                entity.ToTable("arac");

                entity.Property(e => e.AracId).HasColumnName("aracID");

                entity.Property(e => e.GunlukKiraUcret).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Marka)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("marka");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.Plaka)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Renk)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Yil).HasColumnName("yil");
            });

            modelBuilder.Entity<KiralamaBilgi>(entity =>
            {
                entity.ToTable("kiralamaBilgi");

                entity.Property(e => e.KiralamaBilgiId).HasColumnName("kiralamaBilgiID");

                entity.Property(e => e.AracId).HasColumnName("AracID");

                entity.Property(e => e.HasarDurum).HasColumnName("hasarDurum");

                entity.Property(e => e.KiraGunu)
                    .HasColumnType("datetime")
                    .HasColumnName("kiraGunu");

                entity.Property(e => e.KiralamaGun).HasColumnName("kiralamaGun");

                entity.Property(e => e.MusteriId).HasColumnName("MusteriID");

                entity.Property(e => e.MusteriSirketId).HasColumnName("musteriSirketID");

                entity.Property(e => e.TeslimGunu)
                    .HasColumnType("datetime")
                    .HasColumnName("teslimGunu");

                entity.Property(e => e.ToplamUcret).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Arac)
                    .WithMany(p => p.KiralamaBilgis)
                    .HasForeignKey(d => d.AracId)
                    .HasConstraintName("FK__kiralamaB__AracI__3C69FB99");

                entity.HasOne(d => d.Musteri)
                    .WithMany(p => p.KiralamaBilgis)
                    .HasForeignKey(d => d.MusteriId)
                    .HasConstraintName("FK__kiralamaB__Muste__3D5E1FD2");

                entity.HasOne(d => d.MusteriSirket)
                    .WithMany(p => p.KiralamaBilgis)
                    .HasForeignKey(d => d.MusteriSirketId)
                    .HasConstraintName("FK__kiralamaB__muste__49C3F6B7");
            });

            modelBuilder.Entity<Musteri>(entity =>
            {
                entity.ToTable("musteri");

                entity.Property(e => e.MusteriId).HasColumnName("musteriID");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ad");

                entity.Property(e => e.Eposta)
                    .HasMaxLength(50)
                    .HasColumnName("eposta");

                entity.Property(e => e.Soyad)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("soyad");

                entity.Property(e => e.TcKimlik)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("TC_kimlik");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("telefon");
            });

            modelBuilder.Entity<MusteriSirket>(entity =>
            {
                entity.ToTable("musteriSirket");

                entity.Property(e => e.MusteriSirketId).HasColumnName("musteriSirketID");

                entity.Property(e => e.IlgiliKisi)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ilgiliKisi");

                entity.Property(e => e.SirketEposta).HasMaxLength(50);

                entity.Property(e => e.Sirkettelefon)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Unvan)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VergiNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
