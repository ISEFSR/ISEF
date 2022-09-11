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
    /// Manager zodpovedny za manipulaciu s ciselnikom organizacii
    /// </summary>
    /// <remarks>
    /// Nove organizacie vznikaju iba pri importe dat. Organizacie niesu viazane na rok
    /// </remarks>
    public class OrganizacieManager : DataManager
    {
        #region Public Events

        /// <summary>
        /// Event signalizujuci pridanu novu organizaciu ako <see cref="NovaOrganizacia"/>
        /// </summary>
        /// <remarks>
        /// Oragniazacie sa pridavaju iba pri nacitani novych dat. User moze zmenit hodnoty
        /// pri novej organizacii tie sa automaticky prenesu do databazovej tabulky
        /// </remarks>
        public event EventHandler<NovaOrganizacia> OrganizaciaAdded;

        /// <summary>
        /// Event signalizujuci zmenenu organizaciu ako <see cref="OrganizaciaRiadok"/>
        /// </summary>
        public event EventHandler<OrganizaciaRiadok> OrganizaciaUpdatnuta;

        #endregion

        #region Private Variables And Constructors

        private readonly LogManager _log;

        public OrganizacieManager(MSSQLServer server, LogManager log)
            : base(server)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public And Internal Methods

        internal async Task ValidateOrganizationsAsync(IEnumerable<string> organizations)
        {
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = new SqlCommand($"if not exists (select top 1 1 from {Server.DatabaseName}.{OrganizaciaRiadok.TableName} where Ico=@ico) begin insert into {Server.DatabaseName}.{OrganizaciaRiadok.TableName} (Ico, Nazov, Ulica) values (@ico, @nazov, @ulica) end", conn))
            {
                cmd.Parameters.Add("@ico", SqlDbType.Char);
                cmd.Parameters.Add("@nazov", SqlDbType.Char);
                cmd.Parameters.Add("@ulica", SqlDbType.Char);

                var noveOrganizacie = new List<NovaOrganizacia>();
                foreach (var o in organizations)
                {
                    
                    cmd.Parameters["@ico"].Value = o;
                    cmd.Parameters["@nazov"].Value = "-";
                    cmd.Parameters["@ulica"].Value = "-";
                    if (1 == await cmd.ExecuteNonQueryAsync())
                    {
                        if (_log != null)
                            await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage,
                                "OrganizacieManager.ValidateOrganizationsASync",
                                $"New organization added: {o}.");

                        var org = new NovaOrganizacia(o);

                        OrganizaciaAdded?.Invoke(this, org);

                        if (org.IsChanged)
                        {
                            noveOrganizacie.Add(org);
                        }
                    }
                }

                cmd.CommandText = $"update {Server.DatabaseName}.{OrganizaciaRiadok.TableName} set Nazov=@nazov, Ulica=@ulica, KodObec=@obec where Ico=@ico";
                cmd.Parameters.Add("@nazov", SqlDbType.NVarChar);
                cmd.Parameters.Add("@ulica", SqlDbType.NVarChar);
                cmd.Parameters.Add("@obec", SqlDbType.Int);
                foreach (var o in noveOrganizacie)
                {
                    cmd.Parameters["@ico"].Value = o.ICO;
                    cmd.Parameters["@nazov"].Value = o.Nazov;
                    cmd.Parameters["@ulica"].Value = o.Ulica;
                    cmd.Parameters["@obec"].Value = o.KodObce;
                    if (1 == await cmd.ExecuteNonQueryAsync())
                    {
                        if (_log != null)
                            await _log.LogNewMessageSafeAsync(LogMessageType.BackEndMessage,
                                "OrganizacieManager.ValidateOrganizationsASync",
                                $"Organization updated: {o.ICO}, {o.Nazov}.");
                    }
                }
            }
        }

        public async Task<IEnumerable<OrganizaciaRiadok>> VratOrganizacie(ObecRiadok obec = null)
        {
            var org = new List<OrganizaciaRiadok>();
            using (var conn = await Server.GetConnectionAsync(true))
            using (var cmd = OrganizaciaRiadok.GetSelectCommand(conn, Server.DatabaseName))
            {
                if (obec != null)
                {
                    cmd.CommandText += " WHERE KodObec=@obec";
                    cmd.Parameters.AddWithValue("@obec", obec.Kod);
                }
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        org.Add(new OrganizaciaRiadok(reader));
                    }
            }
            return org;
        }

        public async Task<bool> UpdateOrganizacia(OrganizaciaRiadok riadok)
        {
            if (await Update(riadok, _log, "OrganizacieManager.UpdateOrganizacia"))
            {
                OrganizaciaUpdatnuta?.Invoke(this, riadok);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateRiadok(CiselnikRiadok riadok)
        {
            if (await Update(riadok, _log, "OrganizacieManager.UpdateRiadok"))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}