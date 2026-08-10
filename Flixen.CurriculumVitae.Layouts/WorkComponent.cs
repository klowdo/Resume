using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class WorkComponent(Engagement engagement, string mutedColor) : IComponent
{
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
                            .Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem()
                                    .PaddingHorizontal(5)
                                    .Text("•")
                                    .ExtraBold();
                                row.RelativeItem()
                                    .Text(item)
                                    .Medium();
                            });
                    }
                });
        });
    }
}
