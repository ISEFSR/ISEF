namespace cvti.isef.winformapp.Controls.Main.Ciselniky
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using cvti.data;
    using cvti.data.Tables;
    using cvti.data.Enums;
    using cvti.data.Core;
    using System.Collections.Generic;
    using cvti.isef.winformapp.Forms;
    using cvti.data.Conditions;
    using cvti.data.Views;
    using cvti.data.Columns;
    using System.Diagnostics;
    using System.Drawing;

    public partial class TransferControl : UserControl, ICiselnikControl
    {
        // Zoznam dostupnych ekonomickych poloziek pre vybrnay rok
        // naacita sa vvzdy pri zmene vybraneho roku
        private IEnumerable<EkonomickaRiadok6> _ekonomickePolozky;

        public TransferControl()
        {
            InitializeComponent();
        }

        public int SelectedYear { get; private set; }

        public string TitleText { get => "Číselník transferov"; }

        public string InfoText { get => "Číselník transferov. Číselník neni viazaný na kalendárny rok."; }

        public void Deaktivuj()
        {
            listBoxTransfers.Items.Clear();
        }

        public async Task ExportData(string path)
        {
            await Task.Delay(1);
            ExcelUtilities.ExportCiselnikToExcel(
                await _manager.MSSQLManager.Transfery.GetTransfers(SelectedYear), path); 
        }

        public async Task GenerateData()
        {
            await Task.Delay(1);
            MessageBox.Show("Mrzí ma to, ale generovanie transferových položiek z iného roku, alebo z prednahratých dát zatiaľ nie je implementované.",
                "Feature not implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private ISEFDataManager _manager;

        public event EventHandler<Tuple<Column, object, AvailableConditions>> ConditionAdded;

        public async Task NacitajDataAsync(ISEFDataManager manager, int year)
        {
            if (_manager != null)
            {
                listBoxTransfers.Items.Clear();
                comboBoxFrom.Items.Clear();
                comboBoxTo.Items.Clear();

                _manager.MSSQLManager.Transfery.TransferOdstraneny -= Transfery_TransferOdstraneny;
                _manager.MSSQLManager.Transfery.TransferUpdatnuty -= Transfery_TransferUpdatnuty;
                _manager.MSSQLManager.Transfery.TrasnferPridany -= Transfery_TrasnferPridany;
            }

            _manager = manager;
            SelectedYear = year;

            _manager.MSSQLManager.Transfery.TransferOdstraneny += Transfery_TransferOdstraneny;
            _manager.MSSQLManager.Transfery.TransferUpdatnuty += Transfery_TransferUpdatnuty;
            _manager.MSSQLManager.Transfery.TrasnferPridany += Transfery_TrasnferPridany;

            _ekonomickePolozky = await _manager.MSSQLManager.CiselnikyManager.
                VratCiselnikPreRokAsync<EkonomickaRiadok6>(year);

            comboBoxPolozka.DataSource = _ekonomickePolozky;
            foreach (var s in _manager.Stupne)
            {
                comboBoxFrom.Items.Add(s);
                comboBoxTo.Items.Add(s);
            }

            textBoxRok.Text = year.ToString();

            var data = await manager.MSSQLManager.Transfery.GetTransfers(SelectedYear);
            foreach (var d in data)
            {
                listBoxTransfers.Items.Add(d);
            }

            if (listBoxTransfers.Items.Count > 0 &&
                listBoxTransfers.SelectedIndex == -1)
            {
                listBoxTransfers.SelectedIndex = 0;
            }
        }

        private TransferRiadok VratVybrany() => listBoxTransfers.SelectedItem as TransferRiadok;

        private void Transfery_TrasnferPridany(object sender, TransferRiadok e)
        {
            listBoxTransfers.Items.Add(e);
        }

        private void Transfery_TransferUpdatnuty(object sender, TransferRiadok e)
        {
            // prezatial to bude fugnovat, lebo odstranujem vzdy iba vybrany
            // ale v momnente ak mozes nejkym sposobom odstranit aj nevybrany tak to bude treba prerobit
            Debug.Assert(e.Polozka == VratVybrany()?.Polozka);

            var index = listBoxTransfers.SelectedIndex;
            listBoxTransfers.Items.RemoveAt(index);
            listBoxTransfers.Items.Insert(index, e);
            listBoxTransfers.SelectedIndex = index;
        }

        private void Transfery_TransferOdstraneny(object sender, TransferRiadok e)
        {
            listBoxTransfers.Items.Remove(e);
        }

        public async Task RemoveData()
        {
            await Task.Delay(1);
            MessageBox.Show("Mrzí ma to, ale odstraňovanie transferových položiek pre vybraný rok nie je implementované.",
                "Feature not implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public async Task UpdateData()
        {
            await Task.Delay(1);
            MessageBox.Show("Mrzí ma to, ale hromadný update transferových položiek z iného roku, alebo z prednahratých dát zatiaľ nie je implementované.",
                "Feature not implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public bool CanGenerate() => true;

        public bool CanUpdate() => true;

        public bool CanRemove() => true;

        public async Task ReloadData()
        {
            await NacitajDataAsync(_manager, SelectedYear);
        }

        public async Task ShowPreview()
        {
            await Task.Delay(1);

            var p = new List<String>();
            var s = new List<string>();
            foreach (var t in listBoxTransfers.Items)
            {
                s.Add((t as TransferRiadok).FromStupen);
                p.Add((t as TransferRiadok).Polozka);
            }

            var polozky = new Inlist(string.Empty,
                AssuView.VratStlpec(AssuViewAvailableColumns.EKod6),
                p.ToArray()).AddCondition(new Inlist(string.Empty,
                AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod),
                s.ToArray()), ConditionOperator.And);

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
                new AssuViewColumn[]
                {
                    AssuView.VratStlpec(AssuViewAvailableColumns.StupenShort, true, "Stupen"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.OrgIco, true, "ICO"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.OrgNazov, true, "Nazov"),
                    AssuView.VratStlpec(AssuViewAvailableColumns.EKod6, true, "Položka")
                }, SelectedYear, polozky, $"Prehľad transferov pre rok {SelectedYear} podľa organizácií");
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddConditionForSelected(AvailableConditions.Equals);
        }

        private void AddConditionForSelected(AvailableConditions op)
        {
            var vybrany = VratVybrany();
            if (vybrany != null)
            {
                var polozka = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), vybrany.Polozka);
                var stupen = new Equals(string.Empty, AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), vybrany.FromStupen);

                var podmienka = polozka.CloneMe(true);
                podmienka.AddCondition(stupen, ConditionOperator.And);

                // Moznost pridavania podmienky pre transferove polozky este nefunguje
                //ConditionAdded?.Invoke(this, new Tuple<CiselnikRiadok, AvailableConditions>(, op));
            }
        }

        private async void buttonAddNew_Click(object sender, EventArgs e)
        {
            await CreateItem();
        }

        private void listBoxTransfers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTransfers.SelectedIndex == -1)
                return;

            ShowSelected();
        }

        private void ShowSelected()
        {
            var trasnfer = VratVybrany();
            if (trasnfer is null)
                return;

            comboBoxFrom.SelectedItem = _manager.VratStupen(trasnfer.FromStupen);
            comboBoxTo.SelectedItem = _manager.VratStupen(trasnfer.ToStupen);
            comboBoxPolozka.SelectedItem = (from p in _ekonomickePolozky where p.Kod == trasnfer.Polozka select p).FirstOrDefault();
        }

        private async void buttonShowPreview_Click(object sender, EventArgs e)
        {
            if (!(listBoxTransfers.SelectedItem is TransferRiadok transfer))
                return;

            var condPolozka = new Equals(string.Empty,
                AssuView.VratStlpec(AssuViewAvailableColumns.EKod6), transfer.Polozka);

            condPolozka.AddCondition(new Equals(string.Empty,
                AssuView.VratStlpec(AssuViewAvailableColumns.StupenKod), transfer.FromStupen),
                ConditionOperator.And);

            await CiselnikyUtilities.ShowPreviewForColumns(_manager,
               CiselnikyUtilities.VratStlpcePrePreview(), SelectedYear, condPolozka,
               $"Prehľad trasnferu {transfer.Polozka} zo stupňa {transfer.FromStupen} pre rok {SelectedYear}.");
        }

        private async void buttonRemoveSelected_Click(object sender, EventArgs e)
        {
            var vybrany = VratVybrany();
            if (vybrany is null)
                return;

            if (MessageBox.Show($"Skutočne si prajete odstrániť transferovú poloźku {vybrany.Polozka} pre rok {vybrany.Rok}?", "Odstránenie transferu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (await _manager.MSSQLManager.Transfery.OdstranRiadok(vybrany))
                    {

                    }
                    else
                    {

                    }
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            ShowSelected();
        }

        private async void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (!(comboBoxFrom.SelectedItem is StupenRiadok from) || 
                !(comboBoxTo.SelectedItem is StupenRiadok to) || 
                !(comboBoxPolozka.SelectedItem is EkonomickaRiadok6 polozka) || 
                !(listBoxTransfers.SelectedItem is TransferRiadok transfer))
                return;

            try
            {
                transfer.FromStupen = from.Kod;
                transfer.ToStupen = to.Kod;
                transfer.Polozka = polozka.Kod;

                if (await _manager.MSSQLManager.Transfery.UpdateRiadok(transfer))
                {
                    var index = listBoxTransfers.SelectedIndex;
                    listBoxTransfers.Items.RemoveAt(index);
                    listBoxTransfers.Items.Insert(index, transfer);
                    listBoxTransfers.SelectedIndex = index;

                    await _manager.LogFrontendMessageSafeAsync($"Textové polia pre transferovú položku {transfer.Polozka} úspešne zmenené.",
                        "TransferControl.buttonSaveChanges_Click");
                    MessageBox.Show($"Transferová položka {transfer.Polozka} pre rok {transfer.Rok} úspešne zmenená.",
                        "Zmena úspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Vyzerá to tak, źe sa mi nepodarilo uložiť zmeny vykonané na transferovej položke. " +
                        "Pre istotu skontrolujte údaje a prípadne skúste uložiť ešte raz.",
                        "Údaje neuložené", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _manager.LogFrontendErrorSafeAsync(ex,
                    "TransferControl.buttonSaveChanges_Click");
                MessageBox.Show($"Pri pokuse o zmenu transferovej položky pre rok {transfer.Rok} nastala neočakávaná chyba. " + ex.Message,
                    "Zmena neúspešná", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool CanCreate() => true;

        public async Task CreateItem()
        {
            using (var f = new PridajTransferovuPolozkuForm(_ekonomickePolozky, _manager.Stupne))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var transfer = await _manager.MSSQLManager.Transfery.PridajRiadok(f.VybranaEkonomickaPolozka, f.Odkial, f.Kam);
                        if (transfer != null)
                        {
                            var message = $"Transferová položka pre rok {transfer.Rok} úspešne pridaná. Kód trasnferovej položky {transfer.Polozka}";
                            MessageBox.Show(message, "Transferová položka prdaná",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            await _manager.LogFrontendMessageSafeAsync(message, "TransferControl.buttonAddNew_Click");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Pri pokuse o vytvorenie trasnerovej položky nastala chyba. " + ex.Message, 
                            "Položka nevytvorená " + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await _manager.LogFrontendErrorSafeAsync(ex, "TransferControl.buttonAddNew_Click");
                    }
                }
            }
        }

        public string GetInfoText() => null;

        public string GetMoreInfo() => null;

        private void panelControlButtons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(210, 210, 210))),
                                                    new Point(0, 0),
                                                    new Point(panelControlButtons.Width, 0));
        }

        private async void buttonGenerateDefault_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;

                var polozky = await _manager.MSSQLManager.CiselnikyManager.VratCiselnikPreRokAsync<EkonomickaRiadok6>(SelectedYear);

                var bezneObci = (from p in polozky where p.Kod == "641013" select p).FirstOrDefault();
                var kapitaloveObic = (from p in polozky where p.Kod == "721006" select p).FirstOrDefault();
                var bezneVucke = (from p in polozky where p.Kod == "641014" select p).FirstOrDefault();
                var kapitaloveVucke = (from p in polozky where p.Kod == "721007" select p).FirstOrDefault();

                if (bezneObci != null) await _manager.MSSQLManager.Transfery.PridajRiadok(bezneObci, _manager.VratStupen("v"), _manager.VratStupen("o"));
                if (kapitaloveObic != null) await _manager.MSSQLManager.Transfery.PridajRiadok(kapitaloveObic, _manager.VratStupen("v"), _manager.VratStupen("o"));
                if (bezneVucke != null) await _manager.MSSQLManager.Transfery.PridajRiadok(bezneVucke, _manager.VratStupen("2"), _manager.VratStupen("9"));
                if (kapitaloveVucke != null) await _manager.MSSQLManager.Transfery.PridajRiadok(kapitaloveVucke, _manager.VratStupen("2"), _manager.VratStupen("9"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pri pokuse o generovanie transferových položiek nastala neočakávaná chyba: " + ex.Message, "Chyba " + ex.GetType().ToString(), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                await ReloadData();
            }
        }

        void ICiselnikControl.Import()
        {
            throw new NotImplementedException();
        }
    }
}
