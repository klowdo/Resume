using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class DotsAndLines(float padding) : IComponent
{
    public void Compose(IContainer container)
    {
        var centerX = padding / 2;
        container.Svg(size => Render(size, centerX));
    }

    private static string Render(Size size, float centerX)
    {
        var x = centerX.ToString(CultureInfo.InvariantCulture);
        var width = size.Width.ToString(CultureInfo.InvariantCulture);
        var height = size.Height.ToString(CultureInfo.InvariantCulture);
        return $"""
                <svg width="{width}" height="{height}" xmlns="http://www.w3.org/2000/svg">
                    <line x1="{x}" y1="10" x2="{x}" y2="{height}" stroke="black" stroke-width="1" />
                    <circle cx="{x}" cy="9" r="4" fill="black" />
                    <circle cx="{x}" cy="9" r="2" fill="white" />
                </svg>
                """;
    }
}
