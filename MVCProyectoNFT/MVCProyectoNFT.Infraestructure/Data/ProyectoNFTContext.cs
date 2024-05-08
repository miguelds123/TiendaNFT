using System;
using System.Collections.Generic;
using MVCProyectoNFT.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCProyectoNFT.Infraestructure.Data;

public partial class ProyectoNFTContext : DbContext
{
    public ProyectoNFTContext(DbContextOptions<ProyectoNFTContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<ClienteNft> ClienteNft { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalle { get; set; }

    public virtual DbSet<FacturaEncabezado> FacturaEncabezado { get; set; }

    public virtual DbSet<Nft> Nft { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<TipoTarjeta> TipoTarjeta { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaN).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Cliente)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Pais");
        });

        modelBuilder.Entity<ClienteNft>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClienteNFT_1");

            entity.ToTable("ClienteNFT");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdNft)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IdNFT");
            entity.Property(e => e.NombreNft)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreNFT");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteNft)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteNFT_Cliente");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.ClienteNft)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_ClienteNFT_FacturaEncabezado");

            entity.HasOne(d => d.IdNftNavigation).WithMany(p => p.ClienteNft)
                .HasForeignKey(d => d.IdNft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteNFT_NFT");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdFactura, e.IdDetalle });

            entity.Property(e => e.IdNft)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IdNFT");
            entity.Property(e => e.Precio).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaDetalle)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalle_FacturaEncabezado");

            entity.HasOne(d => d.IdNftNavigation).WithMany(p => p.FacturaDetalle)
                .HasForeignKey(d => d.IdNft)
                .HasConstraintName("FK_FacturaDetalle_NFT");
        });

        modelBuilder.Entity<FacturaEncabezado>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroTarjeta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.FacturaEncabezado)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_FacturaEncabezado_Cliente");

            entity.HasOne(d => d.IdTipoTarjetaNavigation).WithMany(p => p.FacturaEncabezado)
                .HasForeignKey(d => d.IdTipoTarjeta)
                .HasConstraintName("FK_FacturaEncabezado_TipoTarjeta");
        });

        modelBuilder.Entity<Nft>(entity =>
        {
            entity.ToTable("NFT");

            entity.Property(e => e.Id)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("numeric(18, 2)");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoTarjeta>(entity =>
        {
            entity.HasKey(e => e.IdTipoTarjeta);

            entity.Property(e => e.Descrpcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.NombreUsuario);

            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasenna)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_TipoUsuario");
        });
        modelBuilder.HasSequence("ReceiptNumber")
            .StartsAt(0L)
            .HasMin(0L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
