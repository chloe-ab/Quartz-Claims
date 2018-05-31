//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;   //added
//using System.Data.OleDb;      //added
//using System.Data;            //added for Dataset
//                              //also added a Windows form by right-clicking "add" on the project

////HOWEVER THIS WHOLE CLASS MAY BE USELESS: "you can open a DBF file in Excel. An Excel file can be easily imported in Sql Server"

//namespace NewQuartzClaimsQuerier   //link to starting tutorial: http://csharp.net-informations.com/data-providers/csharp-oledb-connection.htm
//{
//    public partial class Form1 : Form   //the partial modifier allows for a single class to be implemented in multiple .cs files
//    {
//        public Form1()
//        {
//            InitializeLifetimeService(); //I updated this method, before was "InitializeComponent()"
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            //note that when connecting to DBase via OleDB, you do not specify the DBF file, but rather the folder which contains it. The individual files are tables.
//            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + yourdatabasename.mdb;  //OR +  myDirectoryPath + ";Extended Properties=dBASE III"; //IV;User ID=Admin;Password=;";
//            OleDbConnection connection = new OleDbConnection(connectionString);  //OleDbConnection is a built in class that represents an open connection to a data source.
//            try
//            {
//                connection.Open();
//                MessageBox.Show("Connection Open.");


//                //now from this page: https://www.codeproject.com/Questions/494495/ReadplusDBFplusfilepluswithplusOleDbConnection
//                //
//                //OleDbAdapter class represents a set of data commands and a database connection that are used to fill the DataSet and update the data source.
//                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + Path.GetFileName(fileName), connection);
//                DataSet dataSet = new DataSet(); ;
//                adapter.Fill(dataSet);
//                if (dataSet.Tables.Count > 0)
//                {
//                    data = dataSet.Tables[0];
//                }



//                connection.Close();

//                //or from other tutorial:

//                //using (OleDbConnection con = new OleDbConnection(connectionstring))
//                //{
//                //    var sql = "select * from " + fileName;
//                //    OleDbCommand cmd = new OleDbCommand(sql, con);
//                //    con.Open();
//                //    DataSet ds = new DataSet();
//                //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
//                //    da.Fill(ds);
//                //}
//            }


//            catch (Exception ex)
//            {
//                MessageBox.Show("Can not open connection.");
//            }


//        }

//    }
//}

