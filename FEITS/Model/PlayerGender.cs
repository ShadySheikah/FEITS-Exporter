using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FEITS.Model
{
    public enum PlayerGender : sbyte
    {
        None = -1,
        Male = 0,
        Female = 1
    }

    public static class PlayerGendersExtensions
    {
        private const string Male = "マイユニ男1",
                             Female = "マイユニ女2",
                             MaleUnderscore = "マイユニ_男1",
                             FemaleUnderscore = "マイユニ_女2";

        public static string ToIfString(this PlayerGender @this, bool underscore = false)
        {
            switch (@this)
            {
                case PlayerGender.Male:
                    return underscore
                        ? MaleUnderscore
                        : Male;
                case PlayerGender.Female:
                    return underscore
                        ? FemaleUnderscore
                        : Female;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@this), @this, null);
            }
        }

        public static PlayerGender FromIfString(string value)
        {
            switch (value)
            {
                case Male:
                case MaleUnderscore:
                    return PlayerGender.Male;
                case Female:
                case FemaleUnderscore:
                    return PlayerGender.Female;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static PlayerGender GetGender(this ToolStripMenuItem @this)
        {
            for (var i = 0; i < @this.DropDownItems.Count; i++)
            {
                var mi = (ToolStripMenuItem) @this.DropDownItems[i];
                if (!mi.Checked) continue;
                switch (i)
                {
                    case (int) PlayerGender.Male:
                        return PlayerGender.Male;
                    case (int) PlayerGender.Female:
                        return PlayerGender.Female;
                    default:
                        throw new IndexOutOfRangeException($"Unexpected gender index {i}");
                }
            }
            return PlayerGender.None;
        }

        public static void SetGender(this ToolStripMenuItem @this, PlayerGender value)
        {
            ((ToolStripMenuItem) @this.DropDownItems[(int) value]).Checked = true;
        }
    }
}