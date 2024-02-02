using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SkiaSharp;
using SkiaSharp.Extended.Iconify;

namespace Flixen.CurriculumVitae.Builder;

public class Footer : IComponent
{
    private readonly string _color;
    private readonly int _iconSize = 30;

    public Footer(string color)
    {
        _color = color;
    }

    public void Compose(IContainer container)
    {

        container.AlignCenter().Row(row =>
        {
            row.Spacing(30);
            row.AutoItem().Column(col =>
            {
                col.Spacing(5);
                col.Item()
                    .AlignCenter()
                    .Height(_iconSize)
                    .Width(_iconSize)
                    .Canvas((canvas, size) =>
                    {
                        using var paint = new SKPaint
                        {
                            Color = SKColor.Parse(_color),
                            IsStroke = false,
                            StrokeWidth = 2,
                            IsAntialias = true
                        };
                        canvas.DrawRoundRect(0, 0, size.Width, size.Height, 100, 100, paint);

                        using var textPaint = new SKPaint();
                        textPaint.TextAlign = SKTextAlign.Center;
                        textPaint.FilterQuality = SKFilterQuality.High;
                        textPaint.TextSize = 15;
                        textPaint.Typeface = SKTypeface.FromFamilyName("Arial");
                        canvas.DrawIconifiedText("{{fa-envelope color=000000}}", size.Width / 2, size.Height / 2 + textPaint.TextSize/3,
                            textPaint);
                    });
                col.Item()
                    .Text("felix@flixen.se")
                    .Bold()
                    .FontSize(15);
            });
            row.AutoItem().Column(col =>
            {
                col.Spacing(5);
                col.Item()
                    .AlignCenter()
                    .Height(_iconSize)
                    .Width(_iconSize)
                    .Canvas((canvas, size) =>
                    {
                        using var paint = new SKPaint
                        {
                            Color = SKColor.Parse(_color),
                            IsStroke = false,
                            StrokeWidth = 2,
                            IsAntialias = true
                        };
                        canvas.DrawRoundRect(0, 0, size.Width, size.Height, 100, 100, paint);

                        using var textPaint = new SKPaint();
                        textPaint.TextAlign = SKTextAlign.Center;
                        textPaint.FilterQuality = SKFilterQuality.High;
                        textPaint.TextSize = 15;
                        textPaint.Typeface = SKTypeface.FromFamilyName("Arial");
                        canvas.DrawIconifiedText("{{fa-phone color=000000}}", size.Width / 2, size.Height / 2 + textPaint.TextSize/3,
                            textPaint);
                    });

                col.Item()
                    .Text("+46737120411")
                    .Bold()
                    .FontSize(15);;
            });
        });


        // container.Table(table =>
        // {
        //     table.ColumnsDefinition(def =>
        //     {
        //         def.RelativeColumn();
        //         def.RelativeColumn();
        //     });
        //     
        //     table.Cell()
        // });
    }
}