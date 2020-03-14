using System;
using FluentFTP;
using Gtk;
using Granite.Widgets;
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

            FilePane filePane = new FilePane();

            stack = new Stack();
            stack.AddNamed(welcome, "welcome");
            stack.AddNamed(filePane, "remote");

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
            cnxPopover.connectButton.Sensitive = false;
            var host = cnxPopover.hostEntry.Text;
            ftpClient = new FtpClient(host);

            try
            {
                var profile = await ftpClient.AutoConnectAsync();
                if (ftpClient.IsConnected)
                {
                    cnxPopover.Popdown();
                    Console.WriteLine($"Connected: {ftpClient.IsConnected} to {ftpClient.ServerOS}{ftpClient.ServerType}");

                    var remotePane = stack.GetChildByName("remote") as FilePane;
                    remotePane.StartSpinner();
                    stack.VisibleChildName = "remote";

                    var listing = await ftpClient.GetListingAsync();

                    remotePane.UpdateList(listing);

                    remotePane.StopSpinner();
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
