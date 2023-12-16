using BilConnect.Models.PostModels;
using BilConnect.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilConnect.Data.ViewModels
{
    public class UserProfileVM
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
