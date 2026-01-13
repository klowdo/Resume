using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SkiaSharp;
using SkiaSharp.Extended.Iconify;

namespace Flixen.CurriculumVitae.Builder;

public class Footer : IComponent
{
    private readonly string _color;
    private const int IconSize = 30;
    private const int IconTextSize = 15;
    private const int ItemSpacing = 5;
    private const int RowSpacing = 30;

    public Footer(string color)
    {
        _color = color;
    }

    public void Compose(IContainer container)
    {
        var contactItems = new[]
        {
            (icon: "fa-envelope", text: "felix@flixen.se"),
            (icon: "fa-phone", text: "+46737120411")
        };

        container.AlignCenter().Row(row =>
        {
            row.Spacing(RowSpacing);
            foreach (var item in contactItems)
            {
                row.AutoItem().Component(new ContactItemIcon(_color, item.icon, item.text));
            }
        });
    }

    private class ContactItemIcon : IComponent
    {
        private readonly string _backgroundColor;
        private readonly string _iconName;
        private readonly string _text;

        public ContactItemIcon(string backgroundColor, string iconName, string text)
        {
            _backgroundColor = backgroundColor;
            _iconName = iconName;
            _text = text;
        }

        public void Compose(IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(ItemSpacing);
                col.Item()
                    .AlignCenter()
                    .Height(IconSize)
                    .Width(IconSize)
                    .Canvas(DrawIconWithBackground);
                col.Item()
                    .Text(_text)
                    .Bold()
                    .FontSize(IconTextSize);
            });
        }

        private void DrawIconWithBackground(SKCanvas canvas, QuestPDF.Helpers.Size size)
        {
            using var backgroundPaint = new SKPaint
            {
                Color = SKColor.Parse(_backgroundColor),
                IsStroke = false,
                IsAntialias = true
            };
            canvas.DrawRoundRect(0, 0, size.Width, size.Height, 100, 100, backgroundPaint);

            using var iconPaint = new SKPaint
            {
                TextAlign = SKTextAlign.Center,
                FilterQuality = SKFilterQuality.High,
                TextSize = IconTextSize,
                Typeface = SKTypeface.FromFamilyName("Arial")
            };
            var iconCode = "{{" + _iconName + " color=000000}}";
            canvas.DrawIconifiedText(iconCode, size.Width / 2, size.Height / 2 + iconPaint.TextSize / 3, iconPaint);
        }
    }
}