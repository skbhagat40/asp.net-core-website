using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcMovies.Models
{
    public class MvcMoviesContext : DbContext
    {
        public MvcMoviesContext (DbContextOptions<MvcMoviesContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovies.Models.Producer> Producer { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                .HasKey(t => new { t.ActorId, t.MovieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(pt => pt.Actor)
                .WithMany(p => p.ActorMovies)
                .HasForeignKey(pt => pt.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(pt => pt.Movie)
                .WithMany(t => t.ActorMovies)
                .HasForeignKey(pt => pt.MovieId);
        }
    }
}
