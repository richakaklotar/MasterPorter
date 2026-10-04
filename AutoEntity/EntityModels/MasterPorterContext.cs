using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoEntity.EntityModels
{
    public partial class MasterPorterContext : DbContext
    {
        public MasterPorterContext()
        {
        }

        public MasterPorterContext(DbContextOptions<MasterPorterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Plant> Plant { get; set; } = null!;
        public virtual DbSet<Division> Division { get; set; } = null!;
        public virtual DbSet<Machine> Machine { get; set; } = null!;
        public virtual DbSet<Project> Project { get; set; } = null!;
        public virtual DbSet<Components> Components { get; set; } = null!;
        public virtual DbSet<Activities> Activities { get; set; } = null!;
        public virtual DbSet<SubActivities> SubActivities { get; set; } = null!;
        public virtual DbSet<Shift> Shift { get; set; } = null!;
        public virtual DbSet<Designation> Designation { get; set; } = null!;
        public virtual DbSet<Employee> Employee { get; set; } = null!;
        public virtual DbSet<JobCard> JobCard { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=db66217.databaseasp.net; Database=db66217; User Id=db66217; Password=Baltiboi2026;",
                    new MySqlServerVersion(new Version(8, 0, 46)),
                    o => o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.PlantId);

                entity.ToTable("Plant");

                entity.Property(e => e.PlantId)
                    .HasColumnName("PlantId");

                entity.Property(e => e.PlantName)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.PlantCode)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsRequired()
                    .HasDefaultValue("Active");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
