using System;
using FluentFTP;
using Gtk;
using Porter.Widgets;

namespace Porter
{
    class MainWindow : ApplicationWindow
    {
        Gtk.Application application;
        Stack stack;
        ConnectionPopover cnxPopover;
        FtpClient ftpClient;

        public MainWindow(Gtk.Application application) : base(application)
        {
            this.application = application;

            DefaultSize = new Gdk.Size(800, 600);
            var header = new Header();
            Titlebar = header;

            cnxPopover = new ConnectionPopover(header.AddButton);
            cnxPopover.connectButton.Clicked += OnConnectPopoverClicked;
            header.AddButton.Clicked += ShowPopover;

            DeleteEvent += Window_DeleteEvent;

            var welcome = new Welcome("Connect", "Type a URL and press 'Enter' to\nconnect to a server.");
            welcome.Vexpand = true;
            // welcome.Append("list-add", "Show toast", "");

            stack = new Stack();
            stack.AddTitled(welcome, "welcome", "Welcome");

            Add(stack);
        }

        void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Gtk.Application.Quit();
        }


        void ShowPopover(object sender, EventArgs e)
        {
            cnxPopover.ShowAll();
            cnxPopover.Popup();
        }

        async void OnConnectPopoverClicked(object sender, EventArgs e)
        {
            var host = cnxPopover.hostEntry.Text;
            ftpClient = new FtpClient(host);

            try
            {
                var profile = await ftpClient.AutoConnectAsync();
                if (ftpClient.IsConnected)
                {
                    Console.WriteLine($"Connected: {ftpClient.IsConnected} to {ftpClient.ServerOS}{ftpClient.ServerType}");

                }
            }
            catch (System.Exception err)
            {
                Console.WriteLine(err.Message);
                await ftpClient.DisconnectAsync();
            }
            finally
            {
                cnxPopover.connectButton.Sensitive = true;
            }
        }
    }
}
