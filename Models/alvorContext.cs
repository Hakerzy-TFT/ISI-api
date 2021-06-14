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
        public virtual DbSet<DebugGameLog> DebugGameLogs { get; set; }
        public virtual DbSet<EndUser> EndUsers { get; set; }
        public virtual DbSet<EndUserSecurity> EndUserSecurities { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameBug> GameBugs { get; set; }
        public virtual DbSet<GameKey> GameKeys { get; set; }
        public virtual DbSet<GamePage> GamePages { get; set; }
        public virtual DbSet<GamePlatform> GamePlatforms { get; set; }
        public virtual DbSet<GameReview> GameReviews { get; set; }
        public virtual DbSet<GameType> GameTypes { get; set; }
        public virtual DbSet<GameUser> GameUsers { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<RankingResult> RankingResults { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ServiceStatus> ServiceStatuses { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }
        public virtual DbSet<StudioPage> StudioPages { get; set; }
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
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.Bugs)
                    .HasForeignKey(d => d.EndUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bug__end_user_id__0CA5D9DE");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Bugs)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bug__status_id__0D99FE17");
            });

            modelBuilder.Entity<DebugGameLog>(entity =>
            {
                entity.ToTable("debug_game_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.GamePageId).HasColumnName("game_page_id");

                entity.Property(e => e.GamePlatformId).HasColumnName("game_platform_id");

                entity.Property(e => e.GameTypeId).HasColumnName("game_type_id");

                entity.Property(e => e.Genre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("genre");

                entity.Property(e => e.ImgSrc)
                    .IsUnicode(false)
                    .HasColumnName("img_src");

                entity.Property(e => e.Platform)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("platform");

                entity.Property(e => e.PostedDate).HasColumnName("posted_date");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StudioId).HasColumnName("studio_id");

                entity.Property(e => e.StudioName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("studio_name");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.TotalRating).HasColumnName("total_rating");
            });

            modelBuilder.Entity<EndUser>(entity =>
            {
                entity.ToTable("end_user");

                entity.HasIndex(e => e.Email, "UQ__end_user__AB6E61649325D20D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountLevel).HasColumnName("account_level");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IconSrc)
                    .IsUnicode(false)
                    .HasColumnName("icon_src");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Wallet).HasColumnName("wallet");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.EndUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__end_user__user_t__07E124C1");
            });

            modelBuilder.Entity<EndUserSecurity>(entity =>
            {
                entity.ToTable("end_user_security");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("hashed_password");

                entity.Property(e => e.Salt).HasColumnName("salt");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.EndUserSecurities)
                    .HasForeignKey(d => d.EndUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__end_user___end_u__09946309");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.HasIndex(e => e.Title, "UQ__game__E52A1BB3F70DC27F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.GamePageId).HasColumnName("game_page_id");

                entity.Property(e => e.GamePlatformId).HasColumnName("game_platform_id");

                entity.Property(e => e.GameTypeId).HasColumnName("game_type_id");

                entity.Property(e => e.ImgSrc)
                    .IsUnicode(false)
                    .HasColumnName("img_src");

                entity.Property(e => e.PostedDate).HasColumnName("posted_date");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StudioId).HasColumnName("studio_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.TotalRating).HasColumnName("total_rating");

                entity.HasOne(d => d.GamePage)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GamePageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game__game_page___17236851");

                entity.HasOne(d => d.GamePlatform)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GamePlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game__game_platf__2C1E8537");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game__game_type___2D12A970");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game__status_id__18178C8A");

                entity.HasOne(d => d.Studio)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.StudioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game__studio_id__162F4418");
            });

            modelBuilder.Entity<GameBug>(entity =>
            {
                entity.ToTable("game_bug");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BugId).HasColumnName("bug_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.HasOne(d => d.Bug)
                    .WithMany(p => p.GameBugs)
                    .HasForeignKey(d => d.BugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_bug__bug_id__53F76C67");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameBugs)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_bug__game_i__5303482E");
            });

            modelBuilder.Entity<GameKey>(entity =>
            {
                entity.ToTable("game_key");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("value");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.GameKeys)
                    .HasForeignKey(d => d.EndUserId)
                    .HasConstraintName("FK__game_key__end_us__5CC1BC92");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameKeys)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_key__game_i__5BCD9859");
            });

            modelBuilder.Entity<GamePage>(entity =>
            {
                entity.ToTable("game_page");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BackgroundColor)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("background_color");

                entity.Property(e => e.BackgroundImage)
                    .IsUnicode(false)
                    .HasColumnName("background_image");

                entity.Property(e => e.Button1Url)
                    .IsUnicode(false)
                    .HasColumnName("button1_url");

                entity.Property(e => e.Button2Url)
                    .IsUnicode(false)
                    .HasColumnName("button2_url");

                entity.Property(e => e.ButtonColor)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("button_color");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FontColor)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("font_color");

                entity.Property(e => e.Header)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("header");

                entity.Property(e => e.Img1Src)
                    .IsUnicode(false)
                    .HasColumnName("img1_src");

                entity.Property(e => e.Img2Src)
                    .IsUnicode(false)
                    .HasColumnName("img2_src");

                entity.Property(e => e.Img3Src)
                    .IsUnicode(false)
                    .HasColumnName("img3_src");
            });

            modelBuilder.Entity<GamePlatform>(entity =>
            {
                entity.ToTable("game_platform");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Platform)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("platform");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_revi__game___22951AFD");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.GameReviews)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_revi__revie__23893F36");
            });

            modelBuilder.Entity<GameType>(entity =>
            {
                entity.ToTable("game_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<GameUser>(entity =>
            {
                entity.ToTable("game_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.GameUsers)
                    .HasForeignKey(d => d.EndUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_user__end_u__1BE81D6E");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameUsers)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__game_user__game___1AF3F935");
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

            modelBuilder.Entity<RankingResult>(entity =>
            {
                entity.ToTable("ranking_results");

                entity.HasIndex(e => e.Title, "UQ__ranking___E52A1BB3EC29DA0D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.GamePageId).HasColumnName("game_page_id");

                entity.Property(e => e.Genre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GENRE");

                entity.Property(e => e.ImgSrc)
                    .IsUnicode(false)
                    .HasColumnName("img_src");

                entity.Property(e => e.Platform)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("platform");

                entity.Property(e => e.PostedDate).HasColumnName("posted_date");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Studio)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("studio");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.TotalRating).HasColumnName("total_rating");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__review__end_user__1EC48A19");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__review__status_i__1FB8AE52");
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
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Studio>(entity =>
            {
                entity.ToTable("studio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndUserId).HasColumnName("end_user_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Owner)
                    .IsUnicode(false)
                    .HasColumnName("owner");

                entity.Property(e => e.StudioPageId).HasColumnName("studio_page_id");

                entity.HasOne(d => d.EndUser)
                    .WithMany(p => p.Studios)
                    .HasForeignKey(d => d.EndUserId)
                    .HasConstraintName("FK__studio__end_user__10766AC2");

                entity.HasOne(d => d.StudioPage)
                    .WithMany(p => p.Studios)
                    .HasForeignKey(d => d.StudioPageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__studio__studio_p__62458BBE");
            });

            modelBuilder.Entity<StudioPage>(entity =>
            {
                entity.ToTable("studio_page");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BackgroundColor)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("background_color");

                entity.Property(e => e.BackgroundImage)
                    .IsUnicode(false)
                    .HasColumnName("background_image");

                entity.Property(e => e.Button1Url)
                    .IsUnicode(false)
                    .HasColumnName("button1_url");

                entity.Property(e => e.Button2Url)
                    .IsUnicode(false)
                    .HasColumnName("button2_url");

                entity.Property(e => e.ButtonColor)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("button_color");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FontColor)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("font_color");

                entity.Property(e => e.Header)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("header");

                entity.Property(e => e.Img1Src)
                    .IsUnicode(false)
                    .HasColumnName("img1_src");

                entity.Property(e => e.Img2Src)
                    .IsUnicode(false)
                    .HasColumnName("img2_src");

                entity.Property(e => e.Img3Src)
                    .IsUnicode(false)
                    .HasColumnName("img3_src");
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
                    .IsRequired()
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
