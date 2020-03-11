using Gtk;

namespace Porter.Widgets
{
    public class WelcomeButton : Button
    {
        Label buttonTitle;
        Label buttonDescription;
        Grid buttonGrid;

        public WelcomeButton(Image image, string text, string description)
        {
            buttonTitle = new Label(text);
            buttonTitle.Halign = Align.Start;
            buttonTitle.Valign = Align.End;
            buttonTitle.StyleContext.AddClass("h3");

            buttonDescription = new Label(description);
            buttonDescription.Halign = Align.Start;
            buttonDescription.Valign = Align.Start;
            buttonDescription.LineWrap = true;
            buttonDescription.LineWrapMode = Pango.WrapMode.Word;
            buttonDescription.StyleContext.AddClass("dim-label");

            image.Halign = Align.Center;
            image.Valign = Align.Center;
            image.PixelSize = 48;

            // StyleContext.AddClass("flat");

            buttonGrid = new Grid();
            buttonGrid.ColumnSpacing = 12;
            buttonGrid.Attach(buttonTitle, 1, 0, 1, 1);
            buttonGrid.Attach(buttonDescription, 1, 1, 1, 1);
            buttonGrid.Attach(image, 0, 0, 1, 2);
            Add(buttonGrid);
        }
    }
}