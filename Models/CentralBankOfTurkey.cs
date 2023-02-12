using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models
{
    public partial class CentralBankOfTurkey
    {
        public CentralBankOfTurkey()
        {
            DailyRecordss = new HashSet<DailyRecords>();
        }

        [Key]
        public int Currencyid { get; set; }
        public string? Currencyname { get; set; }

        public virtual ICollection<DailyRecords> DailyRecordss { get; set; }

    }
}
