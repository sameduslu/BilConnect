using BilConnect.Data.Base;
using BilConnect.Data.Enums;
using BilConnect.Models.PostModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models.ReportModels
{
    public class PostReport : Report
    {


        public int ReportedPostId { get; set; }
        [ForeignKey("ReportedPostId")]

        public virtual Post? ReportedPost { get; set; }

    }
}
