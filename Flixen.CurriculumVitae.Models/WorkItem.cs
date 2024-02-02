namespace Flixen.CurriculumVitae.Contracts;

public class WorkItem
{
    public string Name { get; set; } = string.Empty; 
    public string Place { get; set; }  = string.Empty; 
    public DateOnly From { get; set; }
    public DateOnly? To { get; set; }

    public string[] Items { get; set; } = Array.Empty<string>();
}