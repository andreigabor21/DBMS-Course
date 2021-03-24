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

namespace Laboratory1
{
    public partial class Books : Form
    {
        private static string connectionString = "Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                    "Initial Catalog = 'BookLibrary';" +
                                    "Integrated Security = true;";
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        private DataSet booksDataSet;
        private DataSet votesDataSet;
        private SqlDataAdapter booksDataAdapter;
        private SqlDataAdapter votesDataAdapter;
        private SqlCommandBuilder commandBuilder;
        //BindingSource bsShips, bsPirates;
        //only 1 data set
        //DataRelation //GetChildRows

        public Books()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string sqlString = "SELECT * FROM Books";
            booksDataAdapter = new SqlDataAdapter(sqlString, connectionString);
            booksDataSet = new DataSet();
            booksDataAdapter.Fill(booksDataSet, "Books");
            BooksGridView.DataSource = booksDataSet.Tables["Books"];
        }

        private void clientsGridView_SelectionChanged(object sender, EventArgs e)
        {
            object obj = BooksGridView.CurrentRow.Cells["ISBN"].Value;
            if (obj != DBNull.Value)
            {
                long isbn = (long)obj;
                SqlCommand command = new SqlCommand("SELECT * FROM Votes WHERE ISBN=@ISBN", sqlConnection);
                command.Parameters.AddWithValue("@ISBN", isbn);
                votesDataAdapter = new SqlDataAdapter(command);
                votesDataSet = new DataSet();
                votesDataAdapter.Fill(votesDataSet, "Votes");
                votesDataGridView.DataSource = votesDataSet.Tables["Votes"];
                sqlConnection.Close();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                commandBuilder = new SqlCommandBuilder(votesDataAdapter);
                votesDataAdapter.Update(votesDataSet, "Votes");
                MessageBox.Show("Changes are saved");
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

}
    }
}
