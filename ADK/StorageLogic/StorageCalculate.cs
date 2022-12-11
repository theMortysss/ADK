using DataAccessLibrary;
using DataAccessLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.StorageLogic
{
    public class StorageCalculate
    {
        public static StorageParameters CalculateStorageParameters(List<Department> departments)
        {
            var gen = departments.Sum(s => 
            {
                var amount = s.Goods.Where(z => z.StorageKind == StorageRequirements.General).Sum(z => z.AvailableAmount);
                return s.GeneralStorage - amount;
            });
            var frez = departments.Sum(s =>
            {
                var amount = s.Goods.Where(z => z.StorageKind == StorageRequirements.Freezer).Sum(z => z.AvailableAmount);
                return s.FreezerStorage - amount;
            });
            var cold = departments.Sum(s => {
                var amount = s.Goods.Where(z => z.StorageKind == StorageRequirements.Cold).Sum(z => z.AvailableAmount);
                return s.ColdStorage - amount;
            });
            return new StorageParameters(gen, cold, frez);
        }
    }
}
