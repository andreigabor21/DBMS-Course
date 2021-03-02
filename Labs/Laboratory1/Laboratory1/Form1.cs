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
        private DataSet clientsDataSet;
        private DataSet loansDataSet;
        private SqlDataAdapter clientsDataAdapter;
        private SqlDataAdapter loansDataAdapter;
        private SqlCommandBuilder commandBuilder;

        public Books()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string sqlString = "SELECT c.CId, c.FirstName, c.SecondName, c.SSN, c.CAddress, c.PhoneNumber, c.Email, c.RegistrationDate, cg.CGName " +
                               "FROM Client c " +
                               "INNER JOIN ClientsGroups cg ON cg.CGId = c.CGId;";
            clientsDataAdapter = new SqlDataAdapter(sqlString, connectionString);
            clientsDataSet = new DataSet();
            clientsDataAdapter.Fill(clientsDataSet, "Clients");
            clientsGridView.DataSource = clientsDataSet.Tables["Clients"];
        }

        private void clientsGridView_SelectionChanged(object sender, EventArgs e)
        {
            object obj = clientsGridView.CurrentRow.Cells["CId"].Value;
            if (obj != DBNull.Value)
            {
                int clientId = (int)obj;
                SqlCommand command = new SqlCommand("SELECT * FROM Loans WHERE CId=@CId", sqlConnection);
                command.Parameters.AddWithValue("@CId", clientId);
                loansDataAdapter = new SqlDataAdapter(command);
                loansDataSet = new DataSet();
                loansDataAdapter.Fill(loansDataSet, "Loans");
                loansDataGridView.DataSource = loansDataSet.Tables["Loans"];
                sqlConnection.Close();
            }
       
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                commandBuilder = new SqlCommandBuilder(loansDataAdapter);
                loansDataAdapter.Update(loansDataSet, "Loans");
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
