using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace Flixen.CurriculumVitae.Layouts;

public class DotsAndLines(float padding) : IComponent
{
    public void Compose(IContainer container)
    {
        container
            .Canvas((canvas, _) =>
            {
                using var paint = new SKPaint();
                paint.Color = SKColor.Parse(Colors.Black);
                paint.IsStroke = false;
                paint.StrokeWidth = 2;
                paint.IsAntialias = true;
                canvas.DrawCircle(padding / 2, 9, 4, paint);
                const float lineWidth = 1f;
                canvas.DrawRect(
                    padding / 2 - lineWidth / 2,
                    10,
                    lineWidth,
                    PageSizes.A4.Height,
                    paint
                );

                using var whitePaint = new SKPaint();
                paint.Color = SKColor.Parse(Colors.White);
                paint.IsStroke = false;
                paint.StrokeWidth = 2;
                paint.IsAntialias = true;
                canvas.DrawCircle(padding / 2, 9, 2, paint);
            });
    }
}