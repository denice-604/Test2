using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Test22.Models;


namespace Test22.Data
{
    /// <summary>
    /// Класс сгенерированный Entity framework при подходе "code first" для связи с бд
    /// </summary>
    public partial class Test2Context : DbContext
    {
        public Test2Context()
        {
        }

        public Test2Context(DbContextOptions<Test2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Test22.Models.File> Files { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial catalog=Test2; Integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Account__ClassID__3B75D760");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.Name).HasMaxLength(90);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK__Class__FileID__38996AB5");
            });

            modelBuilder.Entity<Test22.Models.File>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(90);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
