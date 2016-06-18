using itechart.PerformanceReview.Data.Filters.Enum;

namespace itechart.PerformanceReview.Data.Filters
{
    public abstract class PagedFilter<TEntity, TSortFields> : BaseFilter<TEntity>
    {
        private bool descending = true;

        public uint? Count { get; set; }

        public uint? From { get; set; }

        public int? To { get; set; }

        public TSortFields[] SortBy { get; set; }

        public bool Descending
        {
            get { return descending; }
            set { descending = value; }
        }
    }
}