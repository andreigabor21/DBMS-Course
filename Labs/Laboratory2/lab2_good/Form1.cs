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
using System.Configuration;
using System.Diagnostics;

namespace lab2_good
{
    public partial class Form1 : Form
    {
        SqlConnection dbConnection;
        SqlDataAdapter dataAdapterChild, dataAdapterParent;
        DataSet dataSet;
        SqlCommandBuilder commandBuilder;
        BindingSource bindingSourceChild, bindingSourceParent;

        private string getParentTable()
        {
            return ConfigurationManager.AppSettings.Get("parentTable");
        }
        private string getParentTablePrimaryKey()
        {
            return ConfigurationManager.AppSettings.Get("parentTablePrimaryKey");
        }
        private string getChildTable()
        {
            return ConfigurationManager.AppSettings.Get("childTable");
        }
        private string getChildTableForeignKey()
        {
            return ConfigurationManager.AppSettings.Get("childTableForeignKey");
        }
        private string getParentQuery()
        {
            return ConfigurationManager.AppSettings.Get("parentQuery");
        }
        private string getChildQuery()
        {
            return ConfigurationManager.AppSettings.Get("childQuery");
        }
  
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataAdapterChild.Update(dataSet, getChildTable());
                MessageBox.Show("Changes are saved");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create the connection
            dbConnection = new SqlConnection("Data Source = DESKTOP-FGTU247\\SQLEXPRESS;Initial Catalog = BookLibrary;Integrated Security = SSPI;");
            dbConnection.Open();
            dataSet = new DataSet(); //typed dataset that contains tables

            //initialize data from parent
            //data tables are filled with data when executing data adapter queries/commands
            dataAdapterParent = new SqlDataAdapter(getParentQuery(), dbConnection);
            dataAdapterParent.Fill(dataSet, getParentTable());

            //initialize data from child
            dataAdapterChild = new SqlDataAdapter(getChildQuery(), dbConnection);
            commandBuilder = new SqlCommandBuilder(dataAdapterChild);
            dataAdapterChild.Fill(dataSet, getChildTable());

            //DataRelation is created to describe the relationships among the dataset’s tables
            //A foreign key constraint is automatically added when creating a DataRelation object in a dataset
            DataRelation relation = new DataRelation("FK_parent_child",
                                               dataSet.Tables[getParentTable()].Columns[getParentTablePrimaryKey()],
                                               dataSet.Tables[getChildTable()].Columns[getChildTableForeignKey()]);
            dataSet.Relations.Add(relation);

            //binding the controls on the form to the table in the dataset
            bindingSourceParent = new BindingSource();
            bindingSourceParent.DataSource = dataSet;
            bindingSourceParent.DataMember = getParentTable();

            bindingSourceChild = new BindingSource();
            bindingSourceChild.DataSource = bindingSourceParent;
            bindingSourceChild.DataMember = "FK_parent_child";

            dgvParent.DataSource = bindingSourceParent;
            dgvChild.DataSource = bindingSourceChild;
        }


        public Form1()
        {
            InitializeComponent();
        }
    }
}
