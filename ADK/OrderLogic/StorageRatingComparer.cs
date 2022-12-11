using System.Collections.Generic;

namespace GSS
{
    public class StorageRatingComparer : IComparer<GoodBatch>
    {
        public int Compare(GoodBatch x, GoodBatch y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return -1 * x.Good.StorageRating.CompareTo(y.Good.StorageRating);
        }
    }
}