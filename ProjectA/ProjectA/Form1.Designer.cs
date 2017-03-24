namespace ProjectA
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // enemyRange
            // 
            this.enemyRange.Location = new System.Drawing.Point(112, 12);
            this.enemyRange.Name = "enemyRange";
            this.enemyRange.Size = new System.Drawing.Size(976, 170);
            this.enemyRange.TabIndex = 0;
            this.enemyRange.TabStop = false;
            // 
            // enemyMelee
            // 
            this.enemyMelee.AutoSize = true;
            this.enemyMelee.Location = new System.Drawing.Point(112, 188);
            this.enemyMelee.Name = "enemyMelee";
            this.enemyMelee.Size = new System.Drawing.Size(976, 170);
            this.enemyMelee.TabIndex = 1;
            this.enemyMelee.TabStop = false;
            // 
            // yourMelee
            // 
            this.yourMelee.AutoSize = true;
            this.yourMelee.Location = new System.Drawing.Point(112, 383);
            this.yourMelee.Name = "yourMelee";
            this.yourMelee.Size = new System.Drawing.Size(976, 170);
            this.yourMelee.TabIndex = 2;
            this.yourMelee.TabStop = false;
            // 
            // yourRange
            // 
            this.yourRange.AutoSize = true;
            this.yourRange.Location = new System.Drawing.Point(112, 559);
            this.yourRange.Name = "yourRange";
            this.yourRange.Size = new System.Drawing.Size(976, 170);
            this.yourRange.TabIndex = 3;
            this.yourRange.TabStop = false;
            // 
            // hand
            // 
            this.hand.AutoSize = true;
            this.hand.Location = new System.Drawing.Point(112, 807);
            this.hand.Name = "hand";
            this.hand.Size = new System.Drawing.Size(976, 170);
            this.hand.TabIndex = 4;
            this.hand.TabStop = false;
            // 
            // playCard
            // 
            this.playCard.Location = new System.Drawing.Point(511, 766);
            this.playCard.Name = "playCard";
            this.playCard.Size = new System.Drawing.Size(75, 23);
            this.playCard.TabIndex = 5;
            this.playCard.Text = "play";
            this.playCard.UseVisualStyleBackColor = true;
            this.playCard.Click += new System.EventHandler(this.playCard_Click);
            // 
            // yourScore
            // 
            this.yourScore.AutoSize = true;
            this.yourScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yourScore.Location = new System.Drawing.Point(12, 538);
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
            this.enemyScore.Location = new System.Drawing.Point(12, 169);
            this.enemyScore.Name = "enemyScore";
            this.enemyScore.Size = new System.Drawing.Size(35, 37);
            this.enemyScore.TabIndex = 7;
            this.enemyScore.Text = "0";
            this.enemyScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1189, 992);
            this.Controls.Add(this.enemyScore);
            this.Controls.Add(this.yourScore);
            this.Controls.Add(this.playCard);
            this.Controls.Add(this.hand);
            this.Controls.Add(this.yourRange);
            this.Controls.Add(this.yourMelee);
            this.Controls.Add(this.enemyMelee);
            this.Controls.Add(this.enemyRange);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

