namespace BoxSmart_ERP
{
    partial class ConfirmationDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cancelConfirmationDialog1 = new CancelConfirmationDialog();
            SuspendLayout();
            // 
            // cancelConfirmationDialog1
            // 
            cancelConfirmationDialog1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cancelConfirmationDialog1.Location = new Point(3, -2);
            cancelConfirmationDialog1.Name = "cancelConfirmationDialog1";
            cancelConfirmationDialog1.Question = "Are you sure? Type CANCEL to proceed.";
            cancelConfirmationDialog1.Size = new Size(401, 218);
            cancelConfirmationDialog1.TabIndex = 0;
            // 
            // ConfirmationDialogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 219);
            Controls.Add(cancelConfirmationDialog1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfirmationDialogForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Confirm Cancel Action";
            ResumeLayout(false);
        }

        #endregion

        private CancelConfirmationDialog cancelConfirmationDialog1;
    }
}