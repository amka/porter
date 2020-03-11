using Gtk;

namespace Porter.Widgets
{
    public class Header : HeaderBar
    {
        public Button AddButton;

        public Header()
        {
            Title = "Porter";
            ShowCloseButton = true;
            HasSubtitle = false;

            AddButton = new Button("list-add", IconSize.SmallToolbar);
            PackStart(AddButton);
        }
    }
}