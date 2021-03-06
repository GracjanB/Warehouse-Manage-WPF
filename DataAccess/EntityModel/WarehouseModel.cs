namespace DataAccess.EntityModel
{
    using DataAccess.Entities;
    using System.Data.Entity;
    using DataAccess.EntitiesConfiguration;

    public class WarehouseModel : DbContext
    {
        public WarehouseModel()
            : base("name=WarehouseDBAzure")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new DeviceConfiguration());
            modelBuilder.Configurations.Add(new ProducerConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}