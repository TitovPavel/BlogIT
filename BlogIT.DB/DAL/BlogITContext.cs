using BlogIT.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BlogIT.DB.DAL
{
    public class BlogITContext : IdentityDbContext<User>
    {
        public BlogITContext(DbContextOptions<BlogITContext> options)
            : base(options)
        { }

        public DbSet<FileModel> Files { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new TagsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsTagsConfiguration());

            Initialization(modelBuilder);
        }

        private void Initialization(ModelBuilder modelBuilder)
        {
            const string ADMIN_ID = "2ea66a9a-1bf0-418a-a9f7-bb00b3a71955";
            const string ROLE_ID = "ccfabe84-124f-473b-81a0-5da1d8ab4857";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole("admin") { Id = ROLE_ID, ConcurrencyStamp = "81b2fd77-9615-4927-98f6-0c8dce30a290", NormalizedName = "ADMIN" },
                new IdentityRole("user") { Id = "53dda6a0-e534-4fac-b3d2-145a7c3e2752", ConcurrencyStamp = "8eb0db2d-383a-4d73-96a2-c1e29cf58af5", NormalizedName = "USER" });

            User user = new User()
            {
                UserName = "Admin",
                Id = ADMIN_ID,
                ConcurrencyStamp = "9a9e2880-e7d9-4b64-add4-795ac26a36bb",
                NormalizedUserName = "ADMIN",
                Email = "Admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                Sex = "0",
                PasswordHash = "AQAAAAEAACcQAAAAEHuKKBDgB7OXg5nK+FMQpZGrnKhE+xrcAP3G6dMGSsh4Xt4zIfTa15arL/soZkLu2A=="
            };

            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            modelBuilder.Entity<Category>().HasData(
               new Category() { Id = 1, Title = "Java" },
               new Category() { Id = 2, Title = "C#" },
               new Category() { Id = 3, Title = "C++" },
               new Category() { Id = 4, Title = "Algorithms" },
               new Category() { Id = 5, Title = "Machine Learning" });

        }
    }

    public class FileConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Path).IsRequired().HasMaxLength(256);
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Sex).IsRequired();
            builder.Property(p => p.Birthday).IsRequired();
            builder.HasOne(p => p.Avatar).WithOne().HasForeignKey<User>(p => p.AvatarId);
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(256);
        }
    }

    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.DateTime).IsRequired();
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.Tags).IsRequired().HasMaxLength(1024);
            builder.Property(p => p.NewsText).IsRequired().HasMaxLength(10240);
            builder.Property(p => p.Deleted).IsRequired().HasDefaultValue(false);
            builder.HasOne(p => p.Category)
               .WithMany(t => t.News)
               .HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.Writer)
                .WithMany(t => t.News)
                .HasForeignKey(p => p.WriterId);
        }
    }

    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Message).IsRequired().HasMaxLength(1024);
            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.News)
                .WithMany(t => t.ChatMessages)
                .HasForeignKey(p => p.NewsId);
        }
    }

    public class TagsConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(256);
        }
    }

    public class NewsTagsConfiguration : IEntityTypeConfiguration<NewsTag>
    {
        public void Configure(EntityTypeBuilder<NewsTag> builder)
        {
            builder.HasKey(bc => new
                {
                    bc.NewsId,
                    bc.TagId
                });

            builder.HasOne(bc => bc.News)
                .WithMany(b => b.NewsTag)
                .HasForeignKey(bc => bc.NewsId);

            builder.HasOne(bc => bc.Tag)
                .WithMany(c => c.NewsTag)
                .HasForeignKey(bc => bc.TagId);
        }
    }
}

