using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.models
{
    public class Sale
    {
        [Column("id")] public int Id { get; set; }
        [Column("good_id")] public int GoodId { get; set; }

        [Column("good_count")] public int SoldAmount { get; set; }

        [Column("date_sold")] public DateTime DateSold { get; set; }
        public Goods Good { get; set; }

        public int RemainingAmount => Good.AvailableAmount;
        public string GoodName => Good.Name;
        public decimal GoodPrice => Good.Price;

        public override string ToString()
        {
            return $"[{DateSold}] {Good}: {SoldAmount}";
        }

        public Sale()
        {
        }

        public Sale(Goods good, int soldAmount, DateTime dateSold)
        {
            Good = good;
            GoodId = good.Id;
            SoldAmount = soldAmount;
            DateSold = dateSold;
        }
    }
}