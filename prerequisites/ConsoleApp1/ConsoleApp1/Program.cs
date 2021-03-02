using System;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection dbConn = new SqlConnection("Data Source = 'DESKTOP-FGTU247\\SQLEXPRESS';" +
                                                      "Initial Catalog = 'BookLibrary';" +
                                                      "Integrated Security = true;");
            dbConn.Open();
            Console.WriteLine("Connected");

            SqlCommand selectCmd = new SqlCommand("SELECT COUNT(*) FROM Books", dbConn);
            int count = (int)selectCmd.ExecuteScalar();
            Console.WriteLine("There are {0} books", count);

            SqlCommand insertCmd = new SqlCommand();
            insertCmd.CommandText = "INSERT Languages VALUES ('Portuguese')";
            insertCmd.CommandType = CommandType.Text;
            insertCmd.Connection = dbConn;
            /*insertCmd.ExecuteNonQuery();*/

            SqlCommand selectCmd2 = new SqlCommand("SELECT LId, LName FROM Languages", dbConn);
            SqlDataReader dr = selectCmd2.ExecuteReader();
            while (dr.Read())
                Console.WriteLine("{0} \t {1}", dr.GetInt32(0), dr.GetString(1));

            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT LId, LName FROM Languages", dbConn);
            dataAdapter.Fill(dataSet, "Languages");
            DataTable table = dataSet.Tables["Languages"];
            DataRow dataRow = table.NewRow();
            dataRow["LName"] = "test2";
            /*dataRow["LId"] = 7;*/
            SqlCommandBuilder cmdb = new SqlCommandBuilder(dataAdapter);
            table.Rows.Add(dataRow);
            dataAdapter.Update(dataSet, "Languages");

            dbConn.Close();



























            /*SqlConnection conn = new SqlConnection();
            string ConnString = "Data Source = DESKTOP-FGTU247\\SQLEXPRESS;Initial catalog = BookLibrary; Integrated Security = True";
            *//*AddElement(10, "qwerty", ConnString);
            deleteElement(10, ConnString);*//*
            ReadData(ConnString);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM S", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "S");
            UpdateRows(ds, 1, ConnString);
            da.Update(ds, "S");*/



            /* SqlConnection conn = new SqlConnection();
             conn.ConnectionString = "Data Source = DESKTOP-FGTU247\\SQLEXPRESS;Initial catalog = BookLibrary; Integrated Security = True";
             DataSet ds = new DataSet();

             SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM S", conn);
             SqlCommandBuilder cb = new SqlCommandBuilder(da);

             da.Fill(ds, "S");

             foreach (DataRow dr in ds.Tables["S"].Rows)
             {
                 Console.WriteLine("{0}, {1}", dr["ID"], dr["ColumnA"]);
             }

             DataRow drr = ds.Tables["S"].NewRow();
             drr["ID"] = 34986;
             drr["ColumnA"] = "cccc";
             ds.Tables["S"].Rows.Add(drr);

             da.Update(ds, "S");
             Console.ReadLine();*/
        }
    }
}
