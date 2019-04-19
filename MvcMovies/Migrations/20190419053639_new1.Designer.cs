﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovies.Models;

namespace MvcMovies.Migrations
{
    [DbContext(typeof(MvcMoviesContext))]
    [Migration("20190419053639_new1")]
    partial class new1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MvcMovies.Models.Actor", b =>
                {
                    b.Property<int>("ActorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActorMovieActorId");

                    b.Property<int?>("ActorMovieMovieId");

                    b.Property<string>("Bio");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Name");

                    b.Property<string>("Sex");

                    b.HasKey("ActorID");

                    b.HasIndex("ActorMovieActorId", "ActorMovieMovieId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MvcMovies.Models.ActorMovie", b =>
                {
                    b.Property<int>("ActorId");

                    b.Property<int>("MovieId");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorMovie");
                });

            modelBuilder.Entity("MvcMovies.Models.Movie", b =>
                {
                    b.Property<int>("MovieID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActorMovieActorId");

                    b.Property<int?>("ActorMovieMovieId");

                    b.Property<string>("Name");

                    b.Property<string>("Plot");

                    b.Property<byte[]>("Poster");

                    b.Property<int>("ProducerId");

                    b.Property<string>("ProducerName");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("SelectedActors");

                    b.HasKey("MovieID");

                    b.HasIndex("ProducerId");

                    b.HasIndex("ActorMovieActorId", "ActorMovieMovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MvcMovies.Models.Producer", b =>
                {
                    b.Property<int>("ProducerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Name");

                    b.Property<string>("Sex");

                    b.HasKey("ProducerID");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("MvcMovies.Models.Actor", b =>
                {
                    b.HasOne("MvcMovies.Models.ActorMovie", "ActorMovie")
                        .WithMany()
                        .HasForeignKey("ActorMovieActorId", "ActorMovieMovieId");
                });

            modelBuilder.Entity("MvcMovies.Models.ActorMovie", b =>
                {
                    b.HasOne("MvcMovies.Models.Actor", "Actor")
                        .WithMany("ActorMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcMovies.Models.Movie", "Movie")
                        .WithMany("ActorMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcMovies.Models.Movie", b =>
                {
                    b.HasOne("MvcMovies.Models.Producer", "Producer")
                        .WithMany("Movies")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcMovies.Models.ActorMovie", "ActorMovie")
                        .WithMany()
                        .HasForeignKey("ActorMovieActorId", "ActorMovieMovieId");
                });
#pragma warning restore 612, 618
        }
    }
}
