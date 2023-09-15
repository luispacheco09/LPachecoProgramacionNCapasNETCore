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

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoInventario> ProductoInventarios { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<SucursalProducto> SucursalProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioFecha> UsuarioFechas { get; set; }

    public virtual DbSet<UsuarioProducto> UsuarioProductos { get; set; }

    public virtual DbSet<VentaProducto> VentaProductos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=LPachecoProgramacionNCapasNETCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK__Area__2FC141AAFEBED6AD");

            entity.ToTable("Area");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D36392B73");

            entity.ToTable("Departamento");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Departame__IdAre__31EC6D26");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A8875755AEF1");

            entity.ToTable("Marca");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BEFEFA225E");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.Metodo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210C672390F");

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
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Producto__IdDepa__20C1E124");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Producto__IdMarc__21B6055D");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Producto__IdProv__22AA2996");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__Producto__IdUsua__239E4DCF");
        });

        modelBuilder.Entity<ProductoInventario>(entity =>
        {
            entity.HasKey(e => e.IdProductoInventario).HasName("PK__Producto__E388F2A22027B828");

            entity.ToTable("ProductoInventario");

            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoInventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__ProductoI__IdPro__2B3F6F97");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AFC19E86A2");

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

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__BFB6CD99BA104B73");

            entity.ToTable("Sucursal", tb => tb.HasTrigger("SucursalProductoAdd"));

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Colonia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CP");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SucursalProducto>(entity =>
        {
            entity.HasKey(e => e.IdSucursalProducto).HasName("PK__Sucursal__072D557E2F3A849E");

            entity.ToTable("SucursalProducto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SucursalProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__SucursalP__IdPro__300424B4");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.SucursalProductos)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK__SucursalP__IdSuc__30F848ED");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF971DE89AD0");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.EmailEmpresarial, "UQ__Usuario__539E972D8A3C068F").IsUnique();

            entity.HasIndex(e => e.Curp, "UQ__Usuario__F46C4CBF9066DD2D").IsUnique();

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

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Usuario__IdArea__145C0A3F");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.InverseIdUsuarioModificacionNavigation)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__Usuario__IdUsuar__15502E78");
        });

        modelBuilder.Entity<UsuarioFecha>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioFecha).HasName("PK__UsuarioF__EE6BEFED99339B3D");

            entity.ToTable("UsuarioFecha");

            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioFechas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__UsuarioFe__IdUsu__182C9B23");
        });

        modelBuilder.Entity<UsuarioProducto>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioProducto).HasName("PK__UsuarioP__0061B623EAE4BC70");

            entity.ToTable("UsuarioProducto");

            entity.Property(e => e.FechaAsignacion).HasColumnType("date");
            entity.Property(e => e.FechaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdProductoInventarioNavigation).WithMany(p => p.UsuarioProductos)
                .HasForeignKey(d => d.IdProductoInventario)
                .HasConstraintName("FK__UsuarioPr__IdPro__276EDEB3");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioProductoIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__UsuarioPr__IdUsu__267ABA7A");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.UsuarioProductoIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__UsuarioPr__IdUsu__286302EC");
        });

        modelBuilder.Entity<VentaProducto>(entity =>
        {
            entity.HasKey(e => e.IdVentaProducto).HasName("PK__VentaPro__E4CB5099A451E788");

            entity.ToTable("VentaProducto");

            entity.HasOne(d => d.IdSucursalProductoNavigation).WithMany(p => p.VentaProductos)
                .HasForeignKey(d => d.IdSucursalProducto)
                .HasConstraintName("FK__VentaProd__IdSuc__5535A963");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaProductos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__VentaProd__IdVen__5441852A");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__BC1240BDE8A97375");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdUser).HasMaxLength(450);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK__Venta__IdMetodoP__5165187F");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Venta__IdUser__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
