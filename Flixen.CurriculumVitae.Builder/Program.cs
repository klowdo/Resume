// See https://aka.ms/new-console-template for more information

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp;

Document
    .Create(container =>
    {
        var mainColor = "#E9DAC4";
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1.5f, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(20));

            page.Header()
                .Table(table =>
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
                                    Color = SKColor.Parse(mainColor),
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
                            .Background(mainColor))
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
         

            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
        });
    })
    .ShowInPreviewer();