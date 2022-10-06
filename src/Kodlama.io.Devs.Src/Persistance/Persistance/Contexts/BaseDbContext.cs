using Core.Security.Entities;
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
        public DbSet<Developer> Developers { get; set; }
        public DbSet<GithubAccount> GithubAccounts { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ProgramingLanguagesConStr")));

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

            

            
            Language[] languages = { new(1, "C#"), new(2, "Java"), new(3, "Python") };

            modelBuilder.Entity<Language>().HasData(languages);

            Technology[] technologies = { new(1, "WPF", 1), new(2, "ASP.NET", 1), new(3, "Spring", 2) };
            modelBuilder.Entity<Technology>().HasData(technologies);

        }


    }
}
