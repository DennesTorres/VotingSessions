using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace libVoting.Models
{
    public partial class VotingContext : DbContext
    {
        public VotingContext()
        {
        }

        public VotingContext(DbContextOptions<VotingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conferences> Conferences { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<Speakers> Speakers { get; set; }

        public virtual DbSet<Voting> Voting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=datasaturday.database.windows.net;Database=Voting;UID=dennes;PWD=Wmhapx4696");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Conferences>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Voting>(entity =>
            {
                entity.Property(e => e.UserKey)
                    .HasColumnName("UserKey")
                    .IsUnicode(false);

                entity.HasOne(d => d.Conference)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.ConferenceId)
                    .HasConstraintName("fkVotingConference");


                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("fkVotingSession");

            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Conference)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.ConferenceId)
                    .HasConstraintName("fkConference");

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.SpeakerId)
                    .HasConstraintName("fkSpeaker");
            });

            modelBuilder.Entity<Speakers>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TagLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
