//Based on code posted to Stack Overflow:
//https://stackoverflow.com/questions/4024798/trying-to-use-the-c-sharp-spellcheck-class/4026132#4026132

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;

namespace FEITS.View
{
    [Designer(typeof(ControlDesigner))]
    //[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    class SpellBox : ElementHost
    {
        public SpellBox()
        {
            box = new TextBox();
            box.TextChanged += (s, e) => OnTextChanged(EventArgs.Empty);
            box.SpellCheck.IsEnabled = true;
            box.TextWrapping = TextWrapping.Wrap;
            box.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Size = new System.Drawing.Size(100, 20);
        }

        public void InitializeChild()
        {
            base.Child = box;
        }

        public override string Text
        {
            get { return box.Text; }
            set { box.Text = value; }
        }

        public void ClearUndo()
        {
            box.IsUndoEnabled = false;
            box.IsUndoEnabled = true;
        }

        [DefaultValue(true)]
        public bool SpellCheckEnabled
        {
            get { return box.SpellCheck.IsEnabled; }
            set { box.SpellCheck.IsEnabled = value; }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get { return box.AcceptsReturn; }
            set { box.AcceptsReturn = value; }
        }

        [DefaultValue(true)]
        public bool WordWrap
        {
            get { return box.TextWrapping != TextWrapping.NoWrap; }
            set { box.TextWrapping = value ? TextWrapping.Wrap : TextWrapping.NoWrap; }
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return box.IsReadOnly; }
            set { box.IsReadOnly = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.UIElement Child
        {
            get { return base.Child; }
            set { /* Do nothing to solve a problem with the serializer !! */ }
        }

        private TextBox box;
    }
}