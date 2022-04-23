using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace IdleStateForm
{
    public partial class MainForm : Form
    {
        Timer _idleTimer;
        IdleInformationForm _idleInformationForm;
        bool _idleWarnDisplayed = false;
        public MainForm(bool useIdleMode)
        {
            InitializeComponent();
            _idleTimer = new Timer();
            _idleTimer.Enabled = false;
            _idleInformationForm = new IdleInformationForm();
            if(useIdleMode)
                initializeIdleStateChecker();
        }

        private void initializeIdleStateChecker()
        {
            try
            {
                int idleCheckTimeout = Int32.Parse(ConfigurationManager.AppSettings["IdleCheckTimeout"]);
                int miliseconds = idleCheckTimeout * 1000;
                _idleInformationForm.setTimeoutString(idleCheckTimeout);
                _idleTimer.Interval = miliseconds;
                _idleTimer.Tick += new EventHandler(onIdleTimePass);
                Application.Idle += new EventHandler(onIdle);
                IdleChangeMessageFilter messageFilter = new IdleChangeMessageFilter();
                messageFilter.BreakIdleCounting += new Action<bool>(onBreakIdleCounting);
                Application.AddMessageFilter(messageFilter);
                _idleTimer.Enabled = true;
            }
            catch(Exception ex)
            {

            }
        }

        private void onIdle(object obj, EventArgs e)
        {
            if (!_idleWarnDisplayed)
            {
                if (!_idleTimer.Enabled)
                    _idleTimer.Start();
            }
        }

        private void onIdleTimePass(object obj, EventArgs e)
        {
            _idleTimer.Stop();
            this.Enabled = false;
            _idleWarnDisplayed = true;        
            _idleInformationForm.AdjustWindow(this.Width);
            _idleInformationForm.ShowDialog();
            _idleWarnDisplayed = false;
            this.Enabled = true;
        }

        private void onBreakIdleCounting(bool result)
        {
            if(result)
            {
                if (!_idleWarnDisplayed)
                {
                    if (_idleTimer.Enabled)
                        _idleTimer.Stop();
                }
            }
        }

        private void btnAdditional_Click(object sender, EventArgs e)
        {
            using (AdditionalForm form = new AdditionalForm())
            {
                form.ShowDialog();
            }
        }
    }
}
