using DataAccessLibrary.models;

namespace GSS
{
    public class GoodBatch
    {
        public readonly Goods Good;
        public int Amount { get; set; }

        public GoodBatch(Goods good)
        {
            Good = good;
        }

        public string Name => Good.ToString();

        public string DepartmentName => $"Отдел \"{Good.Department}\"";

        public override string ToString()
        {
            return $"{Good}: {Amount}";
        }
    }
}