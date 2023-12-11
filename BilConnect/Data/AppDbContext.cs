using BilConnect.Models;
using BilConnect.Models.PostModels;
using BilConnect.Models.ReportModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;

namespace BilConnect.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Existing configuration for Post and ApplicationUser
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<PostReport>()
                .HasOne(pr => pr.ReportedPost)
                .WithMany() // or .WithOne() if that's your model
                .HasForeignKey(pr => pr.ReportedPostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the SellingPost to be a separate table
            modelBuilder.Entity<SellingPost>().ToTable("SellingPosts");
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<SellingPost> SellingPosts { get; set; } 
        public DbSet<PostReport> PostReports { get; set; }

        
    }

}
