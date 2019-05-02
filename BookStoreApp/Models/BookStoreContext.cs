using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.Models
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        //// Disable Lazy Loading at the context level. I can enable 
        //// lazy-loading explicitly I need to utilize it.
        //public BookStoreContext(DbContextOptions<BookStoreContext> options)
        //    : base(options)
        //{
        //    ChangeTracker.LazyLoadingEnabled = false;
        //}

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorContact> AuthorContact { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookAuthors> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should 
                // move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance 
                // on storing connection strings.
                optionsBuilder.UseSqlServer("Server=KGUPI-MACHINE;Database=BookStore;Trusted_Connection=True;");
            }
        }

        //// Enable Lazy Loading with a call to method: UseLazyLoadingProxies.
        //// EF Core will then enable lazy loading for any navigation property that 
        //// can be overridden. Only thing is that it must be virtual and on a 
        //// class that can be inherited from. For example check Author class, 
        //// the BookAuthors navigation property will be lazy-loaded.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder
        //            .UseLazyLoadingProxies()
        //            .UseSqlServer("Server=KGUPI-MACHINE;Database=BookStore;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AuthorContact>(entity =>
            {
                entity.HasKey(e => e.AuthorId)
                    .HasName("PK__AuthorCo__70DAFC34ED5B617E");

                entity.Property(e => e.AuthorId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.ContactNumber).HasMaxLength(15);

                entity.HasOne(d => d.Author)
                    .WithOne(p => p.AuthorContact)
                    .HasForeignKey<AuthorContact>(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AuthorCon__Autho__25869641");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Book__CategoryId__2C3393D0");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Book__PublisherI__2D27B809");
            });

            modelBuilder.Entity<BookAuthors>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.AuthorId })
                    .HasName("PK__BookAuth__6AED6DC47664EB67");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookAutho__Autho__30F848ED");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookAutho__BookI__300424B4");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100);
            });
        }
    }
}
