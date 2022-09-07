namespace NEWS.Models
{
    public partial class Subject
    {
        public int SubjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? Order { get; set; }
    }
}