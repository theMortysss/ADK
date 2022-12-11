using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.models
{
    public class Department
    {
        [Column("id")] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("general_storage")] public int GeneralStorage { get; set; }
        [Column("cold_storage")] public int ColdStorage { get; set; }
        [Column("freezer_storage")] public int FreezerStorage { get; set; }

        public List<Goods> Goods { get; set; } = new List<Goods>();

        public override string ToString()
        {
            return Name;
        }
    }
}