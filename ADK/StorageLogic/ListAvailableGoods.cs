using DataAccessLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.StorageLogic
{
    public class ListAvailableGoods
    {
        public List<GoodBatch> goodsList = new List<GoodBatch>();
        public ListAvailableGoods()
        {
            using (GoodsContext db = new GoodsContext())
            {
                var goods = db.goods.ToList();
                var supplies = db.supplies.ToList();             
            }
        }
    }
}
