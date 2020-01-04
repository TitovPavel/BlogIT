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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

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
                Sex = "0"
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            user.PasswordHash = hasher.HashPassword(user, "Admin");


            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
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
}

