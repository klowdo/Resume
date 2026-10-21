using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class WorkComponent(Engagement engagement, string mutedColor) : IComponent
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
                    text.DefaultTextStyle(x => x.FontColor(mutedColor).Medium());
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
                                text.Span("•\u2002").ExtraBold();
                                text.Span(item).Medium();
                            });
                    }
                });
        });
    }
}
