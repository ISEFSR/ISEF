namespace cvti.data.Classifiers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Threading.Tasks;

    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;

    /// <summary>
    /// Manager zodpovedny za manipulaciu s ciselnikmi analytickej evidencie
    /// Pod tieto ciselniky spada Ekonomicka, funckna, zdrojova a programova klasifikacia
    /// Ciselniky maju data viazane na rok
    /// </summary>
    public class AnalytickaEvidenciaManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Indikuje pridany riadok pre ciselnik programovej klasifikacie ako <see cref="AnalytickaEvidenciaNovaHodnota"/>
        /// </summary>
        public event EventHandler<AnalytickaEvidenciaNovaHodnota> ProgramAdded;

        /// <summary>
        /// Indikuje pridany riadok pre ciselnik funkcnej kl;asifikacie ako <see cref="AnalytickaEvidenciaNovaHodnota"/>
        /// </summary>
        public event EventHandler<AnalytickaEvidenciaNovaHodnota> FunkcnaAdded;

        /// <summary>
        /// Indikuje pridany riadok pre ciselnik zdrojovej klasifikacie ako <see cref="AnalytickaEvidenciaNovaHodnota"/>
        /// </summary>
        public event EventHandler<AnalytickaEvidenciaNovaHodnota> ZdrojAdded;

        /// <summary>
        /// Indikuje pridany riadok pre ciselnik ekonomickej klasifikacie ako <see cref="AnalytickaEvidenciaNovaHodnota"/>
        /// </summary>
        public event EventHandler<AnalytickaEvidenciaNovaHodnota> EkonomickaAdded;

        /// <summary>
        /// Indikuje updatovany riadok pre ciselnik ako <see cref="AnalytickaEvidenciaNovaHodnota"/>
        /// </summary>
        public event EventHandler<AnalytickaEvidenciaRiadok> RiadokUpdated;

        #endregion

        #region Variables, Constants And Constructors

        public const string DefaultNazov = "???";
        public const string DefaultPopis = "??? ???";

        private readonly LogManager _log;

        public AnalytickaEvidenciaManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Vrati vsetky riadky, ktore maju nevyplnenu textaciu
        /// </summary>
        /// <returns>Riadk s nevyplnenou textaciou ako <see cref="IEnumerable{T}"/> kde T je <see cref="ChybajuciRiadok"/></returns>
        public async Task<IEnumerable<ChybajuciRiadok>> GetMissingRows()
        {
            var tables = new string[]
            {
                EkonomickaRiadok1.TableName,
                EkonomickaRiadok2.TableName,
                EkonomickaRiadok3.TableName,
                EkonomickaRiadok6.TableName,
                FunkcnaRiadok2.TableName,
                FunkcnaRiadok3.TableName,
                FunkcnaRiadok4.TableName,
                FunkcnaRiadok5.TableName,
                ProgramRiadok3.TableName,
                ProgramRiadok5.TableName,
                ProgramRiadok7.TableName,
                ZdrojRiadok1.TableName,
                ZdrojRiadok2.TableName,
                ZdrojRiadok3.TableName,
                ZdrojRiadok4.TableName
            };

            
            var _missing = new List<ChybajuciRiadok>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand(string.Empty, conn))
            {
                cmd.Parameters.AddWithValue("@nazov", DefaultNazov);
                foreach (var t in tables)
                {
                    cmd.CommandText = $"select Rok, Kod from {Server.DatabaseName}.{t} where nazov=@nazov";
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            _missing.Add(new ChybajuciRiadok(reader.GetInt32(0), reader.GetString(1), t));
                }

                return _missing;
            }
        }

        public async Task<bool> UpdateMissingRow(ChybajuciRiadok row, string shortText, string longText)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand(string.Empty, conn))
            {
                cmd.CommandText = $"update {Server.DatabaseName}.{row.TableName} set nazov=@short, popis=@long where kod=@kod and rok=@rok";
                cmd.Parameters.AddWithValue("@short", shortText);
                cmd.Parameters.AddWithValue("@long", longText);
                cmd.Parameters.AddWithValue("@kod", row.Kod);
                cmd.Parameters.AddWithValue("@rok", row.Rok);

                return 1 == await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Vrati roky pre ktore su udaje ciselniku analytickej evidencie nahrane 
        /// </summary>
        /// <param name="ae">Ciselnik analytickej evidencie ako <see cref="AnalytickaEvidencia"/></param>
        /// <returns>Roky pre ktore su udaje cislenika nahrane ako <see cref="IEnumerable{T}"/> kde T je <see cref="int"/></returns>
        public async Task<IEnumerable<int>> VratDostupneRokyPreCiselnik(AnalytickaEvidencia ae)
        {
            var table = string.Empty;
            switch (ae)
            {
                case AnalytickaEvidencia.FunkcnaKlasifikacia:
                    table = "dbo.cis_fk2";
                    break;
                case AnalytickaEvidencia.EkonomickaKlasifikacia:
                    table = "dbo.cis_ek3";
                    break;
                case AnalytickaEvidencia.ZdrojovaKlasifikacia:
                    table = "dbo.cis_zk1";
                    break;
                case AnalytickaEvidencia.ProgramovaKlasifikacia:
                    table = "dbo.cis_pk3";
                    break;
                default:
                    break;
            }
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"select Rok FRom {Server.DatabaseName}.{table}", conn))
            {
                var roky = new List<int>();
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                        roky.Add(reader.GetInt32(0));

                return roky;
            }
        }

        /// <summary>
        /// Vygeneruje ciselnik na zaklade nahranych udajov z ineho roku
        /// </summary>
        /// <param name="ae">Ciselnik ako <see cref="AnalytickaEvidencia"/></param>
        /// <param name="fromYear">Rok z ktoreho generujem ciselnik</param>
        /// <param name="toYear">Rok do ktoreho generujem ciselnik</param>
        public async Task GenerujCiselnik(AnalytickaEvidencia ae, int fromYear, int toYear)
        {
            // TODO: kontrola existencie
            var procedure = string.Empty;
            switch (ae)
            {
                case AnalytickaEvidencia.FunkcnaKlasifikacia:
                    procedure = "dbo.usp_GenerateFunkcna";
                    break;
                case AnalytickaEvidencia.EkonomickaKlasifikacia:
                    procedure = "dbo.usp_GenerateEkonomicka";
                    break;
                case AnalytickaEvidencia.ZdrojovaKlasifikacia:
                    procedure = "dbo.usp_GenerateZdroj";
                    break;
                case AnalytickaEvidencia.ProgramovaKlasifikacia:
                    procedure = "dbo.usp_GenerateProgram";
                    break;
                default:
                    break;
            }


            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"{Server.DatabaseName}.{procedure}", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@fromRok", fromYear);
                cmd.Parameters.AddWithValue("@toRok", toYear);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Vrati data pre ciselnik pre vybrany rok
        /// </summary>
        /// <typeparam name="T">Ciselnik pre ktory vraciama data ako trieda typu <see cref="AnalytickaEvidenciaRiadok"/></typeparam>
        /// <param name="rok">Rok pre ktory vraciam udaje ciselniku</param>
        /// <returns>Udaje pre ciselnik ako <see cref="IEnumerable{T}"/></returns>
        /// <remarks>
        /// Predpoklad je ze classa implementujuca ICiselnikRiadok obsahuje staticku metodu GetSelectCommand
        /// </remarks>
        public async Task<IEnumerable<T>> VratCiselnikPreRokAsync<T>(int rok) where T : AnalytickaEvidenciaRiadok
        {
            try
            {
                var data = new List<T>();
                string methodName = "GetSelectCommand";
                Type type = typeof(T);
                MethodInfo info = type.GetMethod(
                    methodName,
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.FlattenHierarchy);

                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(IDataRecord) });

                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = (SqlCommand)info.Invoke(null, new object[] { conn, Server.DatabaseName, rok }))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                        {
                            data.Add((T)ctor.Invoke(new object[] { reader }));
                        }
                }
                return data;
            }
            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "AnalytickaEvidenciaManager.VratCiselnikPreRokAsync", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Updatuje udaje pre ciselnik a rok z ineho roku
        /// </summary>
        /// <param name="ae">Ciselnik pre ktory updatujem udaje ako <see cref="AnalytickaEvidencia"/></param>
        /// <param name="year">Rok pre ktroy updatujem udaje ako <see cref="int"/></param>
        /// <param name="fromYear">Rok z ktoreho beriem data ako <see cref="int"/></param>
        public async Task UpdateClassifierFromYearAsync(AnalytickaEvidencia ae, int year, int fromYear)
        {
            var uspName = string.Empty; 
            switch (ae)
            {
                case AnalytickaEvidencia.FunkcnaKlasifikacia:
                    uspName = "dbo.usp_UpdateFunkcnaFromYear";
                    break;
                case AnalytickaEvidencia.EkonomickaKlasifikacia:
                    uspName = "dbo.usp_UpdateEkonomickaFromYear";
                    break;
                case AnalytickaEvidencia.ZdrojovaKlasifikacia:
                    uspName = "dbo.usp_UpdateZdrojFromYear";
                    break;
                case AnalytickaEvidencia.ProgramovaKlasifikacia:
                    uspName = "dbo.usp_UpdateProgramFromYear";
                    break;
                default:
                    break;
            }

            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"{uspName}", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@toYear", year);
                cmd.Parameters.AddWithValue("@fromYear", fromYear);
                await cmd.ExecuteNonQueryAsync();

                if (_log != null)
                {
                    await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage,
                        "CiselnikyManager.UpdateClassifierFromYearAsync", $"{ae} for year {year} updated from year {fromYear}");
                }
            }
        }

        /// <summary>
        /// Updatuje vybrany ciselnik pre vybrany rok z defaultnyhc dat 
        /// </summary>
        /// <param name="ae">Vybrany ciselnik analytickej evidencie ako <see cref="AnalytickaEvidencia"/></param>
        /// <param name="rok">Rok pre ktory updatujem udaje ako <see cref="int"/></param>
        public async Task UpdateClassifierFromDefaultAsync(AnalytickaEvidencia ae, int rok)
        {
            var uspName = string.Empty;
            switch (ae)
            {
                case AnalytickaEvidencia.FunkcnaKlasifikacia:
                    uspName = "dbo.usp_UpdateFunkcna";
                    break;
                case AnalytickaEvidencia.EkonomickaKlasifikacia:
                    uspName = "dbo.usp_UpdateEkonomicka";
                    break;
                case AnalytickaEvidencia.ZdrojovaKlasifikacia:
                    uspName = "dbo.usp_UpdateZdroj";
                    break;
                case AnalytickaEvidencia.ProgramovaKlasifikacia:
                    uspName = "dbo.usp_UpdateProgram";
                    break;
                default:
                    break;
            }

            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"exec {uspName} @rok", conn))
            {
                cmd.Parameters.AddWithValue("@rok", rok);
                await cmd.ExecuteNonQueryAsync();

                if (_log != null)
                {
                    await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage, "CiselnikyManager.UpdateClassifierFromDefaultAsync", $"Update {ae} method used for year: {rok}");
                }
            }
        }

        /// <summary>
        /// Updatuje riadok ciselniku
        /// </summary>
        /// <param name="riadok">Riadok ciselniku ako <see cref="AnalytickaEvidenciaRiadok"/></param>
        /// <returns>True ak sa podari updatovat inak false</returns>
        public async Task<bool> UpdateCiselnikRiadokAsync(AnalytickaEvidenciaRiadok riadok) 
        {
            if (await Update(riadok, _log, "AnalytickaEvidenciaManager.UpdateCiselnikRiadokAsync"))
            {
                RiadokUpdated?.Invoke(this, riadok);
                return true;
            }
            return false;
        }

        #endregion

        #region Private And Internal Methods

        private async Task UpdateValue(SqlConnection conn, AnalytickaEvidenciaNovaHodnota hodnota, string table)
        {
            using (var cmd = new SqlCommand($"update [{Server.DatabaseName}].{table} set [Nazov]=@nazov, [Popis]=@popis where [ROk]=@rok and [Kod]=@kod", conn))
            {
                cmd.Parameters.AddWithValue("@nazov", hodnota.Skrateny);
                cmd.Parameters.AddWithValue("@popis", hodnota.Popis);
                cmd.Parameters.AddWithValue("@rok", hodnota.Rok);
                cmd.Parameters.AddWithValue("@kod", hodnota.Kod);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        internal async Task ValidateFkAsync(IEnumerable<string> fk, int year, bool updateFromDefault = false) 
        {
            using (var conn = await Server.GetConnectionAsync(true))
            {
                await CheckRoot(conn, FunkcnaRiadok2.TableName, year, fk, 2, FunkcnaAdded);
                await CheckValue(conn, FunkcnaRiadok3.TableName, year, fk, 3, 2, FunkcnaAdded);
                await CheckValue(conn, FunkcnaRiadok4.TableName, year, fk, 4, 3, FunkcnaAdded);
                await CheckValue(conn, FunkcnaRiadok5.TableName, year, fk, 5, 4, FunkcnaAdded);
            }

            if (updateFromDefault) await UpdateClassifierFromDefaultAsync(AnalytickaEvidencia.FunkcnaKlasifikacia,  year);
        }
        internal async Task ValidateEkAsync(IEnumerable<string> ek, int year, bool updateFromDefault = false)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            {
                await CheckRoot(conn, EkonomickaRiadok1.TableName, year, ek, 1, EkonomickaAdded);
                await CheckValue(conn, EkonomickaRiadok2.TableName, year, ek, 2, 1, EkonomickaAdded);
                await CheckValue(conn, EkonomickaRiadok3.TableName, year, ek, 3, 2, EkonomickaAdded);
                await CheckValue(conn, EkonomickaRiadok6.TableName, year, ek, 6, 3, EkonomickaAdded);
            }

            if (updateFromDefault) await UpdateClassifierFromDefaultAsync(AnalytickaEvidencia.EkonomickaKlasifikacia, year);
        }
        internal async Task ValidateZkAsync(IEnumerable<string> zk, int year, bool updateFromDefault = false) 
        {
            using (var conn = await Server.GetConnectionAsync(true))
            {
                await CheckRoot(conn, ZdrojRiadok1.TableName, year, zk, 1, ZdrojAdded);
                await CheckValue(conn, ZdrojRiadok2.TableName, year, zk, 2, 1, ZdrojAdded);
                await CheckValue(conn, ZdrojRiadok3.TableName, year, zk, 3, 2, ZdrojAdded);
                await CheckValue(conn, ZdrojRiadok4.TableName, year, zk, 4, 3, ZdrojAdded);
            }

            if (updateFromDefault) await UpdateClassifierFromDefaultAsync(AnalytickaEvidencia.ZdrojovaKlasifikacia, year);
        }
        internal async Task ValidatePkAsync(IEnumerable<string> pk, int year, bool updateFromDefault = false) 
        {
            using (var conn = await Server.GetConnectionAsync(true))
            {
                await CheckRoot(conn, ProgramRiadok3.TableName, year, pk, 3, ProgramAdded);
                await CheckValue(conn, ProgramRiadok5.TableName, year, pk, 5, 3, ProgramAdded);
                await CheckValue(conn, ProgramRiadok7.TableName, year, pk, 7, 5, ProgramAdded);
            }

            if (updateFromDefault) await UpdateClassifierFromDefaultAsync(AnalytickaEvidencia.ProgramovaKlasifikacia, year);
        }

        private async Task CheckValue(SqlConnection conn, string table, int year, IEnumerable<string> values, int valueLength, int odkazLength, EventHandler<AnalytickaEvidenciaNovaHodnota> novyRiadokHandler)
        {
            using (var cmd = new SqlCommand($"if not exists (select top 1 1 from {Server.DatabaseName}.{table} where Rok=@rok and Kod=@kod)" +
                $" begin insert into {Server.DatabaseName}.{table} values (@rok, @kod, @odkaz, @nazov, @popis) end", conn))
            {
                if (table == "[dbo].[cis_zk4]")
                {
                    cmd.CommandText = $"if not exists (select top 1 1 from {Server.DatabaseName}.{table} where Rok=@rok and Kod=@kod)" +
                        $" begin insert into {Server.DatabaseName}.{table} values (@rok, @kod, @odkaz, @nazov, @popis, '', '', '' ,'', '', '') end";
                }

                cmd.Parameters.AddWithValue("@nazov", DefaultNazov);
                cmd.Parameters.AddWithValue("@popis", DefaultPopis);
                cmd.Parameters.AddWithValue("@rok", year);
                cmd.Parameters.Add("@kod", System.Data.SqlDbType.Char);
                cmd.Parameters.Add("@odkaz", System.Data.SqlDbType.Char);

                foreach (var f in (from e in values select new String(e.Take(valueLength).ToArray())).Distinct())
                {
                    cmd.Parameters["@kod"].Value = f;
                    cmd.Parameters["@odkaz"].Value = f.Substring(0, odkazLength);
                    if (1 == await cmd.ExecuteNonQueryAsync())
                    {
                        if (_log != null)
                            await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage, "CiselnikyManager.CheckValue", $"New {table} added: {f} for year: {year}");
                        var hodnota = new AnalytickaEvidenciaNovaHodnota(year, f, table);
                        novyRiadokHandler?.Invoke(this, hodnota);
                        if (hodnota.IsChanged)
                            await UpdateValue(conn, hodnota, table);
                    }
                }
            }
        }
        private async Task CheckRoot(SqlConnection conn, string table, int year, IEnumerable<string> values, int valueLength, EventHandler<AnalytickaEvidenciaNovaHodnota> novyRiadokHandler)
        {
            using (var cmd = new SqlCommand($"if not exists (select top 1 1 from {Server.DatabaseName}.{table} where Rok=@rok and Kod=@kod) " +
                    $"begin insert into {Server.DatabaseName}.{table} values (@rok, @kod, @nazov, @popis) end", conn))
            {
                cmd.Parameters.AddWithValue("@nazov", DefaultNazov);
                cmd.Parameters.AddWithValue("@popis", DefaultPopis);
                cmd.Parameters.AddWithValue("@rok", year);
                cmd.Parameters.Add("@kod", System.Data.SqlDbType.Char);

                foreach (var f in (from e in values select new String(e.Take(valueLength).ToArray())).Distinct())
                {
                    cmd.Parameters["@kod"].Value = f;
                    if (1 == await cmd.ExecuteNonQueryAsync())
                    {
                        if (_log != null)
                            await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage, "CiselnikyManager.CheckRoot", $"New {table} added: {f} for year: {year}");
                        var hodnota = new AnalytickaEvidenciaNovaHodnota(year, f, table);
                        novyRiadokHandler?.Invoke(this, hodnota);
                        if (hodnota.IsChanged)
                            await UpdateValue(conn, hodnota, table);
                    }
                }
            }
        }

        #endregion
    }
}