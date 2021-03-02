using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataGridViewApp
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                    "Initial Catalog = 'DGVDB';" +
                                    "Integrated Security = true;";
        public Form1()
        {
            InitializeComponent();
        }

        void PopulatePositionComboBox()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Position", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                cbxPosition.ValueMember = "PositionID";
                cbxPosition.DisplayMember = "Position";
                DataRow topItem = dtbl.NewRow();
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                cbxPosition.DataSource = dtbl;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePositionComboBox();
            PopulateDataGridView();
        }

        void PopulateDataGridView()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Employee", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgvEmployee.DataSource = dtbl;
            }
        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvEmployee_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvEmployee.CurrentCell != null)
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    DataGridViewRow dgvRow = dgvEmployee.CurrentRow;
                    SqlCommand sqlCmd = new SqlCommand("EmployeeAddOrEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    if(dgvRow.Cells["txtEmployeeID"].Value == DBNull.Value) //Insert
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", 0);
                    else //update 
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(dgvRow.Cells["txtEmployeeID"].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@Name", dgvRow.Cells["txtName"].Value == DBNull.Value ? "" : dgvRow.Cells["txtName"].Value.ToString());
                    sqlCmd.Parameters.AddWithValue("@PositionId", Convert.ToInt32(dgvRow.Cells["cbxPosition"].Value == DBNull.Value ? "0" : dgvRow.Cells["cbxPosition"].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@Office", dgvRow.Cells["txtOffice"].Value == DBNull.Value ? "" : dgvRow.Cells["txtOffice"].Value.ToString());
                    sqlCmd.Parameters.AddWithValue("@Age", Convert.ToInt32(dgvRow.Cells["txtAge"].Value == DBNull.Value ? "0" : dgvRow.Cells["txtAge"].Value.ToString()));

                    sqlCmd.ExecuteNonQuery();
                    PopulateDataGridView();
                }
            }
        }

        private void dgvEmployee_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(dgvEmployee.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyPress += AllowNumbersOnly;
            }
        }

        private void AllowNumbersOnly(Object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void dgvEmployee_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if(dgvEmployee.CurrentRow.Cells["txtEmployeeID"].Value != DBNull.Value)
            {
                if(MessageBox.Show("Are you sure to delete?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("EmployeeDeleteByID", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(dgvEmployee.CurrentRow.Cells["txtEmployeeID"].Value.ToString()));
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
