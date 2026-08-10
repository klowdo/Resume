using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class DotsAndLines(float padding, float radius = 4f) : IComponent
{
    public void Compose(IContainer container)
    {
        var centerX = padding / 2;
        container.Svg(size => Render(size, centerX, radius));
    }

    private static string Render(Size size, float centerX, float radius)
    {
        var x = centerX.ToString(CultureInfo.InvariantCulture);
        var width = size.Width.ToString(CultureInfo.InvariantCulture);
        var height = size.Height.ToString(CultureInfo.InvariantCulture);
        var r = radius.ToString(CultureInfo.InvariantCulture);
        var inner = (radius / 2).ToString(CultureInfo.InvariantCulture);
        return $"""
                <svg width="{width}" height="{height}" xmlns="http://www.w3.org/2000/svg">
                    <line x1="{x}" y1="9" x2="{x}" y2="{height}" stroke="black" stroke-width="1" />
                    <circle cx="{x}" cy="9" r="{r}" fill="black" />
                    <circle cx="{x}" cy="9" r="{inner}" fill="white" />
                </svg>
                """;
    }
}
