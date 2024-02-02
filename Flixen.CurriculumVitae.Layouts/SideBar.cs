using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class SideBar(ResumeModel model) : IComponent
{
    public void Compose(IContainer container)
    {
        container
            .DefaultTextStyle(x => x
                .FontColor(model.Colors.MainTextColor)
                .FontFamily("Montserrat")
                .Light()
                .FontSize(9)
            )
            .Background(model.Colors.Main)
            .Container()
            .Padding(20)
            .Column(side =>
            {
                side.Spacing(10);
                var contact = model.Contact;
                var paddingHorizontal = 10;
                side.Item()
                    .PaddingTop(20)
                    .Component(new ProfilePicture(model));

                side.Item()
                    .PaddingTop(20)
                    .PaddingHorizontal(paddingHorizontal)
                    .Component(new ContactItems(new[]
                    {
                        ("", contact.Phone),
                        ("", contact.Email),
                        ("", contact.Address)
                    }));

                side.Item()
                    .PaddingHorizontal(paddingHorizontal)
                    .LineHorizontal(10, Unit.Mil)
                    .LineColor(model.Colors.MainTextColor);

                side.Item()
                    .PaddingTop(10)
                    .PaddingHorizontal(paddingHorizontal)
                    .Component(new SkillsSection(model));
            });
    }
}