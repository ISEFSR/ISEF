using cvti.data.Columns;
using cvti.data.Core;
using cvti.data.Enums;
using cvti.data.Tables;
using System;
using System.Threading.Tasks;

namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    public interface ICiselnikControl
    {
        Task NacitajDataAsync(data.ISEFDataManager manager, int year);

        void Deaktivuj();

        string TitleText { get; }
        string InfoText { get; }
        int SelectedYear { get; }

        Task ExportData(string path);

        bool CanGenerate();
        Task GenerateData();

        bool CanUpdate();
        Task UpdateData();

        bool CanRemove();
        Task RemoveData();

        bool CanCreate();
        Task CreateItem();

        Task ReloadData();

        Task ShowPreview();

        event System.EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        string GetInfoText();
        string GetMoreInfo();
        void Import();
    }
}
