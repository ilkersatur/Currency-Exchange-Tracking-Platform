using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models
{
    public partial class DailyRecords
    {
        public int Id { get; set; }
        public int? Currencyid { get; set; }
        public decimal? Forexselling { get; set; }
        public DateTime? Exchangedate { get; set; }

        public virtual CentralBankOfTurkey? Currency { get; set; }
    }
}
