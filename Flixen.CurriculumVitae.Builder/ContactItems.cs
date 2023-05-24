using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Builder;

public class ContactItems : IComponent
{
    private const int Padding = 7;
    private readonly IEnumerable<(string item, string text)> _items;

    public ContactItems(IEnumerable<(string item, string text)> items) 
        => _items = items;

    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn(8);
            });
            foreach (var (icon, text) in _items)
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