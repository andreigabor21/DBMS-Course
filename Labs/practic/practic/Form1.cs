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

namespace practic
{
    public partial class Form1 : Form
    {
        SqlConnection dbConnection;
        SqlDataAdapter dataAdapterChild, dataAdapterParent;
        DataSet dataSet;
        SqlCommandBuilder commandBuilder;
        BindingSource bindingSourceChild, bindingSourceParent;
        public Form1()
        {
            InitializeComponent();
        }
        private string getParentTable()
        {
            return "Task_Types";
        }
        private string getParentTablePrimaryKey()
        {
            return "id";
        }
        private string getChildTable()
        {
            return "Tasks";
        }
        private string getChildTableForeignKey()
        {
            return "task_type_id";
        }
        private string getParentQuery()
        {
            return "SELECT * FROM Task_Types";
        }
        private string getChildQuery()
        {
            return "SELECT * FROM Tasks";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create the connection
            dbConnection = new SqlConnection("Data Source = DESKTOP-FGTU247\\SQLEXPRESS;Initial Catalog = practic;Integrated Security = SSPI;");
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

            dgvTaskTypes.DataSource = bindingSourceParent;
            dgvTasks.DataSource = bindingSourceChild;
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
