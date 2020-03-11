using System;
using System.Collections.Generic;
using Gtk;

namespace Porter.Widgets
{
    public class Welcome : EventBox
    {

        List<Button> children = new List<Button>();

        protected Grid options;
        Label TitleLabel;
        Label SubtitleLabel;

        public string Title
        {
            get => TitleLabel.Text;
            set { TitleLabel.Text = value; }
        }

        public string Subtitle
        {
            get => SubtitleLabel.Text;
            set { SubtitleLabel.Text = value; }
        }

        public Welcome(string title, string subtitle)
        {
            TitleLabel = new Label(title);
            TitleLabel.Hexpand = true;
            TitleLabel.Justify = Justification.Center;
            TitleLabel.StyleContext.AddClass("h1");

            SubtitleLabel = new Label(subtitle);
            SubtitleLabel.Hexpand = true;
            SubtitleLabel.Wrap = true;
            SubtitleLabel.LineWrapMode = Pango.WrapMode.Word;
            SubtitleLabel.Justify = Justification.Center;
            SubtitleLabel.StyleContext.AddClass("h2");
            SubtitleLabel.StyleContext.AddClass("dim-label");

            options = new Grid();
            options.Orientation = Orientation.Vertical;
            options.RowSpacing = 12;
            options.Halign = Align.Center;
            options.MarginTop = 24;

            var content = new Grid();
            content.Expand = true;
            content.Margin = 12;
            content.Orientation = Orientation.Vertical;
            content.Valign = Align.Center;
            content.Add(TitleLabel);
            content.Add(SubtitleLabel);
            content.Add(options);

            Add(content);
        }

        public int Append(string iconName, string text, string description)
        {
            var image = new Gtk.Image(iconName, IconSize.Dialog);
            image.UseFallback = true;
            return AppendWithImage(image, text, description);
        }

        private int AppendWithImage(Image image, string text, string description)
        {
            var button = new WelcomeButton(image, text, description);
            children.Add(button);
            options.Add(button);

            button.Clicked += (object sender, EventArgs e) =>
            {
                int index = children.IndexOf(button);
                var toast = new Toast("Pew-pew");
                toast.Show();
            };
            return children.IndexOf(button);
        }
    }
}