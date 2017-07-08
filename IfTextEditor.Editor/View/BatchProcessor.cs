using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IfTextEditor.Editor.Model;

namespace IfTextEditor.Editor.View
{
    public partial class BatchProcessor : Form
    {
        private BackgroundWorker bw;

        public BatchProcessor()
        {
            InitializeComponent();
            InitializeWorker();
        }

        #region BackgroundWorker

        private void InitializeWorker()
        {
            bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
            };

            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = BatchProcessFiles((string[]) e.Argument, (BackgroundWorker) sender);
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PB_Process.Value = e.ProgressPercentage;
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error");
            }
            else
            {
                if (e.Result == null)
                    MessageBox.Show("Unknown error occured.", "Error");
                else
                    MessageBox.Show("File processing completed successfully!");

                PB_Process.Value = 0;
            }
        }
        #endregion

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (bw.IsBusy)
            {
                MessageBox.Show("Please wait until current process is completed.");
                return;
            }

            bw.RunWorkerAsync((string[]) e.Data.GetData(DataFormats.FileDrop));
        }

        private bool BatchProcessFiles(string[] paths, BackgroundWorker worker)
        {
            var model = new ConversationModel();

            int progress = 0;
            foreach (string p in paths)
            {
                //Open file
                if (!model.LoadFromFile(p))
                    continue;

                //In case no name found
                if (model.FileCont.Name == null)
                    model.FileCont.Name = Path.GetFileNameWithoutExtension(p);

                //Name new file and export
                string newFileName = Path.Combine(Path.GetDirectoryName(p), model.FileCont.Name + "_Processed.txt");
                model.ExportStrippedFile(newFileName);

                //Report progress
                progress++;
                worker.ReportProgress(progress / paths.Length * 100);
            }

            return true;
        }
    }
}
