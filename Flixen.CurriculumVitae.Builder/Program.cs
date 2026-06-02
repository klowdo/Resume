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
    description: "The path to the resume data file",
    getDefaultValue: () =>  new FileInfo("../profile.yaml")
);

var themeOption = new Option<FileInfo>(
    name: "--theme-file",
    description: "The path to the theme file (colors, fonts, image)",
    getDefaultValue: () => new FileInfo("./theme.yaml")
);

var anonymousOption = new Option<bool>(
    name: "--anonymous",
    description: "Generate resume without contact information"
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
rootCommand.AddOption(themeOption);
rootCommand.AddOption(anonymousOption);

liveCommand.SetHandler((configFile, themeFile, anonymous) =>
{
    var document = CreateDocument(configFile, themeFile, anonymous);
    document.ShowInPreviewer();
}, fileOption, themeOption, anonymousOption);

writeCommand.SetHandler((configFile, themeFile, output, anonymous) =>
{
    var document = CreateDocument(configFile, themeFile, anonymous);
    output.Directory?.Create();
    document.GeneratePdf(output.FullName);
}, fileOption, themeOption, outputOption, anonymousOption);

return await rootCommand.InvokeAsync(args);

DefaultCvDocument CreateDocument(FileInfo configFile, FileInfo themeFile, bool anonymous)
{
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    var options = Deserialize<ResumeModel>(deserializer, configFile);
    var theme = Deserialize<ThemeModel>(deserializer, themeFile);

    options.Colors = theme.Colors;
    options.Fonts = theme.Fonts;
    options.Image = theme.Image;
    options.Anonymous = anonymous;

    FontLoader.LoadFront(options.Fonts);
    SKTextRunLookup.Instance.AddFontAwesome();
    QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
    FontManager.RegisterFont(FontAwesome.GetFontStream());
    var defaultCvDocument = new DefaultCvDocument(options);
    return defaultCvDocument;
}

static T Deserialize<T>(IDeserializer deserializer, FileInfo file)
{
    using var reader = file.OpenRead();
    using var stream = new StreamReader(reader);
    return deserializer.Deserialize<T>(stream);
}
