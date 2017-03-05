using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using IfTextEditor.Editor.View;
using IfTextEditor.Editor.Controller;
using IfTextEditor.Editor.Model;

namespace IfTextEditor.Editor
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Run updater
            Update.Program.RunAutoUpdate();

            //Initialize assets
            PreviewFont.Initialize();
            CharacterData.Initialize();

            var model = new ConversationModel();
            var view = new MainView();
            var controller = new MainController(view, model);

            Application.Run(view);
        }
    }
}
