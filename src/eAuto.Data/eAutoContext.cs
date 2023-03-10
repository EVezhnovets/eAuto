using eAuto.Data.Interfaces.DataModels;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Data
{
    public class eAutoContext : DbContext
    {
        //configure OnModelCreating modelbuilder?
        public DbSet<BodyTypeDataModel> BodyTypes { get; set; }
        public DbSet<BrandDataModel> Brands { get; set; }
        public DbSet<CarDataModel> Cars { get; set; }
        public DbSet<DriveTypeDataModel> DriveTypes { get; set; }
        public DbSet<EngineTypeDataModel> EngineTypes { get; set; }
        public DbSet<ModelDataModel> Models { get; set; }
        public DbSet<TransmissionDataModel> Transmissions { get; set; }

        public eAutoContext(DbContextOptions<eAutoContext> options) : base(options)
        {
        }
    }
}