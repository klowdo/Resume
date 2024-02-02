using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace Flixen.CurriculumVitae.Layouts;

public class Header: IComponent
{
    private readonly string _color;

    public Header(string color)
    {
        _color = color;
    }
    public void Compose(IContainer container)
    {
        container.Table(table =>
                {
                    var cellHeight = 150;
                    table.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(cellHeight);
                        c.RelativeColumn();
                    });
                    var padding = 30;

                    table.Cell()
                        .Height(cellHeight)
                        .Layers(layers =>
                        {
                            layers.Layer().Canvas((canvas, size) =>
                            {
                                using var paint = new SKPaint
                                {
                                    Color = SKColor.Parse(_color),
                                    IsStroke = false,
                                    StrokeWidth = 2,
                                    IsAntialias = true
                                };
                                canvas.DrawRoundRect(0, 0, size.Width, size.Height, 100, 100, paint);
                                canvas.DrawRect(size.Width / 2, padding, size.Width / 2 + 1, size.Height - (padding * 2),
                                    paint);
                            });

                            layers.PrimaryLayer()
                                .Padding(5)
                                .Image("Resources/felix_evolve-rund.png")
                                .FitArea();
                        });



                 table.Cell()
                        .Element(c => c
                            .PaddingVertical(padding)
                            .Background(_color))
                        .Padding(13)
                        .Column(column =>
                        {
                            column.Item()
                                .AlignRight()
                                .Text("Felix Svensson")
                                .FontSize(30)
                                .Bold();
                            column.Item()
                                .AlignRight()
                                .Text("SOFTWARE DEVELOPER")
                                .Bold()
                                .FontSize(10);
                        });
                        
                    
                });
         ;
    }
}