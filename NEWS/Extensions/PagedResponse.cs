namespace NEWS.Extensions
{
    public class PagedResponse<T> : ResultMessageResponse
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public int totalItem { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.data = data;
            this.message = null;
            this.success = true;
            this.errors = null;
            this.totalItem = 0;
        }
    }
}