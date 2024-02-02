namespace Flixen.CurriculumVitae.Contracts;

public class ResumeModel
{
    public required ResumeColors Colors { get; set; }
    public required ContactInforation Contact { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public List<WorkItem> WorkItems { get; set; } = new();
    public List<string> Skills { get; set; }
}

public class ResumeColors
{
    
    //Validate  ColorValidator.Validate(color);
    public string Main { get; set; } = "#eadac6";
    public string MainTextColor { get; set; } = "#eadac6";
    public string Background { get; set; } = "#fdf6eb";
} 

