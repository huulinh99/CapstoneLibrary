using System;
using Capstone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Capstone.Infrastructure.Data
{
    public partial class CapstoneContext : DbContext
    {
        public CapstoneContext()
        {
        }

        public CapstoneContext(DbContextOptions<CapstoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }
        public virtual DbSet<BookGroup> BookGroup { get; set; }
        public virtual DbSet<BookShelf> BookShelf { get; set; }
        public virtual DbSet<BorrowBook> BorrowBook { get; set; }
        public virtual DbSet<BorrowDetail> BorrowDetail { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Detection> Detection { get; set; }
        public virtual DbSet<DetectionError> DetectionError { get; set; }
        public virtual DbSet<Drawer> Drawer { get; set; }
        public virtual DbSet<DrawerDetection> DrawerDetection { get; set; }
        public virtual DbSet<FavouriteCategory> FavouriteCategory { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Patron> Patron { get; set; }
        public virtual DbSet<ReturnBook> ReturnBook { get; set; }
        public virtual DbSet<ReturnDetail> ReturnDetail { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<UndefinedError> UndefinedError { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=103.121.89.51;Database=Capstone;User Id=linh;password=Testing)(&*;Integrated Security = false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Note).IsUnicode(false);

                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_BookGroup");

                entity.HasOne(d => d.Drawer)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.DrawerId)
                    .HasConstraintName("FK_Book_Drawer");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.BookCategory)
                    .HasForeignKey(d => d.BookGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookCategory_Book");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BookCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookCategory_Category");
            });

            modelBuilder.Entity<BookGroup>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PublishDate).HasColumnType("date");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.BookGroup)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookGroup_Staff");
            });

            modelBuilder.Entity<BookShelf>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.BookShelf)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookShelf_Location");
            });

            modelBuilder.Entity<BorrowBook>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.HasOne(d => d.Patron)
                    .WithMany(p => p.BorrowBook)
                    .HasForeignKey(d => d.PatronId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BorrowBook_Customer");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.BorrowBook)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BorrowBook_Staff");
            });

            modelBuilder.Entity<BorrowDetail>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BorrowDetail)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BorrowDetail_Book");

                entity.HasOne(d => d.Borrow)
                    .WithMany(p => p.BorrowDetail)
                    .HasForeignKey(d => d.BorrowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BorrowDetail_BorrowBook");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Detection>(entity =>
            {
                entity.Property(e => e.Time).HasColumnType("date");

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.BookShelf)
                    .WithMany(p => p.Detection)
                    .HasForeignKey(d => d.BookShelfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detection_BookShelf");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Detection)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detection_Staff");
            });

            modelBuilder.Entity<DetectionError>(entity =>
            {
                entity.Property(e => e.ErrorMessage).IsRequired();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.DetectionError)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetectionError_Book");

                entity.HasOne(d => d.DrawerDetection)
                    .WithMany(p => p.DetectionError)
                    .HasForeignKey(d => d.DrawerDetectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetectionError_DrawerDetection");
            });

            modelBuilder.Entity<Drawer>(entity =>
            {
                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.BookShelf)
                    .WithMany(p => p.Drawer)
                    .HasForeignKey(d => d.BookShelfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drawer_BookShelf");
            });

            modelBuilder.Entity<DrawerDetection>(entity =>
            {
                entity.HasOne(d => d.Detection)
                    .WithMany(p => p.DrawerDetection)
                    .HasForeignKey(d => d.DetectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DrawerDetection_Detection");

                entity.HasOne(d => d.Drawer)
                    .WithMany(p => p.DrawerDetection)
                    .HasForeignKey(d => d.DrawerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DrawerDetection_Drawer");
            });

            modelBuilder.Entity<FavouriteCategory>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FavouriteCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteCategory_Category");

                entity.HasOne(d => d.Patron)
                    .WithMany(p => p.FavouriteCategory)
                    .HasForeignKey(d => d.PatronId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteCategory_Customer");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ReviewContent).IsRequired();

                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.BookGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_BookGroup");

                entity.HasOne(d => d.Patron)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.PatronId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Customer");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.BookGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Image_BookGroup");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Patron>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedTime).HasColumnType("date");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Patron)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Role");
            });

            modelBuilder.Entity<ReturnBook>(entity =>
            {
                entity.Property(e => e.ReturnTime).HasColumnType("datetime");

                entity.HasOne(d => d.Borrow)
                    .WithMany(p => p.ReturnBook)
                    .HasForeignKey(d => d.BorrowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReturnBook_BorrowBook");

                entity.HasOne(d => d.Patron)
                    .WithMany(p => p.ReturnBook)
                    .HasForeignKey(d => d.PatronId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReturnBook_Customer");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ReturnBook)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReturnBook_Staff");
            });

            modelBuilder.Entity<ReturnDetail>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ReturnDetail)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReturnDetail_Book");

                entity.HasOne(d => d.Return)
                    .WithMany(p => p.ReturnDetail)
                    .HasForeignKey(d => d.ReturnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReturnDetail_ReturnBook");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("date");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.Username).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_Role");
            });

            modelBuilder.Entity<UndefinedError>(entity =>
            {
                entity.Property(e => e.ErrorMessage).IsRequired();

                entity.HasOne(d => d.DrawerDetection)
                    .WithMany(p => p.UndefinedError)
                    .HasForeignKey(d => d.DrawerDetectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UndefinedError_DrawerDetection");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.BookGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserNotification_BookGroup");

                entity.HasOne(d => d.Patron)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.PatronId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Customer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
