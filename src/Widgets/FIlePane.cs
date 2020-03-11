using System;
using System.Collections.Generic;
using FluentFTP;
using Gtk;

namespace Porter.Widgets
{
    public class FilePane : Grid
    {
        public Uri CurrentURI;
        ListBox listBox;
        Stack stack;

        public FilePane()
        {
            var placeholderLabel = new Label("This Folder is Empty")
            {
                Halign = Align.Center,
                Valign = Align.Center,
            };
            placeholderLabel.Show();

            listBox = new ListBox()
            {
                Hexpand = true,
                Vexpand = true,
                Placeholder = placeholderLabel,
                SelectionMode = SelectionMode.Multiple,
            };

            var scrolledPane = new ScrolledWindow();
            scrolledPane.Add(listBox);

            var spinner = new Spinner()
            {
                Hexpand = true,
                Vexpand = true,
                Halign = Align.Center,
                Valign = Align.Center,
            };
            spinner.Start();

            stack = new Stack();
            stack.AddNamed(scrolledPane, "list");
            stack.AddNamed(spinner, "spinner");

            listBox.ListRowActivated += (object o, ListRowActivatedArgs args) =>
            {
                var uri = args.Row.Data["uri"];
                var type = args.Row.Data["type"];
            };

            Add(stack);
        }

        public void UpdateList(FtpListItem[] fileList)
        {
            foreach (var fileInfo in fileList)
            {
                if (fileInfo.Name.StartsWith(".")) continue;
                listBox.Add(NewRow(fileInfo));
            }
            listBox.ShowAll();
        }

        private ListBoxRow NewRow(FtpListItem fileInfo)
        {
            var checkbox = new CheckButton();

            var name = new Label(fileInfo.Name)
            {
                Halign = Align.Start,
                Hexpand = true,
            };

            var row = new Grid()
            {
                ColumnSpacing = 6,
                Hexpand = true,
                Margin = 6,
            };
            row.Add(checkbox);
            row.Add(name);

            var ebrow = new EventBox();
            ebrow.Add(row);

            var lbrow = new ListBoxRow();
            lbrow.Hexpand = true;
            lbrow.Add(ebrow);
            lbrow.Data.Add("uri", new Uri(fileInfo.FullName));
            lbrow.Data.Add("name", fileInfo.Name);
            //lbrow.Data.Add("type", fileInfo.)

            return lbrow;
        }

        public void StartSpinner()
        {
            stack.VisibleChildName = "spinner";
        }

        public void StopSpinner()
        {
            stack.VisibleChildName = "list";
        }
    }
}