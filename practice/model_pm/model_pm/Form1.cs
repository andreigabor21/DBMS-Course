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

namespace model_pm
{
    public partial class Form1 : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter daDevs, daTasks;
        DataSet ds;
        SqlCommandBuilder cb;
        BindingSource bsDevs, bsTasks;

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                daTasks.Update(ds, "Tasks");
                MessageBox.Show("Changes are saved");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string getDevsStringSql = "SELECT * FROM developers";
            string getTasksStringSql = "SELECT * FROM tasks";
            dbConn = new SqlConnection("Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                    "Initial Catalog = 'model_pmm';" +
                                    "Integrated Security = true;");
            ds = new DataSet();
            daDevs = new SqlDataAdapter(getDevsStringSql, dbConn);
            daTasks = new SqlDataAdapter(getTasksStringSql, dbConn);
            cb = new SqlCommandBuilder(daDevs);
            daDevs.Fill(ds, "Devs");
            daTasks.Fill(ds, "Tasks");

            DataRelation dr = new DataRelation("FK_Devs_Tasks",
                                                ds.Tables["Devs"].Columns["id"],
                                                ds.Tables["Tasks"].Columns["developer_id"]);
            ds.Relations.Add(dr);

            bsDevs = new BindingSource();
            bsDevs.DataSource = ds;
            bsDevs.DataMember = "Devs";

            bsTasks = new BindingSource();
            bsTasks.DataSource = bsDevs;
            bsTasks.DataMember = "FK_Devs_Tasks";

            dgvParent.DataSource = bsDevs;
            dgvChild.DataSource = bsTasks;
        }
    }
}
