using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class WorkComponent(WorkItem workItem) : IComponent
{
    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(3);
            col.Item()
                .Text(workItem.Name)
                .Bold()
                .FontSize(10)
                .LetterSpacing(0.1f);

            col.Item()
                .Text(text =>
                {
                    text.Span(workItem.From.Year.ToString());
                    text.Span(" - ");
                    text.Span(workItem.To.HasValue ? workItem.To.Value.Year.ToString() : "Present");
                });

            col.Item()
                .Text(workItem.Place);

            col.Item()
                .Column(itemCOl =>
                {
                    itemCOl.Spacing(5);
                    foreach (var item in workItem.Items)
                    {
                        itemCOl.Item()
                            .Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem()
                                    .PaddingHorizontal(5)
                                    .Text( "•" )
                                    .ExtraBold();;
                                row.RelativeItem()
                                    .Text(item)
                                    .Medium();
                            });
                    }
                });
        });
    }
}