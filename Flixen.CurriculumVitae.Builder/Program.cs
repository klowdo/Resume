// See https://aka.ms/new-console-template for more information

using Flixen.CurriculumVitae.Builder;
using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using SkiaSharp.Extended.Iconify;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;



var deserializer = new DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
    .Build();

using var reader = File.OpenRead(args[0]);
using var stream = new StreamReader(reader);
var options = deserializer.Deserialize<ResumeOptions>(stream);
    
SKTextRunLookup.Instance.AddFontAwesome();

QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
FontManager.RegisterFont(FontAwesome.GetFontStream());
FontManager.RegisterFontWithCustomName("glyphs",File.OpenRead("Resources/fa-regular-400.ttf"));
FontManager.RegisterFontWithCustomName("solid-glyphs",File.OpenRead("Resources/fa-solid-900.ttf"));
Document
    .Create(container =>
    {
        var mainColor = "#eadac6";
        var backgorund = "#fdf6eb";
        container.Page(page =>
        {
            page.PageColor(backgorund);
            page.Size(PageSizes.A4);

          
            page.Content()
                .Row(row =>
                {
                    
                    row.RelativeItem(2)
                        .Component(new SideBar( options, mainColor));

                    row.RelativeItem(3)
                        .Component(new MainPage(options, mainColor));
                });
            

            // page.Content()
            //     .Table(table =>
            //     {
            //         table.ColumnsDefinition(def =>
            //         {
            //             def.RelativeColumn();
            //             def.RelativeColumn(2);
            //         });
            //         table.Cell()
            //             .Background(mainColor)
            //             .Padding(20)
            //             .Column(col =>
            //             {
            //                 col.Item()
            //                     .Image("Resources/felix_evolve_small.jpg")
            //                     .FitUnproportionally()
            //                     .FitArea();
            //                 col.Item()
            //                     .Text("Kontakt")
            //                     .Bold();
            //                 col.Item()
            //                     .Text("email");
            //                 col.Item()
            //                     .Text("phone");
            //                 col.Item()
            //                     .Text("Adress");
            //             });
            //
            //         table.ExtendLastCellsToTableBottom();
            //     });

            // page.Margin(1.5f, Unit.Centimetre);
            // page.PageColor(Colors.White);
            // page.DefaultTextStyle(x => x.FontSize(20));
            //
            // page.Header()
            //     .Component(new Header(mainColor));
            //     
            // page.Footer()
            //     .Component(new Footer(mainColor));

            // page.Footer()
            //     .AlignCenter()
            //     .Text(x =>
            //     {
            //         x.Span("Page ");
            //         x.CurrentPageNumber();
            //     });
        });
    })
    .ShowInPreviewer();