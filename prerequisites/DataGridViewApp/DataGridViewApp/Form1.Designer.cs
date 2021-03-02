
namespace DataGridViewApp
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
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.txtEmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxPosition = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtOffice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtEmployeeID,
            this.txtName,
            this.cbxPosition,
            this.txtOffice,
            this.txtAge});
            this.dgvEmployee.Location = new System.Drawing.Point(52, 12);
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.RowHeadersWidth = 51;
            this.dgvEmployee.RowTemplate.Height = 24;
            this.dgvEmployee.Size = new System.Drawing.Size(766, 483);
            this.dgvEmployee.TabIndex = 0;
            this.dgvEmployee.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellContentClick);
            this.dgvEmployee.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellValueChanged);
            this.dgvEmployee.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEmployee_EditingControlShowing);
            this.dgvEmployee.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvEmployee_UserDeletingRow);
            // 
            // txtEmployeeID
            // 
            this.txtEmployeeID.DataPropertyName = "EmployeeID";
            this.txtEmployeeID.HeaderText = "EmployeeId";
            this.txtEmployeeID.MinimumWidth = 6;
            this.txtEmployeeID.Name = "txtEmployeeID";
            this.txtEmployeeID.Visible = false;
            this.txtEmployeeID.Width = 125;
            // 
            // txtName
            // 
            this.txtName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtName.DataPropertyName = "Name";
            this.txtName.HeaderText = "Name";
            this.txtName.MinimumWidth = 6;
            this.txtName.Name = "txtName";
            // 
            // cbxPosition
            // 
            this.cbxPosition.DataPropertyName = "PositionID";
            this.cbxPosition.HeaderText = "Position";
            this.cbxPosition.MinimumWidth = 6;
            this.cbxPosition.Name = "cbxPosition";
            this.cbxPosition.Width = 125;
            // 
            // txtOffice
            // 
            this.txtOffice.DataPropertyName = "Office";
            this.txtOffice.HeaderText = "Office";
            this.txtOffice.MinimumWidth = 6;
            this.txtOffice.Name = "txtOffice";
            this.txtOffice.Width = 125;
            // 
            // txtAge
            // 
            this.txtAge.DataPropertyName = "Age";
            this.txtAge.HeaderText = "Age";
            this.txtAge.MinimumWidth = 6;
            this.txtAge.Name = "txtAge";
            this.txtAge.Width = 125;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 523);
            this.Controls.Add(this.dgvEmployee);
            this.Name = "Form1";
            this.Text = "Insert Update Delete in C# DataGridView";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtEmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtName;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbxPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtOffice;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtAge;
    }
}

