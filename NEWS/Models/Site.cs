namespace NEWS.Models
{
    public partial class Site
    {
        public int SiteId { get; set; }
        public string? SiteName { get; set; }
        public string? SiteUrl { get; set; }
        public string? Host { get; set; }
        public string? ImageSignal { get; set; }
        public string? ImageServerPath { get; set; }
        public string? DescriptionSignal { get; set; }
        public string? Note { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}