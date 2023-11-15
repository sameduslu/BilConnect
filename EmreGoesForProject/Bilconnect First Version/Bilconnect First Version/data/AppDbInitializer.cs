using Bilconnect_First_Version.data.enums;
using Bilconnect_First_Version.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Bilconnect_First_Version.data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                // Users
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            Name = "Emre with Beard",
                            Description = "Im able to grow beard",
                            ImageURL = "https://emre-akgul.github.io/PersonalWebsite/UserPhotos/Sakall%C4%B1Emre.jfif"
                        },

                        new User()
                        {
                            Name = "Emre without Beard",
                            Description = "Im not able to grow beard",
                            ImageURL = "https://emre-akgul.github.io/PersonalWebsite/UserPhotos/Sakals%C4%B1zEmre.jfif"
                        },

                        new User()
                        {
                            Name = "Angelina Jolie",
                            Description = "Perfect",
                            ImageURL = "https://emre-akgul.github.io/PersonalWebsite/UserPhotos/AngelinaJolie.jfif"
                        },


                    });
                    context.SaveChanges();
                }

                // Posts
                if (!context.Posts.Any())
                {
                    var users = context.Users.ToList(); // Assuming you have some users
                    context.Posts.AddRange(new List<Post>()
                    {
                        new Post()
                        {
                            Title = "Shoe",
                            Description = "Nike Shoe",
                            Price = 6.0,
                            ImageURL = "https://emre-akgul.github.io/PersonalWebsite/PostPhotos/Ayakkab%C4%B1.jfif",
                            PostDate = DateTime.Now,
                            UserId = users[0].Id // Assign to a user
                        },
                        new Post()
                        {
                            Title = "Mug",
                            Description = "Writes E on it",
                            Price = 5.0,
                            ImageURL = "https://emre-akgul.github.io/PersonalWebsite/PostPhotos/EBardak.jfif",
                            PostDate = DateTime.Now,
                            UserId = users[1].Id // Assign to a user
                        },
                        new Post()
                        {
                            Title = "Toy Car",
                            Description = "Classic Haci Murat Car",
                            Price = 120.0,
                            ImageURL = "https://emre-akgul.github.io/PersonalWebsite/PostPhotos/Sar%C4%B1Hac%C4%B1Murat.jfif",
                            PostDate = DateTime.Now,
                            UserId = users[2].Id // Assign to a user
                        },

                        // Add more posts as needed
                    });
                    context.SaveChanges();
                }

                // Add more entities like Cinemas, Actors, Producers, Movies, Actors_Movies as in your current code
            }
        }
    }
}
