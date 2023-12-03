using BilConnect.Models;
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

            // Configure the relationship between PostReport, ApplicationUser, and Post
            modelBuilder.Entity<PostReport>()
                .HasOne(pr => pr.Reporter)
                .WithMany()
                .HasForeignKey(pr => pr.ReporterId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust as needed

            modelBuilder.Entity<PostReport>()
                .HasOne(pr => pr.ReportedPost)
                .WithMany()
                .HasForeignKey(pr => pr.ReportedPostId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust as needed
        }
    


        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReport> PostReports { get; set; }

    }
}
