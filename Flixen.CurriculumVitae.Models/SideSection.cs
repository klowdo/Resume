namespace Flixen.CurriculumVitae.Contracts;

public class SideSection
{
    public string Title { get; set; } = string.Empty;
    public string[] Items { get; set; } = [];
}

public class SkillGroup
{
    public string Category { get; set; } = string.Empty;
    public string[] Items { get; set; } = [];
}
