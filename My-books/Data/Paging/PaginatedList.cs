namespace My_books.Data.Paging
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(Count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }
        public bool HasNextPage
        {
            get 
            { 
                return PageIndex < TotalPages;
            }
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize) 
        { 
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items,count, pageIndex, pageSize);
        }
    }
}
