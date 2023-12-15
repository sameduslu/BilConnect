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
            
            modelBuilder.Entity<Chat>()
               .HasOne(c => c.User)
               .WithMany(u => u.SenderChats)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Receiver)
                .WithMany(u => u.ReceiverChats)
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


            // Post - Chat
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.RelatedPost)
                .WithMany(p => p.Chats)
                .HasForeignKey(c => c.RelatedPostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Chat - Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade); 

            // User - Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict); 


            // Configure the SellingPost to be a separate table
            modelBuilder.Entity<SellingPost>().ToTable("SellingPosts");
            modelBuilder.Entity<DonationPost>().ToTable("DonationPosts");
            modelBuilder.Entity<BorrowingPost>().ToTable("BorrowingPost");
            modelBuilder.Entity<EventTicketPost>().ToTable("EventTicketPost");
            modelBuilder.Entity<FoundItemPost>().ToTable("FoundItemPost");
            modelBuilder.Entity<LostItemPost>().ToTable("LostItemPost");
            modelBuilder.Entity<PetAdoptionPost>().ToTable("PetAdoptionPost");
            modelBuilder.Entity<TravellingPost>().ToTable("TravellingPost");

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReport> PostReports { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }


    }

}
