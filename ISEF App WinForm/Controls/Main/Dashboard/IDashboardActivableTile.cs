namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using cvti.data;
    using cvti.data.Core;
    using System.Threading.Tasks;

    public interface IDashboardActivableTile
    {
        Task LoadData(ISEFDataManager manager);
    }
}