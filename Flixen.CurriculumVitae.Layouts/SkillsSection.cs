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
            col.Spacing(8);
            SideSectionComponent.Heading(col.Item(), "Skills");

            foreach (var group in model.Skills)
            {
                col.Item().ShowEntire().Column(groupCol =>
                {
                    groupCol.Spacing(3);
                    groupCol.Item().Text(group.Category).SemiBold();
                    SideSectionComponent.Bullets(groupCol.Item(), group.Items);
                });
            }
        });
    }
}

public class SideSectionComponent(SideSection section) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(8);
            Heading(col.Item(), section.Title);
            Bullets(col.Item(), section.Items);
        });
    }

    public static void Heading(IContainer container, string title) =>
        container.Text(title.ToUpperInvariant()).FontSize(13);

    // Bullet and text are one text block so PDF text extraction keeps them on the same line
    // (QuestPDF has no hanging indent, so wrapped lines sit under the bullet).
    public static void Bullets(IContainer container, IEnumerable<string> items) =>
        container.Column(col =>
        {
            col.Spacing(4);
            foreach (var item in items)
            {
                col.Item()
                    .Text(text =>
                    {
                        text.Span("•\u2002").Bold();
                        text.Span(item);
                    });
            }
        });
}
