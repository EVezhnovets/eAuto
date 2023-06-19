using eAuto.Data.Interfaces.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Data.Context
{
    public class EAutoContext : IdentityDbContext
    {
        private readonly string? _connectionString;
        public DbSet<BodyTypeDataModel> BodyTypes { get; set; }
        public DbSet<BrandDataModel> Brands { get; set; }
        public DbSet<CarDataModel> Cars { get; set; }
        public DbSet<DriveTypeDataModel> DriveTypes { get; set; }
        public DbSet<ModelDataModel> Models { get; set; }
        public DbSet<TransmissionDataModel> Transmissions { get; set; }
        public DbSet<GenerationDataModel> Generations{ get; set; }
        public DbSet<EngineTypeDataModel> EngineTypes{ get; set; }
        public DbSet<MotorOilDataModel> MotorOils{ get; set; }
        public DbSet<ProductBrandDataModel> ProductBrands{ get; set; }
        public DbSet<ShoppingCartDataModel> ShoppingCarts{ get; set; }
        public DbSet<OrderHeaderDataModel> OrderHeaders { get; set; }
        public DbSet<OrderDetailsDataModel> OrderDetails { get; set; }
        public DbSet<OrderCarDataModel> OrderCarDataModels { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public EAutoContext(DbContextOptions<EAutoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BodyTypeDataModel>()
                .HasKey(bt => bt.BodyTypeId);

            modelBuilder.Entity<BrandDataModel>()
                .HasKey(b => b.BrandId);

            modelBuilder.Entity<CarDataModel>()
                .HasKey(c => c.CarId);

            modelBuilder.Entity<DriveTypeDataModel>()
                .HasKey(dt => dt.DriveTypeId);

            modelBuilder.Entity<ModelDataModel>()
                .HasKey(m => m.ModelId);

            modelBuilder.Entity<TransmissionDataModel>()
                .HasKey(t => t.TransmissionId);

            modelBuilder.Entity<GenerationDataModel>()
                .HasKey(g => g.GenerationId);

            modelBuilder.Entity<EngineTypeDataModel>()
                .HasKey(e => e.EngineTypeId);

            modelBuilder.Entity<MotorOilDataModel>()
                .HasKey(e => e.MotorOilDataModelId);

            modelBuilder.Entity<ProductBrandDataModel>()
                .HasKey(e => e.ProductBrandDataModelId);

            modelBuilder.Entity<ShoppingCartDataModel>()
                .HasKey(e => e.ShoppingCartId);

            modelBuilder.Entity<OrderHeaderDataModel>()
                .HasKey(e => e.OrderId);

            modelBuilder.Entity<OrderDetailsDataModel>()
                .HasKey(e => e.OrderDetailsId);

            modelBuilder.Entity<OrderCarDataModel>()
                .HasKey(e => e.OrderCarId);
        }
    }
}