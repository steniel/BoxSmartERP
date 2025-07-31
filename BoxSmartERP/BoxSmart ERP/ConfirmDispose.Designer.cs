namespace BoxSmart_ERP
{
    partial class ConfirmDispose
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
            cancelConfirmationDialog1.Location = new Point(22, 26);
            cancelConfirmationDialog1.Name = "cancelConfirmationDialog1";
            cancelConfirmationDialog1.Question = "";
            cancelConfirmationDialog1.Size = new Size(510, 217);
            cancelConfirmationDialog1.TabIndex = 0;
            // 
            // ConfirmDispose
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 292);
            Controls.Add(cancelConfirmationDialog1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ConfirmDispose";
            Text = "ConfirmDispose";
            ResumeLayout(false);
        }

        #endregion

        private CancelConfirmationDialog cancelConfirmationDialog1;
    }
}