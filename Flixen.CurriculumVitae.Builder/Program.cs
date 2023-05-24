// See https://aka.ms/new-console-template for more information

using Flixen.CurriculumVitae.Builder;
using Flixen.CurriculumVitae.Builder.Options;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.HarfBuzz;

var options = new ResumeOptions
{
Contact = new ContactInforation
{
    Name = "Felix Svensson",
    Phone = "+46737120411",
    Email = "felix@flixen.se",
    Adress = "Lillekärr Norra 56,\n425 34 Hissing Kärra"
}
};

SKTextRunLookup.Instance.AddFontAwesome();

QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = true;
FontManager.RegisterFont(FontAwesome.GetFontStream());
FontManager.RegisterFontWithCustomName("glyphs",File.OpenRead("Resources/fa-regular-400.ttf"));
FontManager.RegisterFontWithCustomName("solid-glyphs",File.OpenRead("Resources/fa-solid-900.ttf"));
Document
    .Create(container =>
    {
        var mainColor = "#E9DAC4";
        var backgorund = "#EAE3C9";
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