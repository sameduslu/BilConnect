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

        /*    // Chat - UserChat
            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // User - UserChat
            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);*/

            // Post - Chat
            modelBuilder.Entity<Post>()
                 .HasMany(p => p.Chats)
                 .WithOne(c => c.RelatedPost)
                 .OnDelete(DeleteBehavior.NoAction);

            // Chat - Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete




            // Configure the SellingPost to be a separate table
            modelBuilder.Entity<SellingPost>().ToTable("SellingPosts");
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<SellingPost> SellingPosts { get; set; } 
        public DbSet<PostReport> PostReports { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }


    }

}
