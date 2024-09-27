using Flixen.CurriculumVitae.Contracts;
using Flixen.CurriculumVitae.Layouts;
using QuestPDF.Drawing;
using System.CommandLine;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp.Extended.Iconify;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

QuestPDF.Settings.License = LicenseType.Community;

var fileOption = new Option<FileInfo>(
    name: "--config-file",
    description: "The path to the configuration file",
    getDefaultValue: () =>  new FileInfo("./data.yaml")
);

var outputOption = new Option<FileInfo>(
    name: "--output",
    description: "The path to the configuration file"
) { IsRequired = true };

var liveCommand = new Command("live", "Starts a live preview of the resume");
var writeCommand = new Command("write", "Writes the resume to a file");
writeCommand.AddOption(outputOption);
var rootCommand = new RootCommand("A command line tool to generate a PDF resume")
{
    liveCommand,
    writeCommand
};
rootCommand.AddOption(fileOption);

liveCommand.SetHandler((configFile) =>
{
    var document = CreateDocument(configFile);
    document.ShowInPreviewer();
}, fileOption);

writeCommand.SetHandler((configFile, output) =>
{
    var document = CreateDocument(configFile);
    output.Directory?.Create();
    document.GeneratePdf(output.FullName);
}, fileOption, outputOption);

return await rootCommand.InvokeAsync(args); 

DefaultCvDocument CreateDocument(FileInfo configFile)
{
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance) // see height_in_inches in sample yml 
        .Build();

    using var reader = configFile.OpenRead();
    using var stream = new StreamReader(reader);
    var options = deserializer.Deserialize<ResumeModel>(stream);

    FontLoader.LoadFront(options.Fonts);
    SKTextRunLookup.Instance.AddFontAwesome();
    QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
    FontManager.RegisterFont(FontAwesome.GetFontStream());
    var defaultCvDocument = new DefaultCvDocument(options);
    return defaultCvDocument;
}
