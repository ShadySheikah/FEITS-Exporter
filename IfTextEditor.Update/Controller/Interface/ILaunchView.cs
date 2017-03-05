namespace IfTextEditor.Update.Controller.Interface
{
    internal interface ILaunchView
    {
        string StatusDesc { get; set; }
        int Progress { get; set; }

        void SetController(LauncherController c);
    }
}
