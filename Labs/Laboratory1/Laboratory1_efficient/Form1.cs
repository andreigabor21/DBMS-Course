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

namespace Laboratory1_efficient
{
    public partial class Form1 : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter daBooks, daVotes;
        DataSet ds;
        SqlCommandBuilder cbVotes;
        BindingSource bsBooks, bsVotes;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                daVotes.Update(ds, "Votes");
                MessageBox.Show("Changes are saved");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string getBooksStringSql = "SELECT b.ISBN, b.Author, b.BDescription AS 'Description', c.CategoryName, l.LName AS 'Language', cv.Fabric, b.YearOfPublication, b.BCount AS 'Stock' FROM Books b INNER JOIN PublishingHouse ph ON b.PHId = ph.PHId INNER JOIN Languages l ON b.LId = l.LId INNER JOIN Category c ON b.CategoryID = c.CategoryID INNER JOIN CoverType cv ON b.CoverType = cv.CVId";
            string getVotesStringSql = "SELECT * FROM Votes";
            dbConn = new SqlConnection("Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                    "Initial Catalog = 'BookLibrary';" +
                                    "Integrated Security = true;");
            ds = new DataSet();
            daBooks = new SqlDataAdapter(getBooksStringSql, dbConn);
            daVotes = new SqlDataAdapter(getVotesStringSql, dbConn);
            cbVotes = new SqlCommandBuilder(daVotes);
            daBooks.Fill(ds, "Books");
            daVotes.Fill(ds, "Votes");
            
            DataRelation dr = new DataRelation("FK_Votes_Books",
                                                ds.Tables["Books"].Columns["ISBN"],
                                                ds.Tables["Votes"].Columns["ISBN"]);
            ds.Relations.Add(dr);

            bsBooks = new BindingSource();
            bsBooks.DataSource = ds;
            bsBooks.DataMember = "Books";

            bsVotes = new BindingSource();
            bsVotes.DataSource = bsBooks;
            bsVotes.DataMember = "FK_Votes_Books";

            dgvBooks.DataSource = bsBooks;
            dgvVotes.DataSource = bsVotes;
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
