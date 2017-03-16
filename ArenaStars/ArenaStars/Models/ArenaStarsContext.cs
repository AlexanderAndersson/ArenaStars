using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArenaStars.Models
{
    public class ArenaStarsContext : DbContext
    {
        public ArenaStarsContext() : base ("Asdb")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameStats> GameStats { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<MatchmakingSearch> MatchmakingSearches { get; set; }
        public DbSet<Server> Servers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Games)
                .WithMany(g => g.Participants);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tournaments)
                .WithMany(t => t.Participants);

            //modelBuilder.Entity<Tournament>()
            //    .HasMany(t => t.Participants)
            //    .WithMany(p => p.Tournaments); //Samma som ovanför, fast tvärtom ordning.

            modelBuilder.Entity<Game>()
                .HasMany(g => g.GameStats)
                .WithRequired(gs => gs.Game);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Games);

            modelBuilder.Entity<Tournament>()
                .HasOptional(t => t.Winner);

            modelBuilder.Entity<Report>()
                .HasRequired(r => r.ReportedUser);

            //modelBuilder.Entity<Report>()
            //    .HasRequired(r => r.Reportee);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.ReportList)
            //    .WithRequired(r => r.ReportedUser);
        }
    }
}