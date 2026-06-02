using Flixen.CurriculumVitae.Contracts;
using Flixen.CurriculumVitae.Layouts;
using System.CommandLine;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Companion;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

QuestPDF.Settings.License = LicenseType.Community;

var fileOption = new Option<FileInfo>("--config-file")
{
    Description = "The path to the resume data file",
    DefaultValueFactory = _ => new FileInfo("../profile.yaml"),
    Recursive = true
};

var themeOption = new Option<FileInfo>("--theme-file")
{
    Description = "The path to the theme file (colors, fonts, image)",
    DefaultValueFactory = _ => new FileInfo("./theme.yaml"),
    Recursive = true
};

var anonymousOption = new Option<bool>("--anonymous")
{
    Description = "Generate resume without contact information",
    Recursive = true
};

var outputOption = new Option<FileInfo>("--output")
{
    Description = "The path to the output PDF file",
    Required = true
};

var liveCommand = new Command("live", "Starts a live preview of the resume");
var writeCommand = new Command("write", "Writes the resume to a file");
writeCommand.Options.Add(outputOption);

var rootCommand = new RootCommand("A command line tool to generate a PDF resume");
rootCommand.Options.Add(fileOption);
rootCommand.Options.Add(themeOption);
rootCommand.Options.Add(anonymousOption);
rootCommand.Subcommands.Add(liveCommand);
rootCommand.Subcommands.Add(writeCommand);

liveCommand.SetAction(parseResult =>
{
    var document = CreateDocument(
        parseResult.GetValue(fileOption)!,
        parseResult.GetValue(themeOption)!,
        parseResult.GetValue(anonymousOption));
    document.ShowInCompanion();
});

writeCommand.SetAction(parseResult =>
{
    var document = CreateDocument(
        parseResult.GetValue(fileOption)!,
        parseResult.GetValue(themeOption)!,
        parseResult.GetValue(anonymousOption));
    var output = parseResult.GetValue(outputOption)!;
    output.Directory?.Create();
    document.GeneratePdf(output.FullName);
});

return rootCommand.Parse(args).Invoke();

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
    QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
    var defaultCvDocument = new DefaultCvDocument(options);
    return defaultCvDocument;
}

static T Deserialize<T>(IDeserializer deserializer, FileInfo file)
{
    using var reader = file.OpenRead();
    using var stream = new StreamReader(reader);
    return deserializer.Deserialize<T>(stream);
}
