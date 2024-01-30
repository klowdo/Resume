namespace Flixen.CurriculumVitae.Contracts;

public class ResumeOptions
{
    public ContactInforation Contact { get; set; }
    public string MainText { get; set; }
    public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    public List<string> Skills { get; set; }
}
