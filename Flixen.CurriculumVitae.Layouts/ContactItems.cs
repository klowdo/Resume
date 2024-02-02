using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class ContactItems(IEnumerable<(string item, string text)> items) : IComponent
{
    private const int Padding = 12;

    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn(8);
            });
            foreach (var (icon, text) in items)
            {
                table.Cell()
                    .AlignTop()
                    .PaddingBottom(Padding)
                    .Text(icon)
                    .FontFamily("solid-glyphs")
                    .Fallback(x => x.FontFamily("glyphs"));
                table.Cell()
                    .AlignTop()
                    .PaddingBottom(Padding)
                    .Text(text);
            }
        });
    }
}