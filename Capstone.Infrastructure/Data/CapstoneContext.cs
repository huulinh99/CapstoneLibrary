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
        public virtual DbSet<BookDetect> BookDetect { get; set; }
        public virtual DbSet<BookGroup> BookGroup { get; set; }
        public virtual DbSet<BookShelf> BookShelf { get; set; }
        public virtual DbSet<BorrowBook> BorrowBook { get; set; }
        public virtual DbSet<BorrowDetail> BorrowDetail { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Drawer> Drawer { get; set; }
        public virtual DbSet<ErrorMessage> ErrorMessage { get; set; }
        public virtual DbSet<FavouriteCategory> FavouriteCategory { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<ReturnBook> ReturnBook { get; set; }
        public virtual DbSet<ReturnDetail> ReturnDetail { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
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
                entity.Property(e => e.BarCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookGroupId)
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
                    .HasConstraintName("FK_BookCategory_Book");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BookCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BookCategory_Category");
            });

            modelBuilder.Entity<BookDetect>(entity =>
            {
                entity.Property(e => e.IsError).HasColumnName("isError");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookDetect)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookDetect_Book");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.BookDetect)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_BookDetect_Staff");
            });

            modelBuilder.Entity<BookGroup>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PublishDate).HasColumnType("date");
            });

            modelBuilder.Entity<BookShelf>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.BookShelf)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_BookShelf_Location");
            });

            modelBuilder.Entity<BorrowBook>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BorrowBook)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_BorrowBook_Customer");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.BorrowBook)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_BorrowBook_Staff");
            });

            modelBuilder.Entity<BorrowDetail>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BorrowDetail)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BorrowDetail_Book");

                entity.HasOne(d => d.Borrow)
                    .WithMany(p => p.BorrowDetail)
                    .HasForeignKey(d => d.BorrowId)
                    .HasConstraintName("FK_BorrowDetail_BorrowBook");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CreatedTime).HasColumnType("date");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Customer_Role");
            });

            modelBuilder.Entity<Drawer>(entity =>
            {
                entity.HasOne(d => d.BookShelf)
                    .WithMany(p => p.Drawer)
                    .HasForeignKey(d => d.BookShelfId)
                    .HasConstraintName("FK_Drawer_BookShelf");
            });

            modelBuilder.Entity<ErrorMessage>(entity =>
            {
                entity.HasOne(d => d.BookDetectError)
                    .WithMany(p => p.ErrorMessage)
                    .HasForeignKey(d => d.BookDetectErrorId)
                    .HasConstraintName("FK_ErrorMessage_BookDetect");

                entity.HasOne(d => d.Drawer)
                    .WithMany(p => p.ErrorMessage)
                    .HasForeignKey(d => d.DrawerId)
                    .HasConstraintName("FK_ErrorMessage_Drawer");
            });

            modelBuilder.Entity<FavouriteCategory>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FavouriteCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_FavouriteCategory_Category");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FavouriteCategory)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_FavouriteCategory_Customer");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.BookGroupId)
                    .HasConstraintName("FK_Feedback_BookGroup");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Customer");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasOne(d => d.BookGroup)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.BookGroupId)
                    .HasConstraintName("FK_Image_BookGroup");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ReturnBook>(entity =>
            {
                entity.Property(e => e.ReturnTime).HasColumnType("datetime");

                entity.HasOne(d => d.Borrow)
                    .WithMany(p => p.ReturnBook)
                    .HasForeignKey(d => d.BorrowId)
                    .HasConstraintName("FK_ReturnBook_BorrowBook");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ReturnBook)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ReturnBook_Customer");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ReturnBook)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_ReturnBook_Staff");
            });

            modelBuilder.Entity<ReturnDetail>(entity =>
            {
                entity.Property(e => e.Fee)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IsLate).HasColumnName("isLate");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ReturnDetail)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_ReturnDetail_Book");

                entity.HasOne(d => d.Return)
                    .WithMany(p => p.ReturnDetail)
                    .HasForeignKey(d => d.ReturnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReturnDetail_ReturnBook");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("date");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Staff_Role");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Notification_Customer");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Notification_Staff");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
