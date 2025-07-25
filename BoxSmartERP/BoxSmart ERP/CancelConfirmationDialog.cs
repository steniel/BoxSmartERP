using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSmart_ERP
{
    public partial class CancelConfirmationDialog : UserControl
    {
        // Event to notify the parent form of the dialog result
        public event EventHandler<DialogResultEventArgs> DialogClosed;

        // Property to set the question text
        public string Question
        {
            get { return lblQuestion.Text; }
            set { lblQuestion.Text = value; }
        }
        public CancelConfirmationDialog()
        {
            InitializeComponent();
            btnYes.Enabled = false; 
            txtConfirmation.PlaceholderText = "Type 'Yes cancel this request' to enable Yes"; 
        }

        // Helper method to raise the DialogClosed event
        protected virtual void OnDialogClosed(DialogResult result)
        {
            DialogClosed?.Invoke(this, new DialogResultEventArgs(result));
        }

        private void txtConfirmation_TextChanged(object sender, EventArgs e)
        {
            btnYes.Enabled = txtConfirmation.Text.Trim().Equals("Yes cancel this request", StringComparison.OrdinalIgnoreCase);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            OnDialogClosed(DialogResult.No);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            OnDialogClosed(DialogResult.Yes);
        }
    }
    // Custom EventArgs class to pass DialogResult
    public class DialogResultEventArgs : EventArgs
    {
        public DialogResult Result { get; }

        public DialogResultEventArgs(DialogResult result)
        {
            Result = result;
        }
    }
}
