namespace cvti.isef.winformapp.Controls.Main.Import
{
    using System;
    using System.Collections.Generic;

    public interface IImportStep
    {
        event EventHandler MoveNext;
        event EventHandler MovePrev;

        bool IsValid();

        IEnumerable<HelpTileInfo> StepHelp { get; }
    }
}