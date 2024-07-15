using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotnetProject.Models
{
    public partial class Centralbankofturkey
    {
        public Centralbankofturkey()
        {
            Dailyrecords = new HashSet<Currency>();
        }
        [Key]
        public int Currencyid { get; set; }
        public string? Currencyname { get; set; }

        public virtual ICollection<Currency> Dailyrecords { get; set; }
    }
}
