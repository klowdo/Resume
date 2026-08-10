namespace Flixen.CurriculumVitae.Contracts;

public class ResumeModel
{
    public required ResumeColors Colors { get; set; }
    public required ContactInforation Contact { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MainText { get; set; } = string.Empty;
    public Employer[] Employers { get; set; } = [];
    public SkillGroup[] Skills { get; set; } = [];
    public SideSection[] Sections { get; set; } = [];
    public FontInfo[] Fonts { get; set; } = [];
    public bool Anonymous { get; set; }
}
