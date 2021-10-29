using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PerfJournal
{
    public partial class PerfJournalContext : DbContext
    {
        public PerfJournalContext()
        {
        }

        public PerfJournalContext(DbContextOptions<PerfJournalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Build> Builds { get; set; }
        public virtual DbSet<Environment> Environments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<Tester> Testers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Build>(entity =>
            {
                entity.ToTable("Build");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Builds)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Build__ProjectId__39237A9A");
            });

            modelBuilder.Entity<Environment>(entity =>
            {
                entity.ToTable("Environment");

                entity.Property(e => e.EnvironmentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.TestDescription).HasMaxLength(1000);

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test__ProjectId__3DE82FB7");
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("TestResult");

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.HasOne(d => d.Build)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.BuildId)
                    .HasConstraintName("FK__TestResul__Build__44952D46");

                entity.HasOne(d => d.Environment)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.EnvironmentId)
                    .HasConstraintName("FK__TestResul__Envir__4589517F");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestResul__Proje__43A1090D");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestResul__TestI__42ACE4D4");

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TesterId)
                    .HasConstraintName("FK__TestResul__Teste__467D75B8");
            });

            modelBuilder.Entity<Tester>(entity =>
            {
                entity.ToTable("Tester");

                entity.Property(e => e.TesterName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
