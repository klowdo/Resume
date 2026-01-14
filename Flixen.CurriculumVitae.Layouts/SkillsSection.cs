using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class SkillsSection(ResumeModel model) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(10);
            col.Item()
                .ShowOnce()
                .Text("Skills".ToUpperInvariant())
                .FontSize(13)
                .LetterSpacing(0.2f);
            col.Item()
                .ShowOnce()
                .Column(column =>
                {
                    foreach (var skill in model.Skills)
                    {
                        column.Spacing(4);
                        column.Item()
                            .Row(row =>
                            {
                                row.Spacing(8);
                                row.AutoItem().Text("•").Bold(); // text or image
                                row.RelativeItem().Text(skill);
                            });
                    }
                });
        });
    }
}