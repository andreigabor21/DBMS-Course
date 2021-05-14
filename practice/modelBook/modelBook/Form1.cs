using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace modelBook
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private SqlDataAdapter daParent, daChild;
        private DataRelation dataRelation;
        private BindingSource bsParent, bsChild;
        private string connString = "Data Source=DESKTOP-FGTU247\\SQLEXPRESS;Initial Catalog=modelBook;Integrated Security=true";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.sqlConnection = new SqlConnection(connString);
            this.dataSet = new DataSet();

            //load parent table data into dataset
            this.daParent = new SqlDataAdapter("SELECT * FROM publishers", sqlConnection);
            SqlCommandBuilder builder = new SqlCommandBuilder(daParent);
            this.daParent.Fill(dataSet, "publishers");

            this.daChild = new SqlDataAdapter("SELECT * FROM books", sqlConnection);
            builder = new SqlCommandBuilder(daChild);
            this.daChild.Fill(dataSet, "books");

            //create a new relation representing the foreign key of 1:n relation
            DataRelation rel = new DataRelation("fk_books_publishers", dataSet.Tables["publishers"].Columns["id"],
                dataSet.Tables["books"].Columns["publisher_id"]);
            this.dataSet.Relations.Add(rel);

            //create the binding source for the parent to child
            bsParent = new BindingSource();
            bsParent.DataSource = dataSet;
            bsParent.DataMember = "publishers";

            bsChild = new BindingSource();
            bsChild.DataSource = bsParent;
            bsChild.DataMember = "fk_books_publishers";

            //fill the grids
            this.dgvPublishers.DataSource = bsParent;
            this.dgvBooks.DataSource = bsChild;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.daChild.Update(this.dataSet, "books");
            this.daParent.Update(this.dataSet, "publishers");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
