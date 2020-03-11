using System;
using Gtk;

namespace Porter.Widgets
{
    public class Toast : Revealer
    {
        Label notificationLabel;
        Button defaultActionButton;

        public Toast(string title)
        {
            Halign = Align.Center;
            Valign = Align.Start;
            Margin = 3;

            defaultActionButton = new Button();
            defaultActionButton.Visible = false;
            defaultActionButton.NoShowAll = true;
            defaultActionButton.Clicked += (object sender, EventArgs e) =>
            {

            };
            notificationLabel = new Label(title);
            var nBox = new Grid();
            nBox.Add(notificationLabel);
            var nFrame = new Frame();
            nFrame.StyleContext.AddClass("app-notification");
            nFrame.Add(nBox);
            Add(nFrame);
        }
    }
}