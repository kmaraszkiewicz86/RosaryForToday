namespace RosaryForToday.Domain.Entities;

public class Language
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<RosaryType> RosaryTypes { get; set; } = new List<RosaryType>();
    public ICollection<RosaryReflection> RosaryReflections { get; set; } = new List<RosaryReflection>();
}
