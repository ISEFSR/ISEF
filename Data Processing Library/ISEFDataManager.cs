namespace cvti.data
{
    using cvti.data.Core;
    using cvti.data.Files;
    using cvti.data.Output;
    using cvti.data.Enums;
    using cvti.data.Input;
    using cvti.data.Tables;
    using cvti.data.Views;

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using cvti.data.Conditions;
    using cvti.data.Classifiers;
    using System.Drawing;

    /// <summary>
    /// Data manager zodpovedny za manipulaciu so subormi, databazovimi tabulkami import a export udajov
    /// </summary>
    public class ISEFDataManager
    {
        #region Events
        public event EventHandler<Zostava> ZostavaExportovana;

        public event EventHandler SuccessfullyInitialized;

        public event EventHandler SuccessfullySaved;
        #endregion

        #region Constructor, Initialization and State Properties
        private readonly List<StupenRiadok> _stupne = new List<StupenRiadok>();
        private readonly List<SegmentRiadok> _segmenty = new List<SegmentRiadok>();
        private readonly List<UcetRiadok> _ucty = new List<UcetRiadok>();
        private readonly List<KrajRiadok> _kraje = new List<KrajRiadok>();
        private readonly List<OkresRiadok> _okresy = new List<OkresRiadok>();
        private readonly List<ObecRiadok> _obce = new List<ObecRiadok>();
        private readonly List<PodriadenostRiadok> _pod = new List<PodriadenostRiadok>();
        private readonly List<OrganizaciaRiadok> _organizacie = new List<OrganizaciaRiadok>();

        public ISEFDataManager(MSSQLServer server, string dataDirectory, string hlavickyDirectory, string outputDirectory, string backupDirectory, Image logo)
        {         
            Server = server;
            Log = new LogManager(server);
            CoreFiles = new FilesManager(Log, dataDirectory, hlavickyDirectory, outputDirectory, backupDirectory);
            InputObce = new ObceDataManager(Log);
            InputOthers = new OthersDataManager(Log);
            Export = new ExportManager(server, Log);
            MSSQLManager = new MSSQLDataManager(server, Log);
            Output = new OutputManager(Log, Export, MSSQLManager, outputDirectory, logo);

            MSSQLManager.Obce.ObecPridana += (snd, ea) => { _obce.Add(ea); };
            MSSQLManager.Obce.ObecOdstranena += (snd, ea) => { _obce.Remove(ea); };

            MSSQLManager.Okresy.OkresOdstraneny += (snd, ea) => { _okresy.Remove(ea); };
            MSSQLManager.Okresy.OkresPridany += (snd, ea) => { _okresy.Add(ea);  };

            MSSQLManager.Podriadenost.PodriadenostPridana += (snd, ea) => { _pod.Add(ea); };
            MSSQLManager.Podriadenost.PodriadenostOdstranena += (snd, ea) => { _pod.Remove(ea); };

            MSSQLManager.Stupne.StupenOdstraneny += (snd, ea) => { _stupne.Remove(ea); };
            MSSQLManager.Stupne.StupenVytvoreny += (snd, ea) => { _stupne.Add(ea); };

            MSSQLManager.Segmenty.SegmentPridany += (snd, ea) => { _segmenty.Add(ea); };
            MSSQLManager.Segmenty.SegmentOdstraneny += (snd, ea) => { _segmenty.Remove(ea); };

            MSSQLManager.Kraje.KrajAdded += (snd, ea) => { _kraje.Add(ea); };
            MSSQLManager.Kraje.KrajRemoved += (snd, ea) => { _kraje.Remove(ea); };
        }
        public async Task<bool> InitializeManager()
        {
            if (DataLoaded)
                return false;

            _stupne.AddRange(await MSSQLManager.Stupne.VratStupne());
            _segmenty.AddRange(await MSSQLManager.Segmenty.VratSegmenty());
            _ucty.AddRange(await MSSQLManager.Ucty.VratUcty());
            _kraje.AddRange(await MSSQLManager.Kraje.VratKraje());
            _okresy.AddRange(await MSSQLManager.Okresy.VratOkresy());
            _obce.AddRange(await MSSQLManager.Obce.VratObce());
            _pod.AddRange(await MSSQLManager.Podriadenost.VratPodriadenost());
            _organizacie.AddRange(await MSSQLManager.OrganizacieManager.VratOrganizacie());

            CoreFiles.Initialize();

            DataLoaded = true;

            SuccessfullyInitialized?.Invoke(this, EventArgs.Empty);

            return true;
        }
        public async Task SaveManager()
        {
            CoreFiles.Save();

            await Task.Delay(1);

            SuccessfullySaved?.Invoke(this, EventArgs.Empty);
        }

        public bool DataLoaded { get; private set; }

        public StupenRiadok VratStupen(string kod) => (from s in _stupne where s.Kod == kod select s).FirstOrDefault();
        public SegmentRiadok VratSegment(string kod) => (from s in _segmenty where s.Kod == kod select s).FirstOrDefault();
        public UcetRiadok Vratucet(string kod) => (from s in _ucty where s.Kod == kod select s).FirstOrDefault();
        public KrajRiadok VrakKraj(short kod) => (from s in _kraje where s.Kod == kod select s).FirstOrDefault();
        public OkresRiadok VratOkres(short kod) => (from s in _okresy where s.KodOkres == kod select s).FirstOrDefault();
        public ObecRiadok VratObec(int kod) => (from s in _obce where s.Kod == kod select s).FirstOrDefault();
        public PodriadenostRiadok VratPodriadenost(string kod) => (from s in _pod where s.Kod == kod select s).FirstOrDefault();
        public OrganizaciaRiadok VratOrganizaciu(string kod) => (from s in _organizacie where s.Ico == kod select s).FirstOrDefault();

        public IEnumerable<StupenRiadok> Stupne { get => _stupne; }
        public IEnumerable<SegmentRiadok> Segmenty { get => _segmenty; }
        public IEnumerable<UcetRiadok> Ucty { get => _ucty; }
        public IEnumerable<KrajRiadok> Kraje { get => _kraje; }
        public IEnumerable<OkresRiadok> Okresy { get => _okresy; }
        public IEnumerable<ObecRiadok> Obce { get => _obce; }
        public IEnumerable<PodriadenostRiadok> Podriadenost { get => _pod; }
        public IEnumerable<OrganizaciaRiadok> Organizacie { get => _organizacie; }
        #endregion

        #region Public Properties

        public MSSQLServer Server { get; }
        public OutputManager Output { get; }
        public ObceDataManager InputObce { get; }
        public OthersDataManager InputOthers { get; }
        public MSSQLDataManager MSSQLManager { get; }
        public ExportManager Export { get; }
        public LogManager Log { get; }
        public FilesManager CoreFiles { get; }

        #endregion

        #region Files
        public IEnumerable<Condition> GetConditions() 
            => CoreFiles.Conditions.Values;
        public IEnumerable<SelectCommand> GetCommands() 
            => CoreFiles.Commands.Values;
        public IEnumerable<Zostava> GetZostavy() 
            => CoreFiles.Zostavy.Values;
        public IEnumerable<Hlavicka> GetHlavicky() 
            => CoreFiles.Hlavicky.Values;
        #endregion

        #region Log
        public async Task<IEnumerable<LogMessage>> GetLogMessagesAsync(int last)
        {
            return await Log.GetLogMessages(last);
        }
        public async Task LogFrontendErrorSafeAsync(Exception ex, string from)
        { 
            await Log.LogNewMessageSafeAsync(LogMessageType.FrontEndError, from, ex.Message + "|" + ex.StackTrace);
        }
        public async Task LogFrontendMessageSafeAsync(string message, string from)
        {
            await Log.LogNewMessageSafeAsync(LogMessageType.FrontEndMessage, from, message);
        }
        #endregion

        #region Stats
        public async Task<StatsView> GetStats(int rok, InputType stupen)
            => await MSSQLManager.GetStatsAsync(rok, stupen);
        public async Task ReloadStatsForYear(int rok)
            => await MSSQLManager.UpdateStatsAsync(rok);
        public async Task ReloadAllStats()
        {
            foreach (var y in await MSSQLManager.GetAvailableYearsAsync())
                await ReloadStatsForYear(y);
        }
        #endregion

        #region Others
        public async Task<IEnumerable<ChybajuciRiadok>> GetMissingRows()
            => await MSSQLManager.CiselnikyManager.GetMissingRows();
        public async Task<bool> UpdateMissingRow(ChybajuciRiadok row, string shortText, string longText)
            => await MSSQLManager.CiselnikyManager.UpdateMissingRow(row, shortText, longText);

        #endregion
    }
}