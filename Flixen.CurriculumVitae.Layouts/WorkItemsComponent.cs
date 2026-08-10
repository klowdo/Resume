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
                            employerCol.Item().ShowEntire().Column(intro =>
                            {
                                intro.Spacing(10);
                                intro.Item()
                                    .Text(employer.Name)
                                    .Bold()
                                    .FontSize(12);
                                intro.Item()
                                    .Text(employer.Period)
                                    .FontColor(model.Colors.Muted)
                                    .Medium();

                                if (employer.Engagements.Length > 0)
                                {
                                    Engagement(intro.Item(), employer.Engagements[0]);
                                }
                            });

                            foreach (var engagement in employer.Engagements.Skip(1))
                            {
                                Engagement(employerCol.Item(), engagement);
                            }
                        });

                    layers.Layer().Component(new DotsAndLines(EmployerPadding, 5f));
                });
            }
        });
    }

    private void Engagement(IContainer container, Engagement engagement) =>
        container
            .ShowEntire()
            .Layers(layers =>
            {
                layers.PrimaryLayer()
                    .PaddingLeft(EngagementPadding)
                    .Component(new WorkComponent(engagement, model.Colors.Muted));

                layers.Layer()
                    .Component(new DotsAndLines(EngagementPadding, 2.5f));
            });
}
