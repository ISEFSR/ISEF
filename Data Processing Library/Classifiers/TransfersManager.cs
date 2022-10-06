namespace cvti.data.Classifiers
{
    using cvti.data.Conditions;
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;
    using cvti.data.Views;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Manazer pre ciselnik transferovych polozkiek. Transferove polozky tak ako ciselniky analytickej evidencie su viazane na kalendarny rok
    /// </summary>
    /// <remarks>
    /// Umoznuje pre vybrany rok pridavat odstranovat a updatovat trasnery
    /// </remarks>
    public class TransfersManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Indikuje vytvorenie novej transferovej polozky pre rok ako <see cref="TransferRiadok"/>
        /// </summary>
        public event EventHandler<TransferRiadok> TrasnferPridany;
        /// <summary>
        /// Indikuje update transferovej polozky pre rok ako <see cref="TransferRiadok"/>
        /// </summary>
        public event EventHandler<TransferRiadok> TransferUpdatnuty;
        /// <summary>
        /// Indikuje odstranenie transferovej polozky pre rok ako <see cref="TransferRiadok"/>
        /// </summary>
        public event EventHandler<TransferRiadok> TransferOdstraneny;

        #endregion

        #region Variables And Constructors

        private readonly LogManager _log;

        /// <summary>
        /// Inicializuje novy trasnfer manager zodpovedny za manipulaciu s transfermi
        /// </summary>
        /// <param name="server">MSSQL sever a databaze ako <see cref="MSSQLServer"/></param>
        /// <param name="log">Log manager ako <see cref="LogManager"/></param>
        public TransfersManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Vrati transferove polozky pre vybrany kalendarny rok
        /// </summary>
        /// <param name="rok">Rok ako <see cref="int"/></param>
        /// <returns></returns>
        public async Task<IEnumerable<TransferRiadok>> GetTransfers(int rok)
        {
            try
            {
                var transfers = new List<TransferRiadok>();
                using (var conn = await Server.GetConnectionAsync(true))
                using (var cmd = TransferRiadok.GetSelectCommand(conn, Server.DatabaseName, rok))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                        while (await reader.ReadAsync())
                            transfers.Add(new TransferRiadok(reader));
                }
                return transfers;
            }

            catch (Exception ex)
            {
                await _log.LogNewMessageSafeAsync(LogMessageType.BackEndError,
                    "TrasnferManager.GetTransfers", ex.Message + "|" + ex.StackTrace);
                throw;
            }
        }

        public Condition GetWithCondition(IEnumerable<TransferRiadok> transfery)
        {
            Condition transfers = new NotEquals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.Rok), "9999");
            transfers.Wrap = true;

            var first = true;
            foreach (var t in transfery)
            {
                if (first)
                {
                    transfers.AddCondition(t.GetCondition(), ConditionOperator.And);
                    first = false;
                }
                transfers.AddCondition(t.GetCondition(), ConditionOperator.Or);
            }

            return transfers;
        }

        public Condition GetWithoutCondition(IEnumerable<TransferRiadok> transfery)
        {
            var cnd = GetWithCondition(transfery);
            cnd.Negate = true;
            return cnd;
        }

        /// <summary>
        /// Updatne hodnoty v riadku z cislenika transferov na zaklade paramterov
        /// </summary>
        /// <param name="riadok"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRiadok(TransferRiadok riadok)
        {
            if (await Update(riadok, _log, "TrasnferManager.UpdateTransfer"))
            {
                TransferUpdatnuty?.Invoke(this, riadok);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Prida novy riadok medzi transfery
        /// </summary>
        /// <param name="riadok"></param>
        /// <returns></returns>
        public async Task<TransferRiadok> PridajRiadok(EkonomickaRiadok6 riadok, StupenRiadok from, StupenRiadok to)
        {
            var transferRiadok = new TransferRiadok
            {
                FromStupen = from.Kod,
                ToStupen = to.Kod,
                Polozka = riadok.Kod,
                Rok = riadok.Rok
            };

            if (await Insert(transferRiadok, _log, "TransferManager.PridajRiadok"))
            {
                TrasnferPridany?.Invoke(this, transferRiadok);
                return transferRiadok;
            }
            return null;
        }
        
        /// <summary>
        /// Odstrani riadok transaferov
        /// </summary>
        /// <param name="riadok"></param>
        /// <returns></returns>
        public async Task<bool> OdstranRiadok(TransferRiadok riadok)
        {
            if (await Delete(riadok, _log, "TransferManager.OdstranRiadok"))
            {
                TransferOdstraneny?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        #endregion
    }
}
