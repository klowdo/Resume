using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class WorkItemsComponent(ResumeModel model) : IComponent
{
    private const float EmployerPadding = 30f;
    private const float EngagementPadding = 22f;

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(16);
            foreach (var employer in model.Employers)
            {
                col.Item().Layers(layers =>
                {
                    layers.PrimaryLayer()
                        .PaddingLeft(EmployerPadding)
                        .Column(employerCol =>
                        {
                            employerCol.Spacing(10);
                            employerCol.Item().Column(header =>
                            {
                                header.Item()
                                    .Text(employer.Name)
                                    .Bold()
                                    .FontSize(12);
                                header.Item()
                                    .Text(employer.Period)
                                    .FontColor(model.Colors.Muted)
                                    .Medium();
                            });

                            foreach (var engagement in employer.Engagements)
                            {
                                employerCol.Item()
                                    .ShowEntire()
                                    .Layers(engagementLayers =>
                                    {
                                        engagementLayers.PrimaryLayer()
                                            .PaddingLeft(EngagementPadding)
                                            .Component(new WorkComponent(engagement, model.Colors.Muted));

                                        engagementLayers.Layer()
                                            .Component(new DotsAndLines(EngagementPadding, 2.5f));
                                    });
                            }
                        });

                    layers.Layer().Component(new DotsAndLines(EmployerPadding, 5f));
                });
            }
        });
    }
}
