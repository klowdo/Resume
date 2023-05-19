using Flixen.CurriculumVitae.Contracts;
using Flixen.CurriculumVitae.Layouts;
using QuestPDF.Drawing;
using QuestPDF.Previewer;
using SkiaSharp.Extended.Iconify;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


var deserializer = new DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance) // see height_in_inches in sample yml 
    .Build();

var path = args.Length > 0 ? args[0] : "./data.yaml";
using var reader = File.OpenRead(path);
using var stream = new StreamReader(reader);
var options = deserializer.Deserialize<ResumeModel>(stream);

FontLoader.LoadFront(options.Fonts);
SKTextRunLookup.Instance.AddFontAwesome();
QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
FontManager.RegisterFont(FontAwesome.GetFontStream());
var document = new DefaultCvDocument(options);


document.ShowInPreviewer();