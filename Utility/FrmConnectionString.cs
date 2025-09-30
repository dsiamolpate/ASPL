using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;
using System.Configuration;
using ASPL.CLASSES;

namespace ASPL.Utility
{
    public partial class FrmConnectionString : Form
    {
        public FrmConnectionString()
        {
            InitializeComponent();
        }
        public static string ConnectionStringFlag = "";
        #region HIDE CLOSE BUTTON
        //Hide Close Button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        #endregion
        private void FrmConnectionString_Load(object sender, EventArgs e)
        {
            FillSQLServer();
        }


        public void FillSQLServer()
        {
            CmbServerName.Items.Insert(0, "SELECT");
           // string myServer = Environment.MachineName;
            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            for (int i = 0; i < servers.Rows.Count; i++)
            {
                if (servers.Rows[i]["ServerName"].ToString()!="")
                {
                    if ((servers.Rows[i]["InstanceName"] as string) != null && (servers.Rows[i]["InstanceName"] as string)=="SQLEXPRESS")
                    {
                        //CmbServerName.Visibility = Visibility.Visible;
                        CmbServerName.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                    }
                    else
                    {
                        //CmbServerName.Visibility = Visibility.Visible;
                        CmbServerName.Items.Add(servers.Rows[i]["ServerName"]);
                    }
                }            
            }
            this.CmbServerName.ValueMember = "ServerName";
            CmbServerName.SelectedIndex = 0;
            //// Create a instance of the SqlDataSourceEnumerator class
            //SqlDataSourceEnumerator objSqlDS = SqlDataSourceEnumerator.Instance;
 
            //// Fetch all visible SQL server 2000 or SQL Server 2005 instances
            //DataTable objTbl = objSqlDS.GetDataSources();
 
            //if(objTbl != null)
            //{
            //      // Bind the table to the ComboBox
            //     this.CmbServerName.DataSource = objTbl;
            //     this.CmbServerName.DisplayMember = "ServerName";
            //     this.CmbServerName.ValueMember = "ServerName";
            //}
            //else
            //{
            //    MessageBox.Show("SQL Server Instance Not Found", "ASPL");
            //    Application.Exit();
            //}          
        }


        #region Control Backcolor
        private void txtDatabaseName_Enter(object sender, EventArgs e)
        {
            txtDatabaseName.BackColor = Color.Yellow;
        }

        private void txtDatabaseName_Leave(object sender, EventArgs e)
        {
            txtDatabaseName.BackColor = Color.White;
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtUserName.BackColor = Color.Yellow;
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtUserName.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.Yellow;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }
        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            FunctionConnect();
        }

        public void FunctionConnect()
        {
            // string selected = this.CmbServerName.GetItemText(this.CmbServerName.SelectedItem);
            if (CmbServerName.Text == "SELECT" || CmbServerName.Text == "")
            {
                CmbServerName.Focus();
                MessageBox.Show("Select Server Name");
            }
            else if (txtDatabaseName.Text == String.Empty)
            {
                txtDatabaseName.Focus();
                MessageBox.Show("Please Enter DatabaseName");
            }
            //else if (txtUserName.Text == String.Empty)
            //{
            //    txtUserName.Focus();
            //    MessageBox.Show("Please Enter User Name");
            //}
            //else if (txtPassword.Text == String.Empty)
            //{
            //    txtDatabaseName.Focus();
            //    MessageBox.Show("Please Enter Password");
            //}
            else
            {
                if (txtDatabaseName.Text != "ASPLDB")
                {
                    txtDatabaseName.Focus();
                    MessageBox.Show("Wrong DatabaseName");
                }
                //else if (txtUserName.Text != "sa")
                //{
                //    txtDatabaseName.Focus();
                //    MessageBox.Show("Wrong Username");
                //}
                //else if (txtPassword.Text != "aryan05")
                //{
                //    txtDatabaseName.Focus();
                //    MessageBox.Show("Wrong Password");
                //}
                else
                {
                    string name = "ASPLDBCoonection";
                    // string selectedserver = this.CmbServerName.GetItemText(this.CmbServerName.SelectedItem);
                    string selectedserver = CmbServerName.Text;
                    string ApplicationPath = Application.StartupPath;
                    string YourPath = Path.GetDirectoryName(ApplicationPath);
                    bool isNew = false;
                    string path = Path.GetDirectoryName(YourPath) + "\\App.config";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
                    XmlNode node;
                    isNew = list.Count == 0;
                    if (isNew)
                    {
                        node = doc.CreateNode(XmlNodeType.Element, "add", null);
                        XmlAttribute attribute = doc.CreateAttribute("name");
                        attribute.Value = name;
                        node.Attributes.Append(attribute);
                        attribute = doc.CreateAttribute("connectionString");
                        attribute.Value = "";
                        node.Attributes.Append(attribute);
                        attribute = doc.CreateAttribute("providerName");
                        attribute.Value = "System.Data.SqlClient";
                        node.Attributes.Append(attribute);
                    }
                    else
                    {
                        node = list[0];
                    }
                    string conString = node.Attributes["connectionString"].Value;
                    conStringBuilder = new SqlConnectionStringBuilder(conString);
                    conStringBuilder.InitialCatalog = txtDatabaseName.Text;
                    conStringBuilder.DataSource = selectedserver;
                    conStringBuilder.IntegratedSecurity = false;
                    conStringBuilder.PersistSecurityInfo = false;
                    //conStringBuilder.UserID = "sa";
                    //conStringBuilder.Password = "aryan05";
                    node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
                    //UPDATED CONNECTION STRING     
                    ConnectionStringFlag = "UpdatedConnectionStr";
                    Connection.connstr = conStringBuilder.ConnectionString;
                    if (isNew)
                    {
                        doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
                    }
                    ConfigurationManager.RefreshSection("connectionStrings");
                    doc.Save(path);
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configFile.Save(ConfigurationSaveMode.Modified, true);
                    ConfigurationManager.RefreshSection(configFile.ConnectionStrings.SectionInformation.Name);
                    Properties.Settings.Default.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");
                    this.Close();
                }
            }
        }

        private void FrmConnectionString_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Control nextTab;
                if (e.KeyChar == (char)Keys.Enter)
                {
                    nextTab = ((Control)sender);
                    nextTab = GetNextControl(ActiveControl, true);

                    if (nextTab.Name == "BtnExit")
                    {
                    }
                    else
                    {
                        nextTab.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        public System.Data.SqlClient.SqlConnectionStringBuilder conStringBuilder { get; set; }
    }
}
