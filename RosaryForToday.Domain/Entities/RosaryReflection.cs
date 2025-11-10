namespace RosaryForToday.Domain.Entities;

public class RosaryReflection
{
    public int Id { get; set; }
    public int RosaryTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    
    // Navigation properties
    public RosaryType RosaryType { get; set; } = null!;
    public Language Language { get; set; } = null!;
}
