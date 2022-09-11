namespace cvti.data.Classifiers
{
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Manazer pre ciselnik syntetickych uctov
    /// </summary>
    /// <remarks>
    /// Umoznuje editaciu a ziskanie uctov. Pridavanie uctov prebieha pocas importu dat
    /// </remarks>
    public class UctyManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Indikuje uspesny update syntetickeho uctu ako <see cref="UcetRiadok"/>
        /// </summary>
        public event EventHandler<UcetRiadok> UcetUpdatnuty;
        /// <summary>
        /// Indikuje uspesne vytvorenu novu uctovu plozku ako <see cref="string"/> kde hodnota je primarny kluc
        /// </summary>
        /// <remarks>
        /// Ucty vznikaju pri kontrole metoda <see cref="ValidateUcty(IEnumerable{string})"/>
        /// </remarks>
        public event EventHandler<string> UcetVytvoreny;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        public UctyManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log;
        }

        #endregion

        #region Public And Internal Methods

        /// <summary>
        /// Updatne menitelne udaje pre riadok na zaklade parametru
        /// </summary>
        /// <param name="riadok">Riadok ktory updatujem</param>
        /// <returns>True ak sa mi podari updatnut, inak false</returns>
        /// <remarks>
        /// Uspesny update je indikovany eventom <see cref="UcetUpdatnuty"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException">V pripade ak je parameter null</exception>
        public async Task<bool> UpdateRiadok(UcetRiadok riadok)
        {
            if (riadok is null)
            {
                throw new ArgumentNullException(nameof(riadok));
            }

            if (await Update(riadok, _log, "UctyManager.UpdateRiadok"))
            {
                UcetUpdatnuty?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Vrati vsetky dostupne synteticke ucty
        /// </summary>
        /// <returns>Synteticke ucty ako <see cref="IEnumerable{T}"/> kde T je <see cref="UcetRiadok"/></returns>
        public async Task<IEnumerable<UcetRiadok>> VratUcty()
        {
            try
            {
                var ucty = new List<UcetRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = DatabaseUtilities.GenerateSelectCommand<UcetRiadok>(conn, Server.DatabaseName, UcetRiadok.TableName))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            ucty.Add(new UcetRiadok(reader));
                }
                return ucty;
            }
            catch (Exception ex)
            {
                if (_log != null)
                {
                    await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                        "UctyManager.VratUcty", ex.Message + "|" + ex.StackTrace);
                }
                throw;
            }
        }

        /// <summary>
        /// Zvaliduje ci sa hodnoty poskytnute ako argument uz nachadzaju v ciselniku uctov
        /// </summary>
        /// <param name="ucty">Kontrolovane ucty ako <see cref="IEnumerable{T}"/> kde T je <see cref="string"/></param>
        /// <returns>Priaden ucty ako <see cref="IEnumerable{T}"/> kde T je <see cref="string"/></returns>
        /// <remarks>
        /// Pridany ucet je indikovany eventom <see cref="UcetVytvoreny"/>
        /// </remarks>
        internal async Task<IEnumerable<string>> ValidateUcty(IEnumerable<string> ucty)
        {
            var pridaneUcty = new List<string>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"if not exists (select top 1 1 from {Server.DatabaseName}.{UcetRiadok.TableName} where Kod=@kod) begin insert into {Server.DatabaseName}.{UcetRiadok.TableName} (Kod, Nazov, Popis) values (@kod, @nazov, @popis) end", conn))
            {
                cmd.Parameters.Add("@kod", SqlDbType.Char);
                cmd.Parameters.Add("@nazov", SqlDbType.Char);
                cmd.Parameters.Add("@popis", SqlDbType.Char);

                foreach (var u in ucty)
                {
                    cmd.Parameters["@kod"].Value = u;
                    cmd.Parameters["@nazov"].Value = "-";
                    cmd.Parameters["@popis"].Value = "-";

                    if (1 == await cmd.ExecuteNonQueryAsync())
                    {
                        pridaneUcty.Add(u);
                        if (_log != null)
                            await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage,
                                "UctyManager.ValidateUcty", $"New ucet added: {u}.");

                        UcetVytvoreny?.Invoke(this, u);
                    }
                }
            }

            return pridaneUcty;
        }

        #endregion
    }
}
