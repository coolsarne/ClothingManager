using System.Diagnostics;
using ClothingManager.BL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace ClothingManager.DAL.EF{
    public class ClothingManagerDbContext : DbContext{
        public DbSet<ClothingPiece> ClothingPieces{ get; set; }
        public DbSet<Designer> Designers{ get; set; }
        public DbSet<Store> Stores{ get; set; }
        public DbSet<ClothingPieceDesigner> ClothingPieceDesigners{ get; set; }

        public ClothingManagerDbContext(){
            ClothingManagerInitializer.Initialize(this, false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if (!optionsBuilder.IsConfigured){
                optionsBuilder.UseSqlite(@"Data Source=../ClothingManagerDatabase.db");
                optionsBuilder.LogTo(message => Debug.WriteLine(message));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<ClothingPieceDesigner>()
                .HasOne(cpd => cpd.ClothingPiece)
                .WithMany(cp => cp.Designers)
                .HasForeignKey("FK_ClothingPieceId")
                .IsRequired();

            modelBuilder.Entity<ClothingPieceDesigner>()
                .HasOne(cpd => cpd.Designer)
                .WithMany(d => d.ClothingPieces)
                .HasForeignKey("FK_DesignerId")
                .IsRequired();

            modelBuilder.Entity<ClothingPiece>()
                .HasOne(cp => cp.Store)
                .WithMany(s => s.ClothingPieces)
                .IsRequired(false);

            modelBuilder.Entity<ClothingPiece>()
                .HasKey(cp => cp.Id);

            modelBuilder.Entity<Designer>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Store>()
                .HasKey(s => s.Id);
            
            modelBuilder.Entity<ClothingPieceDesigner>()
                .HasKey("FK_ClothingPieceId", "FK_DesignerId");
        }
    }
}