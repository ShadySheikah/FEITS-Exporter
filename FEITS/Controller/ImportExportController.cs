using FEITS.Model;
using System.Collections.Generic;
using System.Linq;

namespace FEITS.Controller
{
    public class ImportExportController
    {
        private readonly IExportImportView view;
        public string MessageScript { get; private set; }

        private readonly bool[] validChars = AssetGeneration.ValidCharacters;

        public ImportExportController(IExportImportView v, string currentMessage)
        {
            view = v;
            MessageScript = currentMessage;
            view.SetController(this);
            SetupView();
        }

        private void SetupView()
        {
            view.MessageText = MessageScript;
        }

        public void OnImportMsgChanged()
        {
            MessageScript = view.MessageText;

            CheckForValidChars();
            view.ContainsGenderCode = HasGenderCode();
        }

        private void CheckForValidChars()
        {
            if (MessageScript.StartsWith("MESS_"))
            {
                view.AllowImport = true;
                view.StatusText = "";
                return;
            }

            var containsInvalids = false;
            var invalidChars = new SortedSet<char>();
            foreach (var c in MessageScript.Where(c => !validChars[AssetGeneration.GetValue(c)]))
            {
                containsInvalids = true;
                if (!invalidChars.Contains(c))
                    invalidChars.Add(c);
            }

            if (containsInvalids)
            {
                view.AllowImport = false;
                view.StatusText =
                    $"Warning! Text contains one or more unsupported characters: {string.Join(",", invalidChars)}";
            }
            else
            {
                view.AllowImport = true;
                view.StatusText = "";
            }
        }

        private bool HasGenderCode()
        {
            return MessageScript.Contains("VOICE_PLAYER");
        }

        public void SwapGenderCode()
        {
            const string vpMale = "VOICE_PLAYER_M#",
                         vpFemale = "VOICE_PLAYER_F#";
            view.MessageText = view.MessageText.Contains(vpMale)
                ? MessageScript.Replace(vpMale, vpFemale)
                : MessageScript.Replace(vpFemale, vpMale);

            view.StatusText =
                "NOTE: Gender has been swapped in code, but words like \"Lord\" and \"Lady\" remain unchanged in dialogue.";
        }
    }
}