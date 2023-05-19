using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class ProfilePicture(ResumeModel model) : IComponent
{
    public void Compose(IContainer container)
    {
        container
            .Image(model.Image)
            .FitUnproportionally()
            .FitArea();
    }
}