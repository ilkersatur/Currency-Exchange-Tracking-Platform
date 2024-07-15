using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotnetProject.Models
{
    public partial class Currency
    {
        [Key]
        public int Id { get; set; }
        public int? Currencyid { get; set; }
        public decimal? Forexselling { get; set; }
        public DateTime? Exchangedate { get; set; }

        public virtual Centralbankofturkey? Currencies { get; set; }
    }
}
