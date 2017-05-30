namespace NotificationJob
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<user_childs> user_childs { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<vaccination_check> vaccination_check { get; set; }
        public virtual DbSet<vaccinations> vaccinations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user_childs>()
                .HasMany(e => e.vaccination_check)
                .WithRequired(e => e.user_childs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.user_childs)
                .WithRequired(e => e.users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vaccinations>()
                .HasMany(e => e.vaccination_check)
                .WithRequired(e => e.vaccinations)
                .WillCascadeOnDelete(false);
        }
    }
}
