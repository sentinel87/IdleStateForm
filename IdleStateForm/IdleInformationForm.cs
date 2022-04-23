using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleStateForm
{
    public partial class IdleInformationForm : Form
    {
        public IdleInformationForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public void setTimeoutString(int totalTimeout)
        {
            TimeSpan t = TimeSpan.FromSeconds(totalTimeout);
            string text = $"There was no activity during the last ";
            if (t.Hours > 0)
                text += t.Hours > 1 ? $"{t.Hours} hours " : $"{t.Hours} hour ";
            if (t.Minutes > 0)
                text += t.Minutes > 1 ? $"{t.Minutes} minutes " : $"{t.Minutes} minute ";
            if (t.Seconds > 0)
                text += t.Seconds > 1 ? $"{t.Seconds} seconds " : $"{t.Seconds} second ";
            lblInfo.Text = $"There was no activity during the { text }. Work or close app.";
        }

        public void AdjustWindow(int width)
        {
            int modifier = 0;
            if (width >= 100)
                modifier = 20;
            this.Width = width - modifier;

            lblWarn.Left = (this.Width - lblWarn.Width) / 2;
            lblInfo.Left = (this.Width - lblInfo.Width) / 2;
            btnOk.Left = (this.Width - btnOk.Width) / 2;
        }
    }
}
