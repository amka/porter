using GLib;
using Gtk;

namespace Porter.Widgets
{
    public class ConnectionPopover : Popover
    {
        public Entry hostEntry;
        public Button connectButton;

        public ConnectionPopover(Widget relative_to) : base(relative_to)
        {

            var box = new Box(Orientation.Horizontal, 4)
            {
                Padding = 16,
                Margin = 8,
            };

            hostEntry = new Entry("FTP host");
            connectButton = new Button("object-select-symbolic", IconSize.Button)
            {
                Label = "Connect",
            };

            box.PackStart(hostEntry, true, true, 0);
            box.PackStart(connectButton, false, true, 0);
            Add(box);
        }
    }
}