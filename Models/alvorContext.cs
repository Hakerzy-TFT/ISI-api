using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace gamespace_api.Models
{
    public partial class alvorContext : DbContext
    {
        public alvorContext()
        {
        }

        public alvorContext(DbContextOptions<alvorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bug> Bugs { get; set; }
        public virtual DbSet<EndUser> EndUsers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameReview> GameReviews { get; set; }
        public virtual DbSet<GameUser> GameUsers { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ServiceStatus> ServiceStatuses { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Util> Utils { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.ToTable("bug");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfCreation).HasColumnName("date_of_creation");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<EndUser>(entity =>
            {
                entity.ToTable("end_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Surname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.EndUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__end_user__user_t__607251E5");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK__game__status__65370702");
            });

            modelBuilder.Entity<GameReview>(entity =>
            {
                entity.ToTable("game_review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameReviews)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK__game_revi__game___6EC0713C");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.GameReviews)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK__game_revi__revie__6FB49575");
            });

            modelBuilder.Entity<GameUser>(entity =>
            {
                entity.ToTable("game_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.GameUsers)
                    .HasForeignKey(d => d.EndUserId)
                    .HasConstraintName("FK__game_user__end_u__681373AD");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.LogsId)
                    .HasName("PK__Logs__F37D07C66282A8AC");

                entity.Property(e => e.LogsId).HasColumnName("logs_id");

                entity.Property(e => e.LogDate).HasColumnName("log_date");

                entity.Property(e => e.LogMsg)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("log_msg");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewContent)
                    .IsUnicode(false)
                    .HasColumnName("review_content");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.EndUserId)
                    .HasConstraintName("FK__review__end_user__6AEFE058");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__review__status_i__6BE40491");
            });

            modelBuilder.Entity<ServiceStatus>(entity =>
            {
                entity.ToTable("ServiceStatus");

                entity.Property(e => e.ServiceStatusId).HasColumnName("service_status_id");

                entity.Property(e => e.Api).HasColumnName("api");

                entity.Property(e => e.Db).HasColumnName("db");

                entity.Property(e => e.WebApp).HasColumnName("web_app");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("test");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("user_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Util>(entity =>
            {
                entity.Property(e => e.UtilId).HasColumnName("util_id");

                entity.Property(e => e.UtilBody)
                    .IsUnicode(false)
                    .HasColumnName("util_body");

                entity.Property(e => e.UtilName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("util_name");

                entity.Property(e => e.UtilType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("util_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
