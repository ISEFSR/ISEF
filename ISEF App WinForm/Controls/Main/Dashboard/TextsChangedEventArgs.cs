namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;

    public class TextsChangedEventArgs : EventArgs
    {
        public TextsChangedEventArgs(string shortText, string longText)
        {
            TextShort = shortText;
            TextLong = longText;
        }

        public string TextShort { get; private set; }
        public string TextLong { get; private set; }

        public bool SuccessfullyChanged { get; internal set; } = false;
    }
}
