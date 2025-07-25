namespace BoxSmart_ERP
{
    partial class CancelConfirmationDialog
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtConfirmation = new TextBox();
            btnYes = new Button();
            btnNo = new Button();
            label1 = new Label();
            lblQuestion = new TextBox();
            SuspendLayout();
            // 
            // txtConfirmation
            // 
            txtConfirmation.Location = new Point(3, 95);
            txtConfirmation.Name = "txtConfirmation";
            txtConfirmation.PlaceholderText = "Type 'Yes cancel this request' to enable Yes";
            txtConfirmation.Size = new Size(387, 25);
            txtConfirmation.TabIndex = 0;
            txtConfirmation.TextChanged += txtConfirmation_TextChanged;
            // 
            // btnYes
            // 
            btnYes.Enabled = false;
            btnYes.Location = new Point(286, 154);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(104, 38);
            btnYes.TabIndex = 2;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            btnNo.Location = new Point(3, 154);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(104, 38);
            btnNo.TabIndex = 2;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 123);
            label1.Name = "label1";
            label1.Size = new Size(248, 17);
            label1.TabIndex = 1;
            label1.Text = "Type: 'Yes cancel this request' to proceed";
            // 
            // lblQuestion
            // 
            lblQuestion.BackColor = SystemColors.Control;
            lblQuestion.BorderStyle = BorderStyle.None;
            lblQuestion.Location = new Point(3, 19);
            lblQuestion.Multiline = true;
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(387, 70);
            lblQuestion.TabIndex = 3;
            lblQuestion.TextAlign = HorizontalAlignment.Center;
            // 
            // CancelConfirmationDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblQuestion);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(label1);
            Controls.Add(txtConfirmation);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "CancelConfirmationDialog";
            Size = new Size(393, 217);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtConfirmation;
        private Button btnYes;
        private Button btnNo;
        private Label label1;
        private TextBox lblQuestion;
    }
}
