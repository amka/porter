using System;
using Gtk;

namespace Porter
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Porter.Application("org.Porter.Porter", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            Application.Run();
        }
    }
}
