using GLib;
using Gtk;

namespace Porter
{
    public class Application : Gtk.Application
    {
        ApplicationWindow MainWindow;


        public Application(string application_id, ApplicationFlags flags) : base(application_id, flags)
        {
            MainWindow = new MainWindow(this);
            AddWindow(MainWindow);

            MainWindow.ShowAll();
        }
    }
}