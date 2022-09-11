namespace cvti.isef.winformapp.Forms.Dashboard
{
    using cvti.data;
    using cvti.isef.winformapp.Controls.Main.Dashboard;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class ZmenTextaciuForm : DialogBase
    {
        public ZmenTextaciuForm(SingleWarningItem item)
        {
            InitializeComponent();

            labelSubTitle.Text = labelSubTitle.Text.Replace("{rok}", item.Item.Rok.ToString()).Replace("{kod}", item.Item.Kod).Replace("{cis}", item.Item.TableName);

            cueTextBoxKod.Text = item.Item.Kod;
            cueTextBoxRok.Text = item.Item.Rok.ToString();

            //cueTextBoxShort.Cue = "Krátky text...";
            //cueTextBoxLong.Cue = "Dlhý text...";

            ShowWait();

            ShowImageIcon = false;

            cueTextBoxShort.Focus();
        }

        public string KratkyText { get => cueTextBoxShort.Text; }
        public string DlhyText { get => cueTextBoxLong.Text; }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await Task.Delay(1250);
            HideWait();
        }

        public override void ShowWait()
        {
            base.ShowWait();
            panelContent.Visible = false;
        }

        public override void HideWait()
        {
            base.HideWait();
            panelContent.Visible = true;
        }
    }
}
