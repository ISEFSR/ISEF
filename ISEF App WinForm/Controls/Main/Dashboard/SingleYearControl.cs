namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using cvti.data.Views;
    using cvti.isef.winformapp.Forms;
    using cvti.data;

    public partial class SingleYearControl : UserControl
    {
        private class StupenPreview
        {
            public Label CountLabel { get; set; }
            public Label SumLabel { get; set; }
        }

        // i know i know toto je zle
        // cele zle :)
        private readonly Dictionary<string, StupenPreview> _stupne;

        // should prolly scale based on os zoom
        private static int CollapsedHeight = 195;
        private static int ExpandedHeight = 550;

        private int _rok;

        public ISEFDataManager  Manager { get; set; }

        public SingleYearControl(IEnumerable<StatsView> stats, int rok)
        {
            _rok = rok;
            var lastEdit = stats.Select(s => s.CreatedDate).Max();
            // Stats po roku a po stupnoch?
            Action<Control> subscribeMouseEvents = null;
            subscribeMouseEvents = new Action<Control>((Control c) =>
            {
                c.MouseEnter += Component_MouseEnter;
                c.MouseLeave += Component_MouseLeave;

                foreach (var ctrl in c.Controls)
                {
                    if (ctrl is Control)
                        subscribeMouseEvents(ctrl as Control);
                }
            });

            InitializeComponent();
            _stupne = new Dictionary<string, StupenPreview>()
            {
                {
                    "MaO",
                    new StupenPreview() { CountLabel = labelCountMao, SumLabel = labelSumMao }
                },
                {
                    "VUC",
                    new StupenPreview() { CountLabel = labelCountVUC, SumLabel = labelSumVUC }
                },
                {
                    "PRO",
                    new StupenPreview() { CountLabel = labelCountOPRO, SumLabel = labelSumOPRO }
                },
                {
                    "VVS",
                    new StupenPreview() { CountLabel = labelCountVVS, SumLabel = labelSumVVS }
                },
                {
                    "MV",
                    new StupenPreview() { CountLabel = labeCountMV, SumLabel = labelSumMV }
                },
            };
            labelYear.Text = _rok.ToString();
            labelShort.Text = labelShort.Text.Replace("{year}", _rok.ToString());
            subscribeMouseEvents(this);
            labelCreated.Text = lastEdit.ToString();

            ShowStats(stats);
        }

        private void ShowStats(IEnumerable<StatsView> stats)
        {
            foreach (var s in _stupne)
            {
                s.Value.CountLabel.Text = "-";
                s.Value.SumLabel.Text = "-";
            }

            foreach (var s in stats)
            {
                if (_stupne.ContainsKey(s.StupenSkratenyNazov))
                {
                    _stupne[s.StupenSkratenyNazov].CountLabel.Text = $"{s.TotalCount.ToString()} záznamov";
                    _stupne[s.StupenSkratenyNazov].SumLabel.Text = s.TotalSum.ToString("C");
                }
            }
        }

        private bool _animating = false;
        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if (_animating)
                return;

            _animating = true;
            var t = new Transitions.Transition(new Transitions.TransitionType_Acceleration(250));

            var targetHeight = Height == ExpandedHeight ? CollapsedHeight : ExpandedHeight;
            var newText = targetHeight == ExpandedHeight ? "^" : "v";
            var toolTipText = targetHeight == ExpandedHeight ? "Zabalí podrobné informácie k roku" : "Rozbalí a zobrazí podrobné informácie k roku";

            t.add(this, "Height", targetHeight);
            t.TransitionCompletedEvent += (snd, ea) =>
            {
                _animating = false;
                Invalidate();
            };

            t.run();

            toolTipInfo.SetToolTip(buttonExpand, toolTipText);
            buttonExpand.Text = newText;
        }

        private void Component_MouseLeave(object sender, EventArgs e) => BackColor = Color.White;
        private void Component_MouseEnter(object sender, EventArgs e) => BackColor = Color.FromArgb(245, 245, 245);

        private void pictureBoxInfo_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void buttonFullScreen_Click(object sender, EventArgs e)
        {
            // TODO: show full screen stats ( in dialog window )
            //MessageBox.Show("Táto možnosť neni momentálne implementovaná", "Feature not implemented yet.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (var previewForm = new PreviewRokaForm(Manager, _rok))
                previewForm.ShowDialog();
        }

        private void tableLayoutPanelTitle_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
