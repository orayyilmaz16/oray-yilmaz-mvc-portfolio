using OrayPortfolio.Domain.Common;

public class Project : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Technologies { get; set; }
    public string? ProjectUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsFeatured { get; set; }
}
