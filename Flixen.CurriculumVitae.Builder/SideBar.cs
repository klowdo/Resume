using Flixen.CurriculumVitae.Builder.Options;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Builder;

public class SideBar : IComponent
{
    private readonly string _color;
    private readonly ResumeOptions _options;

    public SideBar(ResumeOptions options,string color)
    {
        _color = color;
        _options = options;
    }

    public void Compose(IContainer container)
    {
        container
            .Background(_color)
            .Container()
            .Height(PageSizes.A4.Height)
            .Padding(20)
            .Column(side =>
            {
                var contact = _options.Contact;
                side.Spacing(10);
                var paddingHorizontal = 5;
                side.Item()
                    .PaddingTop(20)
                    .Image("Resources/felix_evolve_small.jpg")
                    .FitUnproportionally()
                    .FitArea();
                side.Item()
                    .PaddingHorizontal(paddingHorizontal)
                    .Text("Kontakt")
                    .FontSize(20)
                    .Bold();
                side.Item()
                    .PaddingHorizontal(paddingHorizontal)
                    .Component(new ContactItems(new []
                    {
                        ("", contact.Phone),
                        ("", contact.Email),
                        ("", contact.Adress)
                    }));
               
                side.Item()
                    .PaddingHorizontal(paddingHorizontal)
                    .LineHorizontal(10, Unit.Mill)
                    .LineColor(Colors.White);

                side.Item()
                    .Text("Skills")
                    .FontSize(20);
                side.Item()
                    .PaddingHorizontal(paddingHorizontal)
                    .Column(column =>
                    {
                        foreach (var i in Enumerable.Range(1, 8))
                        {
                            column.Item().Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem().Text("•"); // text or image
                                row.RelativeItem().Text(Placeholders.Name());
                            });
                        }
                    });
            });
        
        
    }
   
}