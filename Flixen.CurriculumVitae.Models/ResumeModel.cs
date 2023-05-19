namespace Flixen.CurriculumVitae.Contracts;

public class ResumeModel
{
    public required ResumeColors Colors { get; set; }
    public required ContactInforation Contact { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public WorkItem[] WorkItems { get; set; } = Array.Empty<WorkItem>();
    public string[] Skills { get; set; } = Array.Empty<string>();
    public FontInfo[] Fonts { get; set; } = Array.Empty<FontInfo>();
}