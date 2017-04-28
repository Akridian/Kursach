namespace ProjectA
{
    partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.enemyRange = new System.Windows.Forms.GroupBox();
            this.enemyMelee = new System.Windows.Forms.GroupBox();
            this.yourMelee = new System.Windows.Forms.GroupBox();
            this.yourRange = new System.Windows.Forms.GroupBox();
            this.hand = new System.Windows.Forms.GroupBox();
            this.playCard = new System.Windows.Forms.Button();
            this.yourScore = new System.Windows.Forms.Label();
            this.enemyScore = new System.Windows.Forms.Label();
            this.enemyRound = new System.Windows.Forms.PictureBox();
            this.yourRound = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.enemyRound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yourRound)).BeginInit();
            this.SuspendLayout();
            // 
            // enemyRange
            // 
            this.enemyRange.BackColor = System.Drawing.SystemColors.ControlLight;
            this.enemyRange.Location = new System.Drawing.Point(112, 12);
            this.enemyRange.Name = "enemyRange";
            this.enemyRange.Padding = new System.Windows.Forms.Padding(0);
            this.enemyRange.Size = new System.Drawing.Size(1065, 180);
            this.enemyRange.TabIndex = 0;
            this.enemyRange.TabStop = false;
            // 
            // enemyMelee
            // 
            this.enemyMelee.AutoSize = true;
            this.enemyMelee.BackColor = System.Drawing.SystemColors.ControlLight;
            this.enemyMelee.Location = new System.Drawing.Point(112, 198);
            this.enemyMelee.Name = "enemyMelee";
            this.enemyMelee.Padding = new System.Windows.Forms.Padding(0);
            this.enemyMelee.Size = new System.Drawing.Size(1065, 180);
            this.enemyMelee.TabIndex = 1;
            this.enemyMelee.TabStop = false;
            // 
            // yourMelee
            // 
            this.yourMelee.AutoSize = true;
            this.yourMelee.BackColor = System.Drawing.SystemColors.ControlLight;
            this.yourMelee.Location = new System.Drawing.Point(112, 409);
            this.yourMelee.Name = "yourMelee";
            this.yourMelee.Padding = new System.Windows.Forms.Padding(0);
            this.yourMelee.Size = new System.Drawing.Size(1065, 180);
            this.yourMelee.TabIndex = 2;
            this.yourMelee.TabStop = false;
            // 
            // yourRange
            // 
            this.yourRange.AutoSize = true;
            this.yourRange.BackColor = System.Drawing.SystemColors.ControlLight;
            this.yourRange.Location = new System.Drawing.Point(112, 595);
            this.yourRange.Name = "yourRange";
            this.yourRange.Padding = new System.Windows.Forms.Padding(0);
            this.yourRange.Size = new System.Drawing.Size(1065, 180);
            this.yourRange.TabIndex = 3;
            this.yourRange.TabStop = false;
            // 
            // hand
            // 
            this.hand.AutoSize = true;
            this.hand.BackColor = System.Drawing.SystemColors.ControlLight;
            this.hand.Location = new System.Drawing.Point(112, 887);
            this.hand.Name = "hand";
            this.hand.Padding = new System.Windows.Forms.Padding(0);
            this.hand.Size = new System.Drawing.Size(1065, 180);
            this.hand.TabIndex = 4;
            this.hand.TabStop = false;
            // 
            // playCard
            // 
            this.playCard.Enabled = false;
            this.playCard.Font = new System.Drawing.Font("Roboto Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playCard.Location = new System.Drawing.Point(601, 801);
            this.playCard.Name = "playCard";
            this.playCard.Size = new System.Drawing.Size(127, 57);
            this.playCard.TabIndex = 5;
            this.playCard.Text = "play";
            this.playCard.UseVisualStyleBackColor = true;
            this.playCard.Click += new System.EventHandler(this.playCard_Click);
            // 
            // yourScore
            // 
            this.yourScore.AutoSize = true;
            this.yourScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yourScore.Location = new System.Drawing.Point(12, 573);
            this.yourScore.Name = "yourScore";
            this.yourScore.Size = new System.Drawing.Size(35, 37);
            this.yourScore.TabIndex = 6;
            this.yourScore.Text = "0";
            this.yourScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enemyScore
            // 
            this.enemyScore.AutoSize = true;
            this.enemyScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enemyScore.Location = new System.Drawing.Point(12, 179);
            this.enemyScore.Name = "enemyScore";
            this.enemyScore.Size = new System.Drawing.Size(35, 37);
            this.enemyScore.TabIndex = 7;
            this.enemyScore.Text = "0";
            this.enemyScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enemyRound
            // 
            this.enemyRound.Location = new System.Drawing.Point(19, 267);
            this.enemyRound.Name = "enemyRound";
            this.enemyRound.Size = new System.Drawing.Size(50, 50);
            this.enemyRound.TabIndex = 8;
            this.enemyRound.TabStop = false;
            this.enemyRound.Visible = false;
            // 
            // yourRound
            // 
            this.yourRound.Location = new System.Drawing.Point(19, 467);
            this.yourRound.Name = "yourRound";
            this.yourRound.Size = new System.Drawing.Size(50, 50);
            this.yourRound.TabIndex = 9;
            this.yourRound.TabStop = false;
            this.yourRound.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 1080);
            this.Controls.Add(this.yourRound);
            this.Controls.Add(this.enemyRound);
            this.Controls.Add(this.enemyScore);
            this.Controls.Add(this.yourScore);
            this.Controls.Add(this.playCard);
            this.Controls.Add(this.hand);
            this.Controls.Add(this.yourRange);
            this.Controls.Add(this.yourMelee);
            this.Controls.Add(this.enemyMelee);
            this.Controls.Add(this.enemyRange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 1030);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.enemyRound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yourRound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox enemyRange;
        private System.Windows.Forms.GroupBox enemyMelee;
        private System.Windows.Forms.GroupBox yourMelee;
        private System.Windows.Forms.GroupBox yourRange;
        private System.Windows.Forms.GroupBox hand;
        private System.Windows.Forms.Button playCard;
        private System.Windows.Forms.Label yourScore;
        private System.Windows.Forms.Label enemyScore;
        private System.Windows.Forms.PictureBox enemyRound;
        private System.Windows.Forms.PictureBox yourRound;
    }
}

