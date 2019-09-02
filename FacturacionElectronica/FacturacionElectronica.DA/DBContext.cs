namespace FacturacionElectronica.DA
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using FacturacionElectronica.Entities;
    

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

       // public virtual DbSet<Usuario_Rol> Usuario_Roles { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Accion> Accion { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Producto)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.idCategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciudad>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Ciudad>()
                .Property(e => e.codigoPostal)
                .IsUnicode(false);

            modelBuilder.Entity<Ciudad>()
                .HasMany(e => e.Cliente)
                .WithRequired(e => e.Ciudad)
                .HasForeignKey(e => e.idCiudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.documento)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Factura)
                .WithRequired(e => e.Cliente)
                .HasForeignKey(e => e.idCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Factura>()
                .HasMany(e => e.DetalleFactura)
                .WithRequired(e => e.Factura)
                .HasForeignKey(e => e.idFactura)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pais>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Pais>()
                .HasMany(e => e.Ciudad)
                .WithRequired(e => e.Pais)
                .HasForeignKey(e => e.idPais)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.foto)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.DetalleFactura)
                .WithRequired(e => e.Producto)
                .HasForeignKey(e => e.idProducto)
                .WillCascadeOnDelete(false);

           
            modelBuilder.Entity<Accion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Accion>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Accion>()
                .Property(e => e.ruta)
                .IsUnicode(false);

            modelBuilder.Entity<Accion>()
                .HasMany(e => e.Rol)
                .WithMany(e => e.Accion)
                .Map(m => m.ToTable("Rol_Accion","seguridad").MapLeftKey("idAccion").MapRightKey("idRol"));
               
            modelBuilder.Entity<Empleado>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.apellidos)
                .IsUnicode(false);
            /*
            modelBuilder.Entity<Empleado>()
                .Property(e => e.foto)
                .IsUnicode(false);
                */
/*
            modelBuilder.Entity<Rol>()
   .HasMany(b => b.Accion)
   .WithMany(c => c.Rol)
   .Map(cs =>
   {
       cs.MapLeftKey("idUsuario");
       cs.MapRightKey("idRol");
       cs.ToTable("Usuario_Rol", "seguridad");
   });
   */
            
            modelBuilder.Entity<Rol>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuario)
                .WithMany(e => e.Rol)
                .Map(m => m.ToTable("Usuario_Rol", "seguridad").MapLeftKey("idRol").MapRightKey("idUsuario"));

            modelBuilder.Entity<Usuario>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Factura)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.idVendedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Empleado)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.idUsuario)
                .WillCascadeOnDelete(false);
        }
    }
}
