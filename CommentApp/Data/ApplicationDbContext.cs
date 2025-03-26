using CommentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Post entity
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                       .HasDefaultValueSql("gen_random_uuid()");
                entity.Property(p => p.Content)                      
                      .HasMaxLength(1000);
                entity.Property(p => p.ImgageUrl)                      
                      .HasMaxLength(500);
                entity.Property(q => q.Timestamp)
                      .HasDefaultValueSql("NOW()");                
                entity.HasOne(p => p.User)
                      .WithMany()
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Comment entity
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(p => p.Id)
                       .HasDefaultValueSql("gen_random_uuid()");
                entity.Property(c => c.Text)
                      .IsRequired()
                      .HasMaxLength(500);
                entity.Property(q => q.Timestamp)
                      .HasDefaultValueSql("NOW()");
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.User)
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(p => p.Id)
                      .HasDefaultValueSql("gen_random_uuid()");
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.Parse("c9bf9e57-1685-4c89-bafb-ff5af830be8a"), Username = "Change can", Email = "john@example.com", PasswordHash = "hashed_password" },
                new User { Id = Guid.Parse("2f69d9bb-5e65-46ce-a292-420e9a137e6a"), Username = "Blend 285", Email = "jane@example.com", PasswordHash = "hashed_password2" }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = Guid.Parse("50d4a73e-1c12-415a-97c3-169120bf24d9"), Content = "", ImgageUrl= "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSFUAfyVe3Easiycyh3isP9wDQTYuSmGPsPQvLIJdEYvQ_DsFq5Ez2Nh_QjiS3oZ3B8ZPfK9cZQyIStmQMV1lDPLw", UserId = Guid.Parse("c9bf9e57-1685-4c89-bafb-ff5af830be8a") }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = Guid.NewGuid(), Text = "have a good day", PostId = Guid.Parse("50d4a73e-1c12-415a-97c3-169120bf24d9"), UserId = Guid.Parse("2f69d9bb-5e65-46ce-a292-420e9a137e6a") }
            );
        }
    }
}
