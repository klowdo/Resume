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
            col.Item()
                .ShowOnce()
                .PaddingTop(10) //TODO: remove
                .Text("Skills".ToUpperInvariant())
                .FontSize(13)
                .LetterSpacing(0.2f);
            col.Item()
                .ShowOnce()
                .Column(column =>
                {
                    foreach (var skill in model.Skills)
                    {
                        column.Item().Row(row =>
                        {
                            row.Spacing(5);
                            row.AutoItem().Text("•").Bold(); // text or image
                            row.RelativeItem().Text(skill);
                        });
                    }
                });
        });
    }
}