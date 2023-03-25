using eAuto.Data.Interfaces.DataModels;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Data.Context
{
    public class EAutoContext : DbContext
    {
        private readonly string _connectionString;
        //configure OnModelCreating modelbuilder?
        public DbSet<BodyTypeDataModel> BodyTypes { get; set; }
        public DbSet<BrandDataModel> Brands { get; set; }
        public DbSet<CarDataModel> Cars { get; set; }
        public DbSet<DriveTypeDataModel> DriveTypes { get; set; }
        public DbSet<EngineDataModel> EngineTypes { get; set; }
        public DbSet<ModelDataModel> Models { get; set; }
        public DbSet<TransmissionDataModel> Transmissions { get; set; }

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

            modelBuilder.Entity<EngineDataModel>()
                .HasKey(e => e.EngineTypeId);

            modelBuilder.Entity<ModelDataModel>()
                .HasKey(m => m.ModelId);

            modelBuilder.Entity<TransmissionDataModel>()
                .HasKey(t => t.TransmissionId);            

            modelBuilder.Entity<GenerationDataModel>()
                .HasKey(g => g.GenerationId);
        }
    }
}