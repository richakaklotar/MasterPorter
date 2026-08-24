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

        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<Division> Division { get; set; } = null!;
        public virtual DbSet<Machine> Machine { get; set; } = null!;
        public virtual DbSet<Project> Project { get; set; } = null!;
        public virtual DbSet<Components> Components { get; set; } = null!;
        public virtual DbSet<Activities> Activities { get; set; } = null!;
        public virtual DbSet<SubActivities> SubActivities { get; set; } = null!;
        public virtual DbSet<Shift> Shift { get; set; } = null!;
        public virtual DbSet<Designation> Designation { get; set; } = null!;
        public virtual DbSet<Employee> Employee { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=MasterPorter;uid=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.ToTable("plant");

                entity.Property(e => e.PlantId).HasColumnName("PlantID");

                entity.Property(e => e.Isactive)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.PlantCode).HasMaxLength(255);

                entity.Property(e => e.PlantName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
