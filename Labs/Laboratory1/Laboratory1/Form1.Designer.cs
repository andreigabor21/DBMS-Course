
namespace Laboratory1
{
    partial class Books
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
            this.BooksGridView = new System.Windows.Forms.DataGridView();
            this.votesDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BooksGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.votesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BooksGridView
            // 
            this.BooksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BooksGridView.Location = new System.Drawing.Point(77, 42);
            this.BooksGridView.Name = "BooksGridView";
            this.BooksGridView.RowHeadersWidth = 51;
            this.BooksGridView.RowTemplate.Height = 24;
            this.BooksGridView.Size = new System.Drawing.Size(1259, 528);
            this.BooksGridView.TabIndex = 0;
            this.BooksGridView.SelectionChanged += new System.EventHandler(this.clientsGridView_SelectionChanged);
            // 
            // votesDataGridView
            // 
            this.votesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.votesDataGridView.Location = new System.Drawing.Point(77, 594);
            this.votesDataGridView.Name = "votesDataGridView";
            this.votesDataGridView.RowHeadersWidth = 51;
            this.votesDataGridView.RowTemplate.Height = 24;
            this.votesDataGridView.Size = new System.Drawing.Size(1259, 277);
            this.votesDataGridView.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Books:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 573);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Votes:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1375, 548);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(134, 66);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Books
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1538, 895);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.votesDataGridView);
            this.Controls.Add(this.BooksGridView);
            this.Name = "Books";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BooksGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.votesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView BooksGridView;
        private System.Windows.Forms.DataGridView votesDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveButton;
    }
}

