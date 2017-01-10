using System;
using System.Windows.Forms;
using FEITS.Model;
using FEITS.View;
using FEITS.Controller;

namespace FEITS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainView = new CompactMainForm();
            var model = new ConversationModel();
            var controller = new MainController(mainView, model);
            controller.LoadAssets();
            Application.Run(mainView);
        }
    }
}
