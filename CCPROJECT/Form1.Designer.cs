namespace CCPROJECT
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.NewFileButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtBox = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Syntax = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Syntax);
            this.groupBox1.Controls.Add(this.OpenFileButton);
            this.groupBox1.Controls.Add(this.AnalyzeButton);
            this.groupBox1.Controls.Add(this.SaveFileButton);
            this.groupBox1.Controls.Add(this.NewFileButton);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(769, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Image = global::CCPROJECT.Properties.Resources.open;
            this.OpenFileButton.Location = new System.Drawing.Point(6, 11);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(47, 46);
            this.OpenFileButton.TabIndex = 3;
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.Image = global::CCPROJECT.Properties.Resources.images;
            this.AnalyzeButton.Location = new System.Drawing.Point(165, 11);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(47, 46);
            this.AnalyzeButton.TabIndex = 2;
            this.AnalyzeButton.UseVisualStyleBackColor = true;
            this.AnalyzeButton.Click += new System.EventHandler(this.AnalyzeButton_Click_1);
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.Image = global::CCPROJECT.Properties.Resources.Save__Copy_;
            this.SaveFileButton.Location = new System.Drawing.Point(112, 11);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(47, 46);
            this.SaveFileButton.TabIndex = 1;
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // NewFileButton
            // 
            this.NewFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NewFileButton.Image = global::CCPROJECT.Properties.Resources.NewFile__Copy_;
            this.NewFileButton.Location = new System.Drawing.Point(59, 11);
            this.NewFileButton.Name = "NewFileButton";
            this.NewFileButton.Size = new System.Drawing.Size(47, 46);
            this.NewFileButton.TabIndex = 0;
            this.NewFileButton.UseVisualStyleBackColor = true;
            this.NewFileButton.Click += new System.EventHandler(this.NewFileButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtBox);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(9, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 472);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editor";
            // 
            // rtBox
            // 
            this.rtBox.Location = new System.Drawing.Point(6, 17);
            this.rtBox.Name = "rtBox";
            this.rtBox.Size = new System.Drawing.Size(556, 368);
            this.rtBox.TabIndex = 2;
            this.rtBox.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(584, 17);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(173, 368);
            this.listBox1.TabIndex = 1;
            // 
            // Syntax
            // 
            this.Syntax.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Syntax.Location = new System.Drawing.Point(218, 11);
            this.Syntax.Name = "Syntax";
            this.Syntax.Size = new System.Drawing.Size(47, 46);
            this.Syntax.TabIndex = 4;
            this.Syntax.Text = "S";
            this.Syntax.UseVisualStyleBackColor = true;
            this.Syntax.Click += new System.EventHandler(this.Syntax_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(788, 572);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CC Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button NewFileButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.RichTextBox rtBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Syntax;
    
    }
}

