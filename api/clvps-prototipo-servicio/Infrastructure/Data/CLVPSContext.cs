using clvps_prototipo_servicio.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace clvps_prototipo_servicio.Infrastructure.Data
{
    public class CLVPSContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<VirtualAccount> VirtualAccounts { get; set; }
        public DbSet<OrdenPago> OrdenesPago { get; set; }
        public DbSet<TransactionPago> TransactionsPago { get; set; }
        public DbSet<ExchangeSpot> ExchangeSpots { get; set; }
        public DbSet<SystemManager> SystemManagers { get; set; }
        public DbSet<Recarga> Recargas { get; set; }
        public DbSet<TransactionRecarga> TransactionsRecarga { get; set; }
        public DbSet<SolicitudRetiro> SolicitudesRetiro { get; set; }
        public DbSet<TransactionRetiro> TransactionsRetiro { get; set; }
        public DbSet<Error> Errors { get; set; }

        public CLVPSContext(DbContextOptions<CLVPSContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de la entidad Buyer
            builder.Entity<Buyer>(entity =>
            {
                entity.ToTable("buyer");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DNI).HasColumnType("char(8)").HasMaxLength(8);
                entity.Property(e => e.CAR_EXT).HasColumnType("char(9)").HasMaxLength(9);
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<Buyer>(e => e.Id);
            });

            // Configuración de la entidad Seller
            builder.Entity<Seller>(entity =>
            {
                entity.ToTable("seller");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Raz_social).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RUC).HasColumnType("char(11)").HasMaxLength(11);
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<Seller>(e => e.Id);
            });

            // Configuración de la entidad VirtualAccount
            builder.Entity<VirtualAccount>(entity =>
            {
                entity.ToTable("virtual_account");
                entity.HasKey(e => new { e.Id, e.Id_user });
                entity.Property(e => e.Id_user).IsRequired();
                entity.Property(e => e.Nro_cuenta).IsRequired().HasColumnType("char(20)").HasMaxLength(20);
                entity.Property(e => e.Saldo_soles).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Saldo_dolares).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<VirtualAccount>(e => e.Id_user);
            });


            // Configuración de la entidad OrdenPago
            builder.Entity<OrdenPago>(entity =>
            {
                entity.ToTable("orden_pago");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Estado).IsRequired().HasColumnType("char(1)").HasMaxLength(1);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Hora).IsRequired();
                entity.Property(e => e.Id_b);
                entity.Property(e => e.Id_s).IsRequired();
                entity.HasOne(e => e.Buyer)
                      .WithMany()
                      .HasForeignKey(e => e.Id_b);

                entity.HasOne(e => e.Seller)
                      .WithMany()
                      .HasForeignKey(e => e.Id_s);
            });


            // Configuración de la entidad TransactionPago
            builder.Entity<TransactionPago>(entity =>
            {
                entity.ToTable("transaction_pago");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id_va).IsRequired();
                entity.Property(e => e.Id_user).IsRequired();
                entity.Property(e => e.Id_ord).IsRequired();
                entity.Property(e => e.Tipo).HasColumnType("char(1)").HasMaxLength(1);
                entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Hora).IsRequired();
                entity.HasOne(e => e.VirtualAccount)
                      .WithMany()
                      .HasForeignKey(e => new { e.Id_va, e.Id_user });
                entity.HasOne(e => e.OrdenPago)
                      .WithMany()
                      .HasForeignKey(e => e.Id_ord);
            });



            // Configuración de la entidad ExchangeSpot
            builder.Entity<ExchangeSpot>(entity =>
            {
                entity.ToTable("exchange_spot");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RazSocial).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RUC).IsRequired().HasColumnType("char(11)").HasMaxLength(11);
                entity.Property(e => e.Pais).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Ciudad).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Distrito).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DomicilioFiscal).HasMaxLength(200);
                entity.Property(e => e.ESFEE).IsRequired().HasColumnType("decimal(8,4)");
            });




            //Configuración de la entidad SystemManager
            builder.Entity<SystemManager>(entity =>
            {
                entity.ToTable("system_manager");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Pais).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Ciudad).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Distrito).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DomicilioFiscal).HasMaxLength(200);
                entity.Property(e => e.SMFEE).IsRequired().HasColumnType("decimal(8,4)");
            });


            // Configuración de la entidad Recarga
            builder.Entity<Recarga>(entity =>
            {
                entity.ToTable("recarga");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Ges).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Gsm).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Mesra).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Msmra).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Id_es).IsRequired();
                entity.Property(e => e.Id_sm).IsRequired();
                entity.HasOne(e => e.ExchangeSpot)
                      .WithMany()
                      .HasForeignKey(e => e.Id_es);
                entity.HasOne(e => e.SystemManager)
                      .WithMany()
                      .HasForeignKey(e => e.Id_sm);
            });


            // Configuración de la entidad TransactionRecarga
            builder.Entity<TransactionRecarga>(entity =>
            {
                entity.ToTable("transaction_recarga");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id_va).IsRequired();
                entity.Property(e => e.Id_user).IsRequired();
                entity.Property(e => e.Id_rec).IsRequired();
                entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Hora).IsRequired();
                entity.HasOne(e => e.VirtualAccount)
                      .WithMany()
                      .HasForeignKey(e => new { e.Id_va, e.Id_user });
                entity.HasOne(e => e.Recarga)
                      .WithOne()
                      .HasForeignKey<TransactionRecarga>(e => e.Id_rec);
            });


            // Configuración de la entidad SolicitudRetiro
            builder.Entity<SolicitudRetiro>(entity =>
            {
                entity.ToTable("solicitud_retiro");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Estado).IsRequired().HasColumnType("char(1)").HasMaxLength(1);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Hora).IsRequired();
                entity.Property(e => e.Id_s).IsRequired();
                entity.HasOne(e => e.Seller)
                      .WithMany()
                      .HasForeignKey(e => e.Id_s);
            });


            // Configuración de la entidad TransactionRetiro
            builder.Entity<TransactionRetiro>(entity =>
            {
                entity.ToTable("transaction_retiro");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id_va).IsRequired();
                entity.Property(e => e.Id_user).IsRequired();
                entity.Property(e => e.Id_retiro).IsRequired();
                entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Hora).IsRequired();
                entity.HasOne(e => e.VirtualAccount)
                      .WithMany()
                      .HasForeignKey(e => new { e.Id_va, e.Id_user });
                entity.HasOne(e => e.SolicitudRetiro)
                      .WithOne()
                      .HasForeignKey<TransactionRetiro>(e => e.Id_retiro);
            });


            // Configuración de la entidad Error
            builder.Entity<Error>(entity =>
            {
                entity.ToTable("error");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha_registro).IsRequired();
                entity.Property(e => e.Desc_corta).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Desc_detallada).HasColumnType("text");
                entity.Property(e => e.Id_user);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.Id_user);
            });
        }
    }
}
