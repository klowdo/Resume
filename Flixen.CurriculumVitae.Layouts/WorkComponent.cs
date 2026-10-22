using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class WorkComponent(Engagement engagement, ResumeColors colors) : IComponent
{
    private const float BulletIndent = 5f;

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(3);
            col.Item()
                .Text(engagement.Role)
                .SemiBold()
                .FontSize(9.5f);

            col.Item()
                .Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontColor(colors.Muted).Medium());
                    text.Span(engagement.Period);
                    if (!string.IsNullOrWhiteSpace(engagement.Client))
                    {
                        text.Span("   Client: ");
                        text.Span(engagement.Client).SemiBold();
                    }
                });

            col.Item()
                .Column(items =>
                {
                    items.Spacing(4);
                    foreach (var item in engagement.Items)
                    {
                        items.Item()
                            .PaddingLeft(BulletIndent)
                            .Text(text =>
                            {
                                text.Span("• ").ExtraBold();
                                text.Span(item).Medium();
                            });
                    }
                });

            if (engagement.Skills.Length > 0)
            {
                col.Item()
                    .PaddingLeft(BulletIndent)
                    .PaddingTop(3)
                    .Component(new SkillTags(engagement.Skills, colors));
            }
        });
    }
}

// Skills for one engagement as a row of small tags that wraps to the next line when needed.
// Each tag is its own text block so PDF text extraction keeps the skill names intact.
public class SkillTags(IEnumerable<string> skills, ResumeColors colors) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Inlined(inlined =>
        {
            inlined.HorizontalSpacing(4);
            inlined.VerticalSpacing(4);
            inlined.AlignLeft();

            foreach (var skill in skills)
            {
                inlined.Item()
                    .Border(0.5f)
                    .BorderColor(colors.Muted)
                    .CornerRadius(3)
                    .PaddingVertical(1.5f)
                    .PaddingHorizontal(5)
                    .Text(skill)
                    .FontSize(7.5f)
                    .FontColor(colors.Muted)
                    .Medium();
            }
        });
    }
}
