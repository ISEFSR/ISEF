namespace cvti.data.Core
{
    using cvti.data.Classifiers;
    using cvti.data.Conditions;
    using cvti.data.Functions;
    using cvti.data.Output;
    using cvti.data.Views;
    using cvti.data.Enums;
    using cvti.data.Input;
    using cvti.data.Tables;

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Datovy manager zodpovedny za operacie nad datami ulozenymi v MSSQL databaze
    /// </summary>
    public partial class MSSQLDataManager
    {
        #region Variables And Constructors

        private readonly LogManager _log;
        private readonly StatsManager _stats;

        /// <summary>
        /// Inicializuje novy MSSQL dta manager nza zaklade serveru a log manageru
        /// </summary>
        /// <param name="server">MSSQL server ako server a DB kde su ulozene data ako <see cref="MSSQLServer"/></param>
        /// <param name="log">Log manager ako <see cref="LogManager"/></param>
        public MSSQLDataManager(MSSQLServer server, LogManager log = null)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));

            _log = log;
            _stats = new StatsManager(server, _log);

            // Zodpovedny za ciselniky analytickej evidencie
            CiselnikyManager = new AnalytickaEvidenciaManager(Server, _log);

            // Zodpovedny za ciselnik organizacii
            OrganizacieManager = new OrganizacieManager(server, _log);

            // Zodpovedne za ciselnyki obci, okresov a krajov
            Obce = new ObceManager(server, _log);
            Okresy = new OkresyManager(server, _log);
            Kraje = new KrajeManager(server, _log);

            // Zodpovedny za ciselniky podriadenosti segmentov a stupnov
            Podriadenost = new PodriadenostManager(server, _log);
            Segmenty = new SegmentyManager(server, _log);
            Stupne = new StupneManager(server, _log);

            // Zodpovedne za ciselnik transferova uctov
            Transfery = new TransfersManager(server, _log);
            Ucty = new UctyManager(server, _log);
        }

        #endregion

        #region Public Properties

        public MSSQLServer Server { get; }

        public AnalytickaEvidenciaManager CiselnikyManager { get; }
        public OrganizacieManager OrganizacieManager { get; }

        public ObceManager Obce { get; }
        public OkresyManager Okresy { get; }
        public KrajeManager Kraje { get; }

        public PodriadenostManager Podriadenost { get; }
        public SegmentyManager Segmenty { get; }
        public StupneManager Stupne { get; }

        public TransfersManager Transfery { get; }
        public UctyManager Ucty { get; }

        #endregion

        #region Public Methods

        public async Task<IList<decimal>> VratSumuBezTransferov(Zostava z, int rok)
        {
            var transferCommand = new SelectCommand(string.Empty);

            var zostavaCommand = z.GetSelectCommand(rok);

            foreach (var c in z.Hlavicka.Data.Stlpce)
                if (c.ContainsAggregateFunction())
                    transferCommand.AddColumn(c);

            var transferovaPodmienka = Transfery.GetWithoutCondition(await Transfery.GetTransfers(rok));
            //transferovaPodmienka.Negate = true;

            transferCommand.CommandCondition = zostavaCommand.CommandCondition.CloneMe(true)
                .AddCondition(transferovaPodmienka, ConditionOperator.And);

            var transfery = new List<decimal>();
            using (var conn = Server.GetConnection(true))
            using (var cmd = transferCommand.GenerateCommand(conn))
            {
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    if (await reader.ReadAsync())
                        for (var i = 0; i < transferCommand.Columns.Count(); i++)
                        {
                            transfery.Add(reader.IsDBNull(i) ? 0 : (decimal)reader.GetDecimal(i));
                        }
            }
            return transfery;
        }

        public async Task<IList<decimal>> VratSumuTransfery(Zostava z, int rok)
        {
            var transferCommand = new SelectCommand(string.Empty);

            var zostavaCommand = z.GetSelectCommand(rok);

            foreach (var c in z.Hlavicka.Data.Stlpce)
                if (c.ContainsAggregateFunction())
                    transferCommand.AddColumn(c);

            transferCommand.CommandCondition = zostavaCommand.CommandCondition.CloneMe(true).AddCondition(
                Transfery.GetWithCondition(await Transfery.GetTransfers(rok)), ConditionOperator.And);


            var transfery = new List<decimal>();
            using (var conn = Server.GetConnection(true))
            using (var cmd = transferCommand.GenerateCommand(conn))
            {
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    if (await reader.ReadAsync())
                        for (var i = 0; i < transferCommand.Columns.Count(); i++)
                            transfery.Add((decimal)reader.GetDecimal(i));
            }
            return transfery;
        }

        public async Task<decimal> VratTransfer(TransferRiadok transfer)
        {
            var command = new SelectCommand(string.Empty);
            var sum = AssuView.VratStlpec(AssuViewAvailableColumns.Skut);
            sum.AddFunction(new Sum());

            command.AddColumn(AssuView.VratStlpec(AssuViewAvailableColumns.Rok));
            command.AddColumn(sum);
            command.CommandCondition = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), transfer.Polozka);
            command.CommandCondition.AddCondition(new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), transfer.FromStupen), ConditionOperator.And);

            using (var conn = Server.GetConnection(true))
            using (var cmd = command.GenerateCommand(conn))
            {
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    if (await reader.ReadAsync())
                        return (decimal)reader.GetDecimal(1);
            }

            return 0;
        }

        public async Task<IEnumerable<DatovyRiadok>> SelectData(SelectCommand command)
        {
            using (var conn = Server.GetConnection(true))
            using (var cmd = command.GenerateCommand(conn))
            {
                cmd.CommandTimeout = 320;
                var data = new List<DatovyRiadok>();
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        data.Add(new DatovyRiadok(reader, command.Columns));

                return data;
            }
        }

        public async Task<DataTable> ReadData(SelectCommand command)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = command.GenerateCommand(conn))
            {
                //await conn.OpenAsync();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var data = new DataTable();

                    //foreach (var c in command.Columns)
                    //{
                    //    data.Columns.Add(new DataColumn(c.ColumnAlias));
                    //}

                    adapter.Fill(data);
                    return data;
                }
            }
        }

        public async Task<StatsView> GetStatsAsync(int rok, InputType stupen) => await _stats.GetStats(stupen, rok);
        public async Task UpdateStatsAsync(int rok) => await _stats.UpdateStats(rok);
        public async Task<int> GetDataCountAsync(int rok, InputType type)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select count(rok) from {Server.DatabaseName}.{AssuView.ViewName} where [Rok]=@rok and [StupenKod]=@stupen", conn))
            {
                cmd.Parameters.AddWithValue("@rok", rok);
                cmd.Parameters.AddWithValue("@stupen", (char)type);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }
        public async Task<IEnumerable<AssuView>> GetDataAsync(int rok, InputType type, int from, int count)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($@"select * from {Server.DatabaseName}.{AssuView.ViewName} where [Rok]=@rok and [StupenKod]=@stupen order by [Rok]
                                                offset {from} rows
                                                FETCH NEXT {count} rows only", conn))
            {
                var data = new List<AssuView>();
                cmd.Parameters.AddWithValue("@rok", rok);
                cmd.Parameters.AddWithValue("@stupen", (char)type);
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        data.Add(new AssuView(reader));

                return data;
            }
        }

        public async Task<IEnumerable<int>> GetAvailableYearsAsync()
        {
            var years = new List<int>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select distinct [Rok] from [{Server.DatabaseName}].{StatsView.TableName}", conn))
            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    years.Add(reader.GetInt32(0));

            return years;
        }

        public async Task<IEnumerable<int>> GetAvailableYearsAsync(InputType type)
        {
            var years = new List<int>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select distinct [Rok] from {Server.DatabaseName}.{AssuView.ViewName} where [StupenKod]=@stupen order by [Rok]", conn))
            {
                cmd.Parameters.AddWithValue("@stupen", (char)type);
                using (var reader = cmd.ExecuteReader())
                {
                    cmd.Parameters.AddWithValue("@stupen", (char)type);
                    while (await reader.ReadAsync())
                        years.Add(reader.GetInt32(0));

                    return years;
                }
            }
        }

        public async Task<int> GetDataCountAsync(InputType type, int rok)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select count(*) from {Server.DatabaseName}.{AssuView.ViewName} where [StupenKod]=@stupen and [Rok]=@rok", conn))
            {
                cmd.Parameters.AddWithValue("@rok", rok);
                cmd.Parameters.AddWithValue("@stupen", (char)type);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task<bool> AreDataPresentAsync(InputType type, int year)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select top 1 1 from [{Server.DatabaseName}].{AssuView.ViewName} where [Rok]=@rok and [StupenKod]=@stupen", conn))
            {
                cmd.Parameters.AddWithValue("@rok", year);
                cmd.Parameters.AddWithValue("@stupen", (char)type);
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    return await reader.ReadAsync();
            }
        }

        public async Task<bool> AreDataPresentAsync(int year)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select top 1 1 from [{Server.DatabaseName}].{AssuView.ViewName} where [Rok]=@rok", conn))
            {
                cmd.Parameters.AddWithValue("@rok", year);
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult))
                    return await reader.ReadAsync();
            }
        }

        public async Task<int> RemoveDataAsync(InputType type, int rok)
        {
            var sourceTable = type == InputType.MaO ? "[dbo].[mao]" : "[dbo].[ostatne]";
            var condition = type == InputType.MaO ? string.Empty : " and [Stupen]=@stupen";

            using (var conn = await Server.GetConnectionAsync(true))
            using (var tran = conn.BeginTransaction())
            using (var cmd = new SqlCommand($"delete from [{Server.DatabaseName}].{sourceTable} where [Rok]=@rok {condition}", conn, tran))
            {
                if (type != InputType.MaO)
                {
                    cmd.Parameters.AddWithValue("@stupen", (char)type);
                }

                cmd.Parameters.AddWithValue("@rok", rok);
                var removed = await cmd.ExecuteNonQueryAsync();
                await _stats.UpdateStats(tran, rok);
                tran.Commit();
                if (_log != null)
                {
                    _log.LogNewMessageSafe(LogMessageType.BackEndMessage, "MSSQLDataManage.RemoveDataAsync", $"Successfully removed {removed} lines from {type} for {rok}.");
                }
                DateRemoved?.Invoke(this, new Tuple<InputType, int>(type, rok));
                return removed;
            }
        }

        public async Task<int> ImportOthersAsync(IList<OthersDataRow> data, int rok, OthersInputType type, int updateFromYear)
        {
            await ValidateClassifiersAsync(data, rok, updateFromYear);
            await ValidateUcty((from d in data select d.SyntAlFik).Distinct());
            return await ImportOthers(data, rok, type);
        }

        public async Task<int> ImportOthersAsync(IList<OthersDataRow> data, int rok, OthersInputType type)
        {
            await ValidateClassifiersAsync(data, rok);
            await ValidateUcty((from d in data select d.SyntAlFik).Distinct());
            return await ImportOthers(data, rok, type);
        }

        private async Task<int> ImportOthers(IList<OthersDataRow> data, int rok, OthersInputType type)
        {
            var stupen = (char)type;

            Array.ForEach(data.ToArray(), d => d.Stupen = ((char)type).ToString());

            const int Step = 1000;
            string InsertBase = $"insert into {Server.DatabaseName}.[dbo].[ostatne] (rok, stupen, ico, klient, druh_rozpisu, kapitola, ucet, fk, ek, zk, pk, druh_rozp, rozpp, rozpu, skut) values ";

            var totalcCount = 0;
            using (var conn = await Server.GetConnectionAsync(true))
            using (var tran = conn.BeginTransaction())
            using (var cmd = new SqlCommand(InsertBase, conn, tran))
            {
                for (var i = 0; i < data.Count; i += Step)
                {

                    cmd.CommandText += string.Join(",", data.Skip(i).Take(Step).Select(d => d.GetInsert(rok)));
                    var c = await cmd.ExecuteNonQueryAsync();
                    totalcCount += c;
                    OthersRowsImported?.Invoke(this, new Tuple<OthersInputType, int>(type, c));
                    cmd.CommandText = InsertBase;
                }
                tran.Commit();
                await _stats.UpdateStats(rok);
            }
            if (_log != null)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage, "MSSQLDataManage.ImportOthersAsync", $"Successfully imported {totalcCount} data for {type} and year {rok}.");
            }
            return totalcCount;
        }

        public async Task<int> ImportObceAsync(IList<ObceDataRow> data, int rok, int fromYear)
        {
            await ValidateClassifiersAsync(data, rok, fromYear);
            await ValidateUcty((from d in data select d.Su).Distinct());
            return await ImportObce(data, rok);
        }

        public async Task<int> ImportObceAsync(IList<ObceDataRow> data, int rok)
        {
            await ValidateClassifiersAsync(data, rok);
            await ValidateUcty((from d in data select d.Su).Distinct());
            return await ImportObce(data, rok);
        }

        private async Task<int> ImportObce(IList<ObceDataRow> data, int rok)
        {
            //if (AreDataPresentObce(rok)) throw new DataAlreadyExistsException();
            const int Step = 1000; // mssql max
            string InsertBase = $"insert into {Server.DatabaseName}.[dbo].[mao] (Rok, Ico, NadriadeneIco, Okres, Obec, Segm, Ucet, Cast, Fk, Ek, Zk, Pk, Skut, Rozpp, Rozpu, Sknace, Druh_rozp, Remove) values ";


            var totalCount = 0;
            using (var conn = await Server.GetConnectionAsync(true))
            using (var tran = conn.BeginTransaction())
            using (var cmd = new SqlCommand(InsertBase, conn, tran))
            {

                for (var i = 0; i < data.Count; i += Step)
                {
                    cmd.CommandText += string.Join(",", data.Skip(i).Take(Step).Select(d => d.GetInsert(rok)));
                    var c = await cmd.ExecuteNonQueryAsync();
                    ObceRowsImported?.Invoke(this, c);
                    cmd.CommandText = InsertBase;
                    totalCount += c;
                }

                tran.Commit();
                await _stats.UpdateStats(rok);
            }
            if (_log != null)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage, "MSSQLDataManage.ImportObceAsync", $"Successfully imported {totalCount} data for MaO and year {rok}.");
            }
            return totalCount;
        }

        public async Task<DataTable> VratObceAsync(int rok)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand("select * from [isef].[dbo].[mao] where [rok]=@rok", conn))
            {
                cmd.Parameters.AddWithValue("@rok", rok);
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }

        public async Task<IEnumerable<StatsView>> GetStatsAsync()
        {
            return await _stats.GetStats();
        }

        private async Task ValidateClassifiersAsync(IEnumerable<IInputDataRow> data, int rok)
        {
            // TODO: Organizacie su prednahrame, ale aj tak ich treba zvalidovat 
            // ci nahodou nepribudli nove organizacie ak ano tak ich je treba pridat 
            //CiselnikyManager.ValidateOrg(data.Select(d => d.Ico).Distinct(), rok, stupen);

            //await Task.WhenAll(
            //    OrganizacieManager.ValidateOrganizationsAsync(data.Select(d => d.Ico).Distinct()),
            //    CiselnikyManager.ValidateFkAsync(data.Select(d => d.Fk).Distinct(), rok),
            //    CiselnikyManager.ValidateEkAsync(data.Select(d => d.Ek).Distinct(), rok),
            //    CiselnikyManager.ValidateZkAsync(data.Select(d => d.Zk).Distinct(), rok),
            //    CiselnikyManager.ValidatePkAsync(data.Select(d => d.Pk).Distinct(), rok));
            await OrganizacieManager.ValidateOrganizationsAsync(data.Select(d => d.Ico).Distinct());
            await CiselnikyManager.ValidateFkAsync(data.Select(d => d.Fk).Distinct(), rok, true);
            await CiselnikyManager.ValidateEkAsync(data.Select(d => d.Ek).Distinct(), rok, true);
            await CiselnikyManager.ValidateZkAsync(data.Select(d => d.Zk).Distinct(), rok, true);
            await CiselnikyManager.ValidatePkAsync(data.Select(d => d.Pk).Distinct(), rok, true);
        }

        public async Task ValidateUcty(IEnumerable<string> ucty)
        {
            await Ucty.ValidateUcty(ucty);
        }

        public async Task ValidateClassifiersAsync(IEnumerable<IInputDataRow> data, int rok, int updateFrom)
        {
            await OrganizacieManager.ValidateOrganizationsAsync(data.Select(d => d.Ico).Distinct());
            await CiselnikyManager.ValidateFkAsync(data.Select(d => d.Fk).Distinct(), rok);
            await CiselnikyManager.UpdateClassifierFromYearAsync(Enums.AnalytickaEvidencia.FunkcnaKlasifikacia, rok, updateFrom);

            await CiselnikyManager.ValidateEkAsync(data.Select(d => d.Ek).Distinct(), rok);
            await CiselnikyManager.UpdateClassifierFromYearAsync(Enums.AnalytickaEvidencia.EkonomickaKlasifikacia, rok, updateFrom);

            await CiselnikyManager.ValidateZkAsync(data.Select(d => d.Zk).Distinct(), rok);
            await CiselnikyManager.UpdateClassifierFromYearAsync(Enums.AnalytickaEvidencia.ZdrojovaKlasifikacia, rok, updateFrom);

            await CiselnikyManager.ValidatePkAsync(data.Select(d => d.Pk).Distinct(), rok);
            await CiselnikyManager.UpdateClassifierFromYearAsync(Enums.AnalytickaEvidencia.ProgramovaKlasifikacia, rok, updateFrom);
        }

        #endregion

        public event EventHandler<int> ObceRowsImported;

        public event EventHandler<Tuple<OthersInputType, int>> OthersRowsImported;

        public event EventHandler<Tuple<InputType, int>> DateRemoved;
    }
}
