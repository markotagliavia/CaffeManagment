using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CaffeManagment.View
{
    public class DigetTextBox : TextBox
    {
        private static readonly Regex regex = new Regex(@"^\d*(\.\d{0,8})?$");
        public DigetTextBox() { }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!regex.IsMatch(e.Text))
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }
    }
}
