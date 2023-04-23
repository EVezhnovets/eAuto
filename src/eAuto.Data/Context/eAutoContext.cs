using eAuto.Data.Interfaces.DataModels;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Data.Context
{
    public class EAutoContext : DbContext
    {
        private readonly string _connectionString;
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

        public EAutoContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(_connectionString);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}