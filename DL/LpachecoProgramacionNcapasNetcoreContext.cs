using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class LpachecoProgramacionNcapasNetcoreContext : DbContext
{
    public LpachecoProgramacionNcapasNetcoreContext()
    {
    }

    public LpachecoProgramacionNcapasNetcoreContext(DbContextOptions<LpachecoProgramacionNcapasNetcoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoInventario> ProductoInventarios { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioFecha> UsuarioFechas { get; set; }

    public virtual DbSet<UsuarioProducto> UsuarioProductos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= LPachecoProgramacionNCapasNETCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433DBEBC202D");

            entity.ToTable("Departamento");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A887863D86D6");

            entity.ToTable("Marca");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210F47FF636");

            entity.ToTable("Producto");

            entity.Property(e => e.CodigoBarras)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Producto__IdDepa__1DE57479");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Producto__IdMarc__1ED998B2");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Producto__IdProv__1FCDBCEB");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__Producto__IdUsua__20C1E124");
        });

        modelBuilder.Entity<ProductoInventario>(entity =>
        {
            entity.HasKey(e => e.IdProductoInventario).HasName("PK__Producto__E388F2A2F2167B3E");

            entity.ToTable("ProductoInventario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoInventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__ProductoI__IdPro__286302EC");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF9DE2EFD2");

            entity.ToTable("Proveedor");

            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97657DFE53");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.EmailEmpresarial, "UQ__Usuario__539E972D4533545B").IsUnique();

            entity.HasIndex(e => e.Curp, "UQ__Usuario__F46C4CBF6FCF5A29").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.EmailEmpresarial)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.InverseIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__Usuario__IdUsuar__1273C1CD");
        });

        modelBuilder.Entity<UsuarioFecha>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioFecha).HasName("PK__UsuarioF__EE6BEFEDC1A8C2C9");

            entity.ToTable("UsuarioFecha");

            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioFechas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__UsuarioFe__IdUsu__15502E78");
        });

        modelBuilder.Entity<UsuarioProducto>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioProducto).HasName("PK__UsuarioP__0061B623B6DD964D");

            entity.ToTable("UsuarioProducto");

            entity.Property(e => e.FechaAsignacion).HasColumnType("date");
            entity.Property(e => e.FechaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.UsuarioProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__UsuarioPr__IdPro__24927208");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioProductoIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__UsuarioPr__IdUsu__239E4DCF");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.UsuarioProductoIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__UsuarioPr__IdUsu__25869641");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
