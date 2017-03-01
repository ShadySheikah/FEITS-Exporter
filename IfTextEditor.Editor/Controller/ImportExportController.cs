using FEITS.Model;
using System.Collections.Generic;
using System.Linq;

namespace FEITS.Controller
{
    public class ImportExportController
    {
        private IExportImportView view;
        private string messageScript;
        public string MessageScript { get { return messageScript; } }

        bool[] validChars = AssetGeneration.ValidCharacters;

        public ImportExportController(IExportImportView v, string currentMessage)
        {
            view = v;
            messageScript = currentMessage;
            view.SetController(this);
            SetupView();
        }

        private void SetupView()
        {
            view.MessageText = messageScript;
        }

        public void OnImportMsgChanged()
        {
            if(messageScript != view.MessageText)
                messageScript = view.MessageText;

            CheckForValidChars();
            view.ContainsGenderCode = HasGenderCode();
        }

        private void CheckForValidChars()
        {
            if (messageScript.StartsWith("MESS_"))
            {
                view.AllowImport = true;
                view.StatusText = "";
                return;
            }

            bool containsInvalids = false;
            List<char> inv = new List<char>();
            foreach(char c in messageScript.Where(c => !validChars[AssetGeneration.GetValue(c)]))
            {
                if (!containsInvalids)
                    containsInvalids = true;
                if (!inv.Contains(c))
                    inv.Add(c);
            }

            if (containsInvalids)
            {
                view.AllowImport = false;
                view.StatusText = string.Format("Warning! Text contains one or more unsupported characters: {0}", string.Join(",", inv));
            }
            else
            {
                view.AllowImport = true;
                view.StatusText = "";
            }
        }

        private bool HasGenderCode()
        {
            if(messageScript.Contains("VOICE_PLAYER"))
                return true;
            else
                return false;
        }

        public void SwapGenderCode()
        {
            if (view.MessageText.Contains("VOICE_PLAYER_M#"))
                view.MessageText = messageScript.Replace("VOICE_PLAYER_M#", "VOICE_PLAYER_F#");
            else
                view.MessageText = messageScript.Replace("VOICE_PLAYER_F#", "VOICE_PLAYER_M#");

            view.StatusText = "NOTE: Gender has been swapped in code, but words like \"Lord\" and \"Lady\" remain unchanged in dialogue.";
        }
    }
}
