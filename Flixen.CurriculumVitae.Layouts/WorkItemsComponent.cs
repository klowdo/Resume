using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class WorkItemsComponent(ResumeModel model) : IComponent
{
    public void Compose(IContainer container)
    {
        var padding = 30f;
        var spacing = 15;
        container
            .Column(col =>
            {
                col.Spacing(spacing);
                foreach (var workItem in model.WorkItems)
                {
                    col.Item()
                        .ShowEntire()
                        .Layers(layers =>
                        {
                            layers.PrimaryLayer()
                                .PaddingLeft(padding)
                                .Component(new WorkComponent(workItem));

                            layers.Layer()
                                .Component(new DotsAndLines(padding));
                        });
                }
            });
    }
}