namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    
    using cvti.data.Core;
    using cvti.data.Enums;
    using cvti.data.Tables;

    /// <summary>
    /// Sluzi na zobrazenie LOG message
    /// </summary>
    public partial class ActionControl : UserControl
    {
        public ActionControl()
        {
            InitializeComponent();
            pictureBox1.BringToFront();
            LinkLabelTitle.MouseEnter += Component_MouseEnter;
            LabelInfo.MouseEnter += Component_MouseEnter;
            panelBorder.MouseEnter += Component_MouseEnter;
            panelSeparator.MouseEnter += Component_MouseEnter;
            pictureBoxIcon1.MouseEnter += Component_MouseEnter;
            lblActionType.MouseEnter += Component_MouseEnter;
            lblWhen.MouseEnter += Component_MouseEnter;

            LinkLabelTitle.MouseLeave += Component_MouseLeave;
            LabelInfo.MouseLeave += Component_MouseLeave;
            panelBorder.MouseLeave += Component_MouseLeave;
            panelSeparator.MouseLeave += Component_MouseLeave;
            pictureBoxIcon1.MouseLeave += Component_MouseLeave;
            lblActionType.MouseLeave += Component_MouseLeave;
            lblWhen.MouseLeave += Component_MouseLeave;
        }

        public LogMessage LogMessage { get; private set; }

        public void ShowAction(LogMessage log)
        {
            LogMessage = log;

            if (log is null) 
            {
                pictureBox1.BringToFront(); 
            }
            else
            {
                pictureBox1.SendToBack();
                var creatorData = log.CreatedBy.Split('|');
                var type = (LogMessageType)log.LogType;

                LabelInfo.Text = string.Join(Environment.NewLine, creatorData) + log.LogInfo;
                LinkLabelTitle.Text = log.LogTitle;
                lblActionType.Text = (type).ToString();

                lblWhen.Text = log.CreatedDate.ToString();
                toolTipInfo.SetToolTip(lblWhen, log.CreatedDate.ToString());

                var pb1ToolTip = string.Empty;
                var pb2ToolTip = string.Empty;
                switch (type)
                {
                    case LogMessageType.BackEndMessage:
                        pb1ToolTip = "Info message";
                        pb2ToolTip = "Back end info message";
                        pictureBoxIcon1.Image = Properties.Resources.info20;
                        pictureBoxIcon2.Image = Properties.Resources.gear20;
                        break;
                    case LogMessageType.BackEndError:
                        pb1ToolTip = "Error message";
                        pb2ToolTip = "Back end error message";
                        pictureBoxIcon1.Image = Properties.Resources.error20;
                        pictureBoxIcon2.Image = Properties.Resources.gear20;
                        break;
                    case LogMessageType.FrontEndMessage:
                        pb1ToolTip = "Info message";
                        pb2ToolTip = "Front end info message";
                        pictureBoxIcon1.Image = Properties.Resources.info20;
                        pictureBoxIcon2.Image = Properties.Resources.display20;
                        break;
                    case LogMessageType.FrontEndError:
                        pb1ToolTip = "Error message";
                        pb2ToolTip = "Front end error message";
                        pictureBoxIcon1.Image = Properties.Resources.error20;
                        pictureBoxIcon2.Image = Properties.Resources.display20;
                        break;
                    default:
                        break;
                }
                toolTipInfo.SetToolTip(pictureBoxIcon1, pb1ToolTip);
                toolTipInfo.SetToolTip(pictureBoxIcon2, pb2ToolTip);
            }
        }

        private void Component_MouseLeave(object sender, EventArgs e)
        {
            LinkLabelTitle.LinkColor = Color.FromArgb(21, 72, 144);
            BackColor = Color.White;
        }

        private void Component_MouseEnter(object sender, EventArgs e)
        {
            LinkLabelTitle.LinkColor = Color.FromArgb(21, 72, 244);
            BackColor = Color.FromArgb(245, 245, 245);
        }

        private void panelSeparator_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawLine(new Pen(Brushes.LightGray), 
            new Point(3, panelSeparator.Height - 1),
            new Point(panelSeparator.Width - 6, panelSeparator.Height - 1));

        private void component_Clicked(object sender, EventArgs e) => MouseClicked?.Invoke(this, EventArgs.Empty);

        public event EventHandler MouseClicked;

        private void LinkLabelTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
