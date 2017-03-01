using FEITS.Model;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FEITS.View
{
    public partial class LoadingPopup : Form
    {
        public LoadingPopup()
        {
            InitializeComponent();
            //BeginLoading();
        }

        public void BeginLoading()
        {
            assetLoader.RunWorkerAsync();
        }

        private void assetLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PB_Progress.Value = e.ProgressPercentage;
        }

        private void assetLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                Dispose();
            }
        }

        private void assetLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            AssetGeneration.Initialize(worker, e);
        }
    }
}
