﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Data
{
	public class DataContext : IdentityDbContext<User>
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<SuccessCase> SuccessCases { get; set; }
        public DbSet<Adoption> Adoption { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Pet>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Article>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            //modelBuilder.Entity<SuccessCase>().HasIndex(x => x.Id).IsUnique();
            DisableCascadingDelete(modelBuilder);
        }

        public void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationShips = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationShips)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}

