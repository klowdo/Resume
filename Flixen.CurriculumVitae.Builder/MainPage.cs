using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace Flixen.CurriculumVitae.Builder;

public class MainPage : IComponent
{
    private readonly ResumeOptions _options;
    private readonly string _lineColor;

    public MainPage(ResumeOptions options, string lineColor)
    {
        _options = options;
        _lineColor = lineColor;
    }

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Spacing(30);
            col.Item()
                .PaddingTop(30)
                .AlignCenter()
                .PaddingHorizontal(30)
                .Text(_options.Contact.Name)
                .ExtraBold()
                .FontSize(40);

            col.Item()
                .PaddingHorizontal(35)
                .LineHorizontal(1.5f)
                .LineColor(_lineColor);

            col.Item()
                .PaddingHorizontal(10)
                .Text(_options.MainText);

            col.Item()
                .PaddingHorizontal(35)
                .LineHorizontal(1.5f)
                .LineColor(_lineColor);

            col.Item()
                .AlignCenter()
                .PaddingHorizontal(30)
                .Text("ARBETSLIVSERFARENHET")
                .ExtraBold()
                .FontSize(20);

            col.Item()
                .EnsureSpace()
                .Column(innerCol =>
                {
                    foreach (var workItem in _options.WorkItems)
                    {
                        innerCol.Item()
                            .EnsureSpace()
                            .DebugArea()
                            .Layers(layers =>
                            {
                                var padding = 30;
                                layers.PrimaryLayer()
                                    .PaddingHorizontal(padding)
                                    .Component(new WorkComponent(workItem));
                                
                                layers.Layer()
                                    .Canvas(((canvas, size) =>
                                    {
                                        using var paint = new SKPaint
                                        {
                                            Color = SKColor.Parse(Colors.Black),
                                            IsStroke = false,
                                            StrokeWidth = 2,
                                            IsAntialias = true
                                        };
                                        canvas.DrawCircle(size.Width - (int)(padding / 2), 9, 4, paint);
                                        var lineWidth = 2;
                                        canvas.DrawRect((size.Width) - (int)(padding / 2)-(int)(lineWidth/2), 10, lineWidth, size.Height, paint);
                                    }));
                            });

                    }
                });


        });
    }
}

// public class WorkItem
// {
//     public string Name { get; set; } = Placeholders.Name();
//     public string Place { get; set; } = Placeholders.Label();
//     public DateOnly From { get; set; } = DateOnly.FromDateTime(DateTime.Parse(Placeholders.DateTime()));
//     public DateOnly? To { get; set; } = DateOnly.FromDateTime(DateTime.Parse(Placeholders.DateTime()));
//
//     public string[] Items { get; set; } =
//     {
//         Placeholders.Sentence(),
//         Placeholders.Sentence(),
//         Placeholders.Sentence(),
//     };
// }

public class WorkComponent : IComponent
{
    private readonly WorkItem _item;

    public WorkComponent(WorkItem item)
    {
        _item = item;
    }

    public void Compose(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem()
                    .Text(_item.Name)
                    .FontSize(20);

                row.AutoItem()
                    .Text(text =>
                    {
                        text.Span(_item.From.Year.ToString());
                        text.Span("-");
                        if (_item.To.HasValue)
                            text.Span(_item.To.Value.Year.ToString());
                    });
            });

            col.Item()
                .Text(_item.Place);

            col.Item()
                .Column(itemCOl =>
                {
                    foreach (var item in _item.Items)
                    {
                        itemCOl.Item().Row(row =>
                        {
                            row.Spacing(5);
                            row.AutoItem().Text("•"); // text or image
                            row.RelativeItem().Text(item);
                        });
                    }
                });
        });
    }
}