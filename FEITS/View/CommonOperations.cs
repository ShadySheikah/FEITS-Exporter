using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace FEITS.View
{
    public static class CommonOperations
    {
        public static void SetEnabledAndUpdateDialogResult(this Button @this, bool value)
        {
            @this.Enabled = value;

            @this.DialogResult = value ? DialogResult.OK : DialogResult.None;
        }
    }
}