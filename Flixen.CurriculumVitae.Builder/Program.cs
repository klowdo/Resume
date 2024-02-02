using Flixen.CurriculumVitae.Contracts;
using Flixen.CurriculumVitae.Layouts;
using QuestPDF.Drawing;
using QuestPDF.Previewer;
using SkiaSharp.Extended.Iconify;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


var deserializer = new DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
    .Build();

var path = args.Length > 0 ? args[0] : "./data.yaml";
using var reader = File.OpenRead(path);
using var stream = new StreamReader(reader);
var options = deserializer.Deserialize<ResumeModel>(stream);
    
SKTextRunLookup.Instance.AddFontAwesome();

QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
var fontfolders = Directory.EnumerateFiles("Resources/Montserrat/static")
    .Concat(Directory.EnumerateFiles("Resources/Bebas Neue"));
foreach (var file in fontfolders)
{
    using var fontStream = File.OpenRead(file);
    FontManager.RegisterFont(fontStream);
}
FontManager.RegisterFont(FontAwesome.GetFontStream());
FontManager.RegisterFontWithCustomName("glyphs",File.OpenRead("Resources/fa-regular-400.ttf"));
FontManager.RegisterFontWithCustomName("solid-glyphs",File.OpenRead("Resources/fa-solid-900.ttf"));
var document = new DefaultCvDocument(options);


document.ShowInPreviewer();