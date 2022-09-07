namespace NEWS.Models
{
    public partial class Article
    {
        public long No { get; set; }
        public string? ResourceType { get; set; }
        public string? Url { get; set; }
        public string? Emotion { get; set; }
        public string? Title { get; set; }
        public string? SiteName { get; set; }
        public string? Time { get; set; }
        public DateTime? Date { get; set; }
        public string? SubjectName { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int? Process { get; set; }
        public int? SiteId { get; set; }
        public int? SubjectId { get; set; }
        public virtual Subject subjectIdNavigition { get; set; } = null!;
    }

    public class SubjectByID
    {
        public PaginationFilter? Pagination { get; set; }
        public int? SubjectId { get; set; }
    }

    public class ArticleList
    {
        public long No { get; set; }
        public string? ResourceType { get; set; }
        public string? Url { get; set; }
        public string? Emotion { get; set; }
        public string? Title { get; set; }
        public string? SiteName { get; set; }
        public string? Time { get; set; }
        public DateTime? Date { get; set; }
        public string? SubjectName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int? Process { get; set; }
        public int? SiteId { get; set; }
        public int? SubjectId { get; set; }
    }
}