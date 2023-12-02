using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class Report<T> : ReportBase where T : class
    {

        public virtual T? ReportedObj { get; set; }
    }
}
