namespace FEITS.Controller
{
    public interface IExportImportView
    {
        string MessageText { get; set; }
        string StatusText { get; set; }
        bool AllowImport { get; set; }
        bool ContainsGenderCode { get; set; }

        void SetController(ImportExportController controller);
    }
}
