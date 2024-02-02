namespace Flixen.CurriculumVitae.Contracts;

public class WorkItem
{
    public string Name { get; set; } 
    public string Place { get; set; }
    public DateOnly From { get; set; }
    public DateOnly? To { get; set; }

    public string[] Items { get; set; }
}