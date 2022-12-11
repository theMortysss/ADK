using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataAccessLibrary.models
{
    public class Goods
    {
        [Column("id")] public int Id { get; set; }
        [Column("dept_id")] public int DepartmentId { get; set; }
        [Column("price")] public decimal Price { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("storage_kind")] public StorageRequirements StorageKind { get; set; }
        [Column("expiration_days")] public int ExpirationDaysCount { get; set; }

        public Department Department { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public List<Supplie> Supplies { get; set; } = new List<Supplie>();

        public const double HighPopularityThreshold = 0.25;
        public const int LongExpirationDaysCount = 90;

        [NotMapped] public double Popularity { get; set; }
        [NotMapped] public double StorageEase => GetStorageEase();
        [NotMapped] public int AvailableAmount => Supplies.Where(s => !s.IsExpired).Sum(s => s.RemainingQuantity);
        [NotMapped] public double StorageRating => (double)Price * Popularity * StorageEase;

        public List<Sale> GetRecentSales(int dayCount)
        {
            return Sales
                .Where(s => DateTime.Now.AddDays(-dayCount) < s.DateSold)
                .ToList();
        }

        private double GetStorageEase()
        {
            if (Popularity < HighPopularityThreshold)
                return ExpirationDaysCount < LongExpirationDaysCount ? 0.6 : 0.8;
            return ExpirationDaysCount < LongExpirationDaysCount ? 1.0 : 0.9;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}