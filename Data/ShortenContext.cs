using Entities;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ShortenContext : DbContext
    {
        public ShortenContext() : base("name=shorturl")
        {

        }  

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estatistica>()
                .HasRequired(s => s.ShortenUrl)
                .WithMany(u => u.Estatistica)
                .Map(m => m.MapKey("shortenUrl_id"));
        }

        public virtual DbSet<ShortenUrl> ShortenUrl { get; set; }
        public virtual DbSet<Estatistica> Estatistica { get; set; }
    }
}
