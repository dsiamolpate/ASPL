using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net;
using System.IO;
using ASPL.CLASSES;
using ASPL.STARTUP.FORMS;

namespace ASPL.Utility
{
    public partial class FrmCreateNewDataBase : Form
    {
        string StrSQL,ConnectionString;
        SqlCommand cmd;
        Connection Objcon = new Connection();
        public FrmCreateNewDataBase()
        {
            InitializeComponent();
        }

        










        private void FrmCreateNewDataBase_Load(object sender, EventArgs e)
        {
            txtOlddatabase.Focus();
        }

        public void CreateDataBase(string OldDataBaseName, string NewDataBaseName)
        {
           

           // StrSQL = "IF EXISTS(SELECT * FROM master..sysdatabases WHERE Name='" + NewDataBaseName + "') DROP DATABASE " + NewDataBaseName + " CREATE DATABASE " + NewDataBaseName + "";
           //cmd = new SqlCommand(StrSQL, Objcon.connect());
           // cmd.ExecuteNonQuery();

            //StrSQL = "SELECT * FROM sys.Tables";
            //cmd = new SqlCommand(StrSQL, Objcon.connect());
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    StrSQL = "SELECT * INTO " + NewDataBaseName + ".dbo." + dr["name"] + " FROM " + OldDataBaseName + ".dbo." + dr["name"] + "";
            //    cmd = new SqlCommand(StrSQL, Objcon.connect());
            //    cmd.ExecuteNonQuery();
            //}

            ////DELETE TABLES
            //StrSQL = "USE " + NewDataBaseName + " DELETE FROM ";
        }
        private void BtnCreareDB_Click(object sender, EventArgs e)
        {
            CreateDataBase(txtOlddatabase.Text, txtNewDatabase.Text);


            string connString = "Data Source={0};Persist Security Info=True;Initial Catalog={1};User ID={2};Password={3}";

            connString = string.Format(connString, txtOlddatabase.Text,txtNewDatabase.Text, txtusernm.Text, txtPassword.Text);
            
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            ConnectionStringsSection connSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            connSection.ConnectionStrings["ASPLDBCoonection"].ConnectionString = connString;

            config.Save(ConfigurationSaveMode.Modified);



            MessageBox.Show("Successfully Completed", "Success",

                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtOlddatabase_Enter(object sender, EventArgs e)
        {
            txtNewDatabase.BackColor = Color.White;     
            txtOlddatabase.BackColor = Color.Yellow;     
        }

        private void txtNewDatabase_Enter(object sender, EventArgs e)
        {
           txtOlddatabase.BackColor = Color.White;
           txtNewDatabase.BackColor = Color.Yellow; 
        }

        private void txtOlddatabase_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
