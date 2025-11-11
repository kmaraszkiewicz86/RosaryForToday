namespace RosaryForToday.Models.Dtos;

public class RosaryReflectionDto
{
    public int Id { get; set; }
    public int RosaryTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}