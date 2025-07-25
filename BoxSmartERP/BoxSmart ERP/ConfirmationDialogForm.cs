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
    public partial class ConfirmationDialogForm : Form
    {
        // Expose a public property to set the question text on the user control
        public string Question
        {
            get { return cancelConfirmationDialog1.Question; }
            set { cancelConfirmationDialog1.Question = value; }
        }

        public ConfirmationDialogForm()
        {
            InitializeComponent();
            // Subscribe to the DialogClosed event of the embedded User Control
            cancelConfirmationDialog1.DialogClosed += ConfirmationDialogControl_DialogClosed;
        }

        private void ConfirmationDialogControl_DialogClosed(object sender, DialogResultEventArgs e)
        {
            // Set the DialogResult of this modal form based on the user control's result
            this.DialogResult = e.Result;
            this.Close(); // Close the modal form
        }

        private void ConfirmationDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the dialog result hasn't been set by the buttons, assume Cancel
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

    }
}
