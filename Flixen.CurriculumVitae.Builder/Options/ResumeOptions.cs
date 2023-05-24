namespace Flixen.CurriculumVitae.Builder.Options;

public class ResumeOptions
{
    public ContactInforation Contact { get; set; }
}

public class ContactInforation
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Adress { get; set; }
}