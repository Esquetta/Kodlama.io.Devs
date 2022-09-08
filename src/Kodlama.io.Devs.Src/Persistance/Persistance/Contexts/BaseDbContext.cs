using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Persistance.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Technology>(x =>
            {
                x.ToTable("Tech").HasKey("Id");
                x.Property(x => x.Name).HasColumnName("Name");
                x.Property(x => x.LanguageId).HasColumnName("LanguageId");

                x.HasOne(x => x.Language);
            });

            Technology[] technologies = { new(1, "WPF", 2), new(2, "ASP.NET", 2), new(3, "Spring", 3) };
            modelBuilder.Entity<Technology>().HasData(technologies);

        }


    }
}
