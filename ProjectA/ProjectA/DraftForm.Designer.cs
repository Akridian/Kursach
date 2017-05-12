namespace ProjectA
{
    partial class DraftForm
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
            this.takeButton = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.cardsPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // takeButton
            // 
            this.takeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.takeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.takeButton.Location = new System.Drawing.Point(325, 400);
            this.takeButton.Name = "takeButton";
            this.takeButton.Size = new System.Drawing.Size(150, 50);
            this.takeButton.TabIndex = 3;
            this.takeButton.Text = "TAKE";
            this.takeButton.UseVisualStyleBackColor = true;
            this.takeButton.Click += new System.EventHandler(this.takeButton_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label.Location = new System.Drawing.Point(71, 58);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(175, 22);
            this.label.TabIndex = 4;
            this.label.Text = "►Choose one card";
            // 
            // cardsPanel
            // 
            this.cardsPanel.Location = new System.Drawing.Point(0, 150);
            this.cardsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Size = new System.Drawing.Size(800, 152);
            this.cardsPanel.TabIndex = 5;
            // 
            // DraftForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(798, 598);
            this.ControlBox = false;
            this.Controls.Add(this.cardsPanel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.takeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DraftForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DraftForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button takeButton;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel cardsPanel;
    }
}