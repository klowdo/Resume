using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class MainPage(ResumeModel model) : IComponent
{

    public void Compose(IContainer container)
    {
        container.DefaultTextStyle(x => x
                .FontFamily("Montserrat")
                .Light()
                .FontSize(9)
            )
            .PaddingTop(30)
            .PaddingHorizontal(30)
            .Column(col =>
            {
                col.Spacing(15);
                col.Item()
                    .AlignLeft()
                    .Text(model.Contact.Name)
                    .FontFamily("Bebas Neue")
                    .FontSize(67)
                    .LetterSpacing(0.05f)
                    .LineHeight(0.8f);

                col.Item()
                    .Text(model.Title.ToUpperInvariant())
                    .FontSize(16)
                    .LetterSpacing(0.1f)
                    .NormalWeight();
                
                col.Item()
                    .Text(model.MainText);

                col.Item()
                    .Text("PROFESSIONAL EXPERIENCE")
                    .FontSize(13)
                    .LetterSpacing(0.1f)
                    .NormalWeight();

                col.Item()
                    .Component(new WorkItemsComponent(model));
            });
    }
}
