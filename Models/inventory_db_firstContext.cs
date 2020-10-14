using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace inventory_management_api.Models
{
    public partial class inventory_db_firstContext : DbContext
    {
        public inventory_db_firstContext()
        {
        }

        public inventory_db_firstContext(DbContextOptions<inventory_db_firstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrateur> Administrateur { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Commercial> Commercial { get; set; }
        public virtual DbSet<Emplacement> Emplacement { get; set; }
        public virtual DbSet<Inventaire> Inventaire { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;uid=root;database=inventory_db_first", x => x.ServerVersion("10.4.11-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrateur>(entity =>
            {
                entity.HasKey(e => e.IdAdministrateur)
                    .HasName("PRIMARY");

                entity.ToTable("administrateur");

                entity.Property(e => e.IdAdministrateur)
                    .HasColumnName("id_administrateur")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LoginAdministrateur)
                    .IsRequired()
                    .HasColumnName("login_administrateur")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomAdministrateur)
                    .IsRequired()
                    .HasColumnName("nom_administrateur")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PasswordAdministrateur)
                    .IsRequired()
                    .HasColumnName("password_administrateur")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrenomAdministrateur)
                    .IsRequired()
                    .HasColumnName("prenom_administrateur")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.IdArticle)
                    .HasName("PRIMARY");

                entity.ToTable("article");

                entity.HasIndex(e => e.IdEmplacement)
                    .HasName("id_emplacement");

                entity.Property(e => e.IdArticle)
                    .HasColumnName("id_article")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.IdEmplacement)
                    .HasColumnName("id_emplacement")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LibelleArticle)
                    .IsRequired()
                    .HasColumnName("libelle_article")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.IdEmplacementNavigation)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.IdEmplacement)
                    .HasConstraintName("article_ibfk_1");
            });

            modelBuilder.Entity<Commercial>(entity =>
            {
                entity.HasKey(e => e.IdCommercial)
                    .HasName("PRIMARY");

                entity.ToTable("commercial");

                entity.Property(e => e.IdCommercial)
                    .HasColumnName("id_commercial")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LoginCommercial)
                    .IsRequired()
                    .HasColumnName("login_commercial")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NomCommercial)
                    .IsRequired()
                    .HasColumnName("nom_commercial")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PasswordCommercial)
                    .IsRequired()
                    .HasColumnName("password_commercial")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PrenomCommercial)
                    .IsRequired()
                    .HasColumnName("prenom_commercial")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Emplacement>(entity =>
            {
                entity.HasKey(e => e.IdEmplacement)
                    .HasName("PRIMARY");

                entity.ToTable("emplacement");

                entity.HasIndex(e => e.IdEmplacementParent)
                    .HasName("id_emplacement_parent");

                entity.Property(e => e.IdEmplacement)
                    .HasColumnName("id_emplacement")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ContientArticle)
                    .HasColumnName("contient_article")
                    .HasColumnType("int(1) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdEmplacementParent)
                    .HasColumnName("id_emplacement_parent")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LibelleEmplacement)
                    .IsRequired()
                    .HasColumnName("libelle_emplacement")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.IdEmplacementParentNavigation)
                    .WithMany(p => p.InverseIdEmplacementParentNavigation)
                    .HasForeignKey(d => d.IdEmplacementParent)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("emplacement_ibfk_1");
            });

            modelBuilder.Entity<Inventaire>(entity =>
            {
                entity.HasKey(e => e.IdInventaire)
                    .HasName("PRIMARY");

                entity.ToTable("inventaire");

                entity.HasIndex(e => e.IdEmplacement)
                    .HasName("id_emplacement");

                entity.Property(e => e.IdInventaire)
                    .HasColumnName("id_inventaire")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DateInventaire)
                    .IsRequired()
                    .HasColumnName("date_inventaire")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdEmplacement)
                    .HasColumnName("id_emplacement")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.IdEmplacementNavigation)
                    .WithMany(p => p.Inventaire)
                    .HasForeignKey(d => d.IdEmplacement)
                    .HasConstraintName("inventaire_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
