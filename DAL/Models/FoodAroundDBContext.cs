using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Models
{
    public partial class FoodAroundDBContext : DbContext
    {
        public FoodAroundDBContext()
        {
        }

        public FoodAroundDBContext(DbContextOptions<FoodAroundDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MsLogin> MsLogins { get; set; }
        public virtual DbSet<MsStore> MsStores { get; set; }
        public virtual DbSet<MsStoreImage> MsStoreImages { get; set; }
        public virtual DbSet<TrLocation> TrLocations { get; set; }
        public virtual DbSet<TrOtp> TrOtps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UI28E21;User ID=sa;Password=serverr;Initial Catalog=FoodAroundDB;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MsLogin>(entity =>
            {
                entity.HasKey(e => e.PkLoginId)
                    .HasName("PK__ms_login__2DD7993DBC448347");

                entity.ToTable("ms_login");

                entity.Property(e => e.PkLoginId).HasColumnName("pk_login_id");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.LoginActivation).HasColumnName("login_activation");

                entity.Property(e => e.LoginPhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("login_phone_number");

                entity.Property(e => e.LoginPin)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("login_pin");

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MsStore>(entity =>
            {
                entity.HasKey(e => e.PkStoreId)
                    .HasName("PK__ms_store__9136CF13D5A1E9C3");

                entity.ToTable("ms_store");

                entity.Property(e => e.PkStoreId).HasColumnName("pk_store_id");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FkLoginId).HasColumnName("fk_login_id");

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StoreDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("store_description");

                entity.Property(e => e.StoreMerchantName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("store_merchant_name");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("store_name");

                entity.Property(e => e.StoreStatus).HasColumnName("store_status");
            });

            modelBuilder.Entity<MsStoreImage>(entity =>
            {
                entity.HasKey(e => e.PkStoreImageId)
                    .HasName("PK__ms_store__082D0988A7262D49");

                entity.ToTable("ms_store_image");

                entity.Property(e => e.PkStoreImageId).HasColumnName("pk_store_image_id");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FkStoreId).HasColumnName("fk_store_id");

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StoreIamgeGuid)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("store_iamge_guid");

                entity.Property(e => e.StoreIamgeName)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("store_iamge_name");

                entity.Property(e => e.StoreImagePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("store_image_path");
            });

            modelBuilder.Entity<TrLocation>(entity =>
            {
                entity.HasKey(e => e.PkLocationId)
                    .HasName("PK__tr_locat__937B46D5162C860F");

                entity.ToTable("tr_location");

                entity.Property(e => e.PkLocationId).HasColumnName("pk_location_id");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FkStoreId).HasColumnName("fk_store_id");

                entity.Property(e => e.LocationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("location_date");

                entity.Property(e => e.LocationLatitude)
                    .IsUnicode(false)
                    .HasColumnName("location_latitude");

                entity.Property(e => e.LocationLongitude)
                    .IsUnicode(false)
                    .HasColumnName("location_longitude");

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TrOtp>(entity =>
            {
                entity.HasKey(e => e.PkOtpId)
                    .HasName("PK__tr_otp__6BF69D6C294A5489");

                entity.ToTable("tr_otp");

                entity.Property(e => e.PkOtpId).HasColumnName("pk_otp_id");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FkLoginId).HasColumnName("fk_login_id");

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Otp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("otp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
