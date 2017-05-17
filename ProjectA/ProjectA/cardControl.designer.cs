namespace CardControl
{
    partial class Card
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.name = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.Label();
            this.picture = new System.Windows.Forms.PictureBox();
            this.power = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.White;
            this.name.Location = new System.Drawing.Point(0, 50);
            this.name.Margin = new System.Windows.Forms.Padding(0);
            this.name.MinimumSize = new System.Drawing.Size(100, 10);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(100, 30);
            this.name.TabIndex = 2;
            this.name.Text = "name";
            this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.name.Click += new System.EventHandler(this.Card_Click);
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.SystemColors.ControlLight;
            this.text.Location = new System.Drawing.Point(0, 80);
            this.text.Margin = new System.Windows.Forms.Padding(0);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(100, 70);
            this.text.TabIndex = 3;
            this.text.Text = "imba";
            this.text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.text.Click += new System.EventHandler(this.Card_Click);
            // 
            // picture
            // 
            this.picture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picture.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picture.InitialImage = null;
            this.picture.Location = new System.Drawing.Point(50, 0);
            this.picture.Margin = new System.Windows.Forms.Padding(0);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(50, 50);
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.Click += new System.EventHandler(this.Card_Click);
            // 
            // power
            // 
            this.power.BackColor = System.Drawing.Color.AliceBlue;
            this.power.Location = new System.Drawing.Point(0, 0);
            this.power.Margin = new System.Windows.Forms.Padding(0);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(50, 50);
            this.power.TabIndex = 1;
            this.power.Text = "0";
            this.power.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.power.Click += new System.EventHandler(this.Card_Click);
            // 
            // Card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.power);
            this.Controls.Add(this.text);
            this.Controls.Add(this.name);
            this.Controls.Add(this.picture);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Card";
            this.Size = new System.Drawing.Size(102, 152);
            this.Click += new System.EventHandler(this.Card_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label power;
    }
}
