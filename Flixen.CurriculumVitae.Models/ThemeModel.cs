namespace Flixen.CurriculumVitae.Contracts;

public class ThemeModel
{
    public ResumeColors Colors { get; set; } = new();
    public FontInfo[] Fonts { get; set; } = Array.Empty<FontInfo>();
    public string Image { get; set; } = string.Empty;
}
