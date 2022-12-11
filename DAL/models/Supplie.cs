using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.models
{
    // [Table("supplies")]
    public class Supplie
    {
        [Column("id")] public int Id { get; set; }
        [Column("good_id")] public int GoodId { get; set; }

        [Column("date_supplied")] public DateTime DateSupplied { get; set; }

        [Column("date_sold")] public DateTime? DateSold { get; set; }
        [Column("remaining_qty")] public int RemainingQuantity { get; set; }

        public Goods Good { get; set; }

        [NotMapped] public bool IsExpired => DateSupplied.AddDays(Good.ExpirationDaysCount) < DateTime.Now;

        public string GoodName => Good.Name;

        public Supplie()
        {
        }

        public Supplie(Goods good, int amount, DateTime dateSupplied)
        {
            Good = good;
            RemainingQuantity = amount;
            DateSupplied = dateSupplied;
        }
    }
}