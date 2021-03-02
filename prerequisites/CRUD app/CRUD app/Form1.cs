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

namespace CRUD_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-FGTU247\\SQLEXPRESS;Initial Catalog=employee_detail;User Id=;Password=;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                if (txtempid.Text == "")
                {
                    MessageBox.Show("Enter Employee Id");
                    txtempid.Focus();
                }
                else if (txtempname.Text == "")
                {
                    MessageBox.Show("Enter Employee Name");
                    txtempname.Focus();
                }
                else if (txtsalary.Text == "")
                {
                    MessageBox.Show("Enter Salary");
                    txtsalary.Focus();
                }
                else //we can add
                {
                    string query = "INSERT INTO tbl_empdetail (empid, empname, salary) VALUES (@emp, @emp_name, @emp_salary)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@emp", txtempid.Text);
                    cmd.Parameters.AddWithValue("@emp_name", txtempname.Text);
                    cmd.Parameters.AddWithValue("@emp_salary", txtsalary.Text);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (i > 0)
                    {
                        MessageBox.Show("Record inserted succesfully!");
                        txtempid.Text = "";
                        txtempname.Text = "";
                        txtsalary.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Record not inserted");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                loadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtempid.Text == "")
                {
                    MessageBox.Show("Enter Employee Id");
                    txtempid.Focus();
                }
                else if (txtempname.Text == "")
                {
                    MessageBox.Show("Enter Employee Name");
                    txtempname.Focus();
                }
                else if (txtsalary.Text == "")
                {
                    MessageBox.Show("Enter Salary");
                    txtsalary.Focus();
                }
                else //we can add
                {
                    string query = "UPDATE tbl_empdetail SET empname=@name, salary=@salary WHERE empid=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtempid.Text);
                    cmd.Parameters.AddWithValue("@name", txtempname.Text);
                    cmd.Parameters.AddWithValue("@salary", txtsalary.Text);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (i > 0)
                    {
                        MessageBox.Show("Record Updated succesfully!");
                        txtempid.Text = "";
                        txtempname.Text = "";
                        txtsalary.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Record not updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtempid.Text == "")
                {
                    MessageBox.Show("Enter Employee Id");
                    txtempid.Focus();
                }
                else //we can add
                {
                    string query = "DELETE FROM tbl_empdetail WHERE empid=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", txtempid.Text);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (i > 0)
                    {
                        MessageBox.Show("Record Deleted succesfully!");
                        txtempid.Text = "";
                        txtempname.Text = "";
                        txtsalary.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Record not deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                loadData();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string query = "SELECT * FROM tbl_empdetail";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "employee");
            dataGridView1.DataSource = dataSet.Tables["employee"];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            string query = "SELECT * FROM tbl_empdetail";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "employee");
            dataGridView1.DataSource = dataSet.Tables["employee"];
        }
    }
}
