using Microsoft.EntityFrameworkCore;

namespace NEWS.Models
{
    public partial class SocialfireContext : DbContext
    {
        public SocialfireContext()
        {
        }

        public SocialfireContext(DbContextOptions<SocialfireContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<CrawlerSite> CrawlerSites { get; set; } = null!;
        public virtual DbSet<Site> Sites { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-481MNPS;Database=Socialfire;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK__article2__3214D4A8749E12E8");

                entity.ToTable("article");

                entity.Property(e => e.Emotion).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.ResourceType).HasMaxLength(255);

                entity.Property(e => e.SiteId).HasColumnName("siteId");

                entity.Property(e => e.SiteName).HasMaxLength(255);

                entity.Property(e => e.SubjectName).HasMaxLength(255);

                entity.Property(e => e.Time).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.HasOne(d => d.subjectIdNavigition)
                    .WithMany()
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("37a42c86-8f06-4b08-98c5-5e0ea29ae6bd");
            });

            modelBuilder.Entity<CrawlerSite>(entity =>
            {
                entity.HasKey(e => e.SiteId)
                    .HasName("PK__CrawlerS__B9DCB9636BC251C5");

                entity.ToTable("crawlerSite");

                entity.Property(e => e.SiteId).ValueGeneratedNever();

                entity.Property(e => e.DescriptionSignal).HasMaxLength(50);

                entity.Property(e => e.ImageServerPath).HasMaxLength(50);

                entity.Property(e => e.ImageSignal).HasMaxLength(50);

                entity.Property(e => e.SiteName).HasMaxLength(50);

                entity.Property(e => e.SiteUrl).HasMaxLength(50);
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("site");

                entity.Property(e => e.SiteId)
                    .ValueGeneratedNever()
                    .HasColumnName("siteId");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.DescriptionSignal)
                    .HasMaxLength(50)
                    .HasColumnName("descriptionSignal");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .HasColumnName("host");

                entity.Property(e => e.ImageServerPath)
                    .HasMaxLength(50)
                    .HasColumnName("imageServerPath");

                entity.Property(e => e.ImageSignal)
                    .HasMaxLength(50)
                    .HasColumnName("imageSignal");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.SiteName)
                    .HasMaxLength(255)
                    .HasColumnName("siteName");

                entity.Property(e => e.SiteUrl)
                    .HasMaxLength(50)
                    .HasColumnName("siteUrl");

                entity.Property(e => e.Updated).HasColumnName("updated");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.SubjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("subjectId");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(400)
                    .HasColumnName("keywords");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Title)
                    .HasMaxLength(400)
                    .HasColumnName("title");

                entity.Property(e => e.Updated).HasColumnName("updated");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}