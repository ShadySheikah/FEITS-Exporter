using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IfTextEditor.Editor.View;
using IfTextEditor.Editor.Controller;
using IfTextEditor.Editor.Model;

namespace IfTextEditor.Editor
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

            var sw = new Stopwatch();
            sw.Start();
            PreviewFont.Initialize();
            CharacterData.Initialize();
            sw.Stop();
            Debug.WriteLine("Init time: " + sw.Elapsed);

            var model = new ConversationModel();
            var view = new MainView();
            var controller = new MainController(view, model);

            Application.Run(view);
        }
    }
}
