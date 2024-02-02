using Flixen.CurriculumVitae.Contracts;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Flixen.CurriculumVitae.Layouts;

public class DefaultCvDocument(ResumeModel model) : IDocument
{
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.PageColor(model.Colors.Background);
            page.Size(PageSizes.A4);

            page.Content()
                .Row(row =>
                {
                    row.RelativeItem(2)
                        .Component(new SideBar(model));
            
                    row.RelativeItem(3)
                        .Component(new MainPage(model));
                });
        });
    }
}