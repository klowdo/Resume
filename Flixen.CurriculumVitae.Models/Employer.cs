namespace Flixen.CurriculumVitae.Contracts;

public class Employer
{
    public string Name { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public Engagement[] Engagements { get; set; } = [];
}

public class Engagement
{
    public string? Client { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public string[] Items { get; set; } = [];
}
