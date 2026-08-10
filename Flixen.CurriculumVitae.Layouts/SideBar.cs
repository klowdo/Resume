using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class SideBar(ResumeModel model) : IComponent
{
    private const int PaddingHorizontal = 10;

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
                side.Spacing(14);
                var contact = model.Contact;

                side.Item()
                    .ShowOnce()
                    .PaddingTop(20)
                    .Component(new ProfilePicture(model));

                if (!model.Anonymous)
                {
                    side.Item()
                        .ShowOnce()
                        .PaddingTop(20)
                        .PaddingHorizontal(PaddingHorizontal)
                        .Component(new ContactItems(new[]
                        {
                            ("", contact.Phone),
                            ("", contact.Email),
                            ("", contact.Location),
                            ("", contact.Github)
                        }));
                }

                side.Item()
                    .ShowOnce()
                    .PaddingHorizontal(PaddingHorizontal)
                    .LineHorizontal(10, Unit.Mil)
                    .LineColor(model.Colors.MainTextColor);

                side.Item()
                    .PaddingHorizontal(PaddingHorizontal)
                    .Component(new SkillsSection(model));

                foreach (var section in model.Sections)
                {
                    side.Item()
                        .ShowEntire()
                        .PaddingHorizontal(PaddingHorizontal)
                        .Component(new SideSectionComponent(section));
                }
            });
    }
}
