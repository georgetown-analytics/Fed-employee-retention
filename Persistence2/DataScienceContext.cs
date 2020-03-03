using Models2;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistence2
{
    public class DataScienceContext : DbContext
    {
        public DataScienceContext() : base("DataScienceContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataScienceContext>());
        }

        public DbSet<Agency> Agency { get; set; }

        public DbSet<Employment> Employment { get; set; }

        public DbSet<Survey> Survey { get; set; }

        public DbSet<Years> Years { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
