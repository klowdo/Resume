using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Drawing;

public static class FontLoader
{
    public static void LoadFront(IReadOnlyList<FontInfo> fonts)
    {
        foreach (var optionsFont in fonts)
        {
            if (optionsFont.Name is not null)
            {
                FontManager.RegisterFontWithCustomName(optionsFont.Name, File.OpenRead(optionsFont.Path));
                continue;
            }

            if (File.Exists(optionsFont.Path))
            {
                using var fontStream = File.OpenRead(optionsFont.Path);
                FontManager.RegisterFont(fontStream);
                continue;
            }

            foreach (var file in Directory.EnumerateFiles(optionsFont.Path))
            {
                using var fontStream = File.OpenRead(file);
                FontManager.RegisterFont(fontStream);
            }
        }
    }
}