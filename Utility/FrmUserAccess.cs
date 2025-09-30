using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net;
using ASPL.CLASSES;
using ASPL.LODGE.STARTUP_FORM;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic; 


namespace ASPL.Utility
{
    public partial class FrmUserAccess : Form
    {
        Design ObjDesign = new Design();
        Connection con = new Connection();
        SqlCommand command = new SqlCommand();
      //  SqlConnection connection = con.connect();
      //  SqlCommand command = new SqlCommand();
        Module mod = new Module();
        DataGridViewCellStyle style = new DataGridViewCellStyle();
        Connection ObjCon = new Connection();
        string flagchar, strSQL,str;
        string ctype = "T";
        int previousTabIndex = 1;
        public FrmUserAccess()
        {
            InitializeComponent();
        }

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


        private void mastGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        
        }

        private void FrmUserAccess_Load(object sender, EventArgs e)
        {
            try
            {
                ObjDesign.FormDesign(this,mastGrid);
                ObjDesign.FormDesign(this, transGrid);
                ObjDesign.FormDesign(this, reportGrid);
                ObjDesign.FormDesign(this, utilityGrid);
                ObjDesign.FormDesign(this, applicationgrid);
                // mod.centerme(this);
                if (Globalvariable.ModuleNm == "Lodge")
                {
                    flagchar = "L";
                }
                else if( Globalvariable.ModuleNm == "Restaurant")
                {
                  Globalvariable.flagchar = "R";
                }
                else if (Globalvariable.ModuleNm == "Material")
                {
                    Globalvariable.flagchar = "M";
                }
                else if (Globalvariable.ModuleNm == "Accounts")
                {
                    Globalvariable.flagchar = "A";
                }

              //  Globalvariable.flagchar
                strSQL = "SELECT UserName,User_ID FROM tbLoginDetails WHERE Branchcode='" + Globalvariable.bcode + "' ORDER BY UserName";
                //  SqlDataAdapter da = new SqlDataAdapter(strSQL, ASPLCSS.connect());
                //  DataTable dt = new DataTable();
                //    da.Fill(dt);
                SqlCommand cmd = new SqlCommand(strSQL, con.connect());
                SqlDataReader dr = cmd.ExecuteReader();
                var dict = new Dictionary<string, string>();
                while (dr.Read())
                {
                    dict.Add(dr["User_ID"].ToString(), dr["UserName"].ToString());
                }
                Userlist.DataSource = new BindingSource(dict, null);
                Userlist.DisplayMember = "Value";
                Userlist.ValueMember = "Key";

                Userlist.Focus();
                tabUtility.SelectedIndex = 0;
                previousTabIndex = 1;
                fillgrid(Globalvariable.flagchar, 1, mastGrid);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }          
            }


        private void FillApplication(DataGridView gridnm)
        {
            ArrayList ApplicatinID =new ArrayList(4);
            gridnm.Rows.Clear();
                SqlCommand cmd = new SqlCommand("SP_FrmRegister", ObjCon.connect());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@variable", "SELECT");
                cmd.Parameters.AddWithValue("@MacAddress", Globalvariable.MacAddress);
                //dsForRegister = ObjCon.GetConnectionSelect(StrSQL);
               SqlDataReader dataRegisterdr = cmd.ExecuteReader();
                while (dataRegisterdr.Read())
                {
                    if (dataRegisterdr["Application_Name_Lodge"].ToString() == "L")
                    {
                        ApplicatinID.Add( "1");
                    }
                    if (dataRegisterdr["Application_Name_Restaurant"].ToString() == "R")
                    {
                        ApplicatinID.Add("2");
                    }
                    if (dataRegisterdr["Application_Name_MaterialMgmnt"].ToString() == "M")
                    {
                        ApplicatinID.Add("3");
                    }
                    if (dataRegisterdr["Application_Name_Accounts"].ToString() == "A")
                    {
                        ApplicatinID.Add("4");
                    }
                    int cnt = 0,i=0;
                    for (i = 0; i < ApplicatinID.Count; i++)
                    {
                        SqlCommand strSQL = new SqlCommand("SELECT Application_id,Application_name FROM tbApplicationName WHERE Application_id='" + ApplicatinID[i] + "'", con.connect());
                        SqlDataReader dr = strSQL.ExecuteReader();
                        while (dr.Read())
                        {
                            string[] row;
                            cnt = cnt + 1;
                            row = new string[] { cnt.ToString(), "", dr["Application_name"].ToString(), "N", dr["Application_id"].ToString() };
                            gridnm.Rows.Add(row);
                            Application_YN(cnt, dr["Application_name"].ToString(), dr["Application_id"].ToString(), gridnm);
                        }
                    }
                    
                }
        }

        private void Application_YN(int srNo,string descr, string parentIDTemp, DataGridView tempGrid)
        {
            try
            {

                SqlCommand str2 = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' and user_id='" +
                         Userlist.SelectedValue + "' and Application_id='" + (previousTabIndex)+"'", con.connect());
                SqlDataReader dr3 = str2.ExecuteReader();

                if (dr3.HasRows)
                {
                    dr3.Read();
                    string[] subRow;
                    subRow = new string[] {srNo.ToString(),"", descr, mod.Isnull(dr3["FormAccess"].ToString(),"N") , parentIDTemp};
                    tempGrid.Rows.Add(subRow);
                }
              
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void fillgrid(string strflag, int parentid, DataGridView gridnm)
        {
            try
            {
                gridnm.Rows.Clear();
                //SqlCommand strSQL = new SqlCommand("SELECT * FROM tbLevel_2 WHERE flag='" + strflag +
                //         "' AND lev1_id=" + parentid + " ORDER BY lev2_fieldname", con.connect());
                SqlCommand strSQL = new SqlCommand("SELECT * FROM tbLevel_2 WHERE (flag=('" + Globalvariable.flagchar +
                    "') OR flag=('AL')) AND lev1_id=" + parentid + " ORDER BY lev2_fieldname", con.connect());
                SqlDataReader dr = strSQL.ExecuteReader();
                string st = "SELECT * FROM tbLevel_2 WHERE flag='" + strflag +
                         "' AND lev1_id=" + parentid + " ORDER BY lev2_fieldname";
                int cnt = 0, subCnt = 0,childCnt=0;
                style.Font = new Font(gridnm.Font, FontStyle.Bold);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cnt = cnt + 1;
                        SqlCommand str1 = new SqlCommand("SELECT * FROM tbLevel_3 WHERE lev2_id='"+ dr["lev2_id"] + "'", con.connect());
                       SqlDataReader dr1 =str1.ExecuteReader();
                      //  subCnt = 0;
                        if (dr1.HasRows)
                        {
                            string[] row;
                            if (previousTabIndex == 4)
                                row = new string[] { cnt.ToString(), "", dr["lev2_fieldname"].ToString(), "", dr["lev2_id"].ToString(), "" };
                            else
                                row = new string[] { cnt.ToString(), "", dr["lev2_fieldname"].ToString(), "", "", "", "", "", dr["lev2_id"].ToString(), "","" };
                            gridnm.Rows.Add(row);
                            while (dr1.Read())
                            {
                                subCnt = subCnt + 1;
                                Level3_YN(dr1["lev3_fieldname"].ToString(), dr["lev2_id"].ToString(), dr1["lev3_id"].ToString(), subCnt, gridnm);
                                SqlCommand str2 = new SqlCommand("SELECT * FROM tbLevel_4 WHERE lev3_id='" + dr1["lev3_id"] + "'", con.connect());                              
                                SqlDataReader dr2 = str2.ExecuteReader();
                                while (dr2.Read())
                                {
                                    childCnt = childCnt + 1;
                                    Level4_YN(dr2["lev4_fieldname"].ToString(), dr["lev2_id"].ToString(), dr1["lev3_id"].ToString(), dr2["lev4_id"].ToString(), childCnt, gridnm);
                                }
                            }
                        }
                        else
                        {
                            Level2_YN(cnt, dr["lev2_fieldname"].ToString(), dr["lev2_id"].ToString(), gridnm);
                        }
                    }
                    BoldAlignGrid(gridnm); 
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void Level3_YN(string descr, string parentIDTemp, string childIDTemp, int srNo, DataGridView tempGrid)
        {
            try
            {
                
                SqlCommand str2 = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' and user_id='" +
                         Userlist.SelectedValue + "' and lev1_id='" + (previousTabIndex) +
                         "' and lev2_id='" + parentIDTemp + "' and lev3_id='" + childIDTemp + "' and flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader dr3 = str2.ExecuteReader();
               
                if (dr3.HasRows)
                {
                    dr3.Read();
                    string[] subRow;
                    if (previousTabIndex == 4)
                        subRow = new string[] { "", srNo.ToString(), descr, mod.Isnull(dr3["FormAccess"].ToString(),"N") , 
                       parentIDTemp, childIDTemp,"" };
                    else
                        subRow = new string[] { "", srNo.ToString(), descr, mod.Isnull(dr3["FormAccess"].ToString(),"N") , 
                        mod.Isnull(dr3["bAdd"].ToString(),"N"), mod.Isnull(dr3["bUpdate"].ToString(),"N"), mod.Isnull(dr3["bDelete"].ToString(),"N"),  
                        mod.Isnull(dr3["bPrint"].ToString(),"N"), parentIDTemp, childIDTemp,"" };
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow;
                    if (previousTabIndex == 4)
                        subRow = new string[] { "", srNo.ToString(), descr, "N", parentIDTemp, childIDTemp,"" };
                    else
                        subRow = new string[] { "", srNo.ToString(), descr, "N" , 
                            "N","N","N","N", parentIDTemp, childIDTemp,"" };
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void Level4_YN(string descr, string lev_2, string lev_3,string lev_4, int srNo, DataGridView tempGrid)
        {
            try
            {

                SqlCommand str2 = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' and user_id='" +
                         Userlist.SelectedValue + "' and lev1_id='" + (previousTabIndex) +
                         "' and lev2_id='" + lev_2 + "' and lev3_id='" + lev_3 + "' and lev4_id='" + lev_4 + "' and flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader dr3 = str2.ExecuteReader();

                if (dr3.HasRows)
                {
                    dr3.Read();
                    string[] subRow;
                    if (previousTabIndex == 4)
                        subRow = new string[] { "", srNo.ToString(), descr, mod.Isnull(dr3["FormAccess"].ToString(),"N") , 
                       lev_2, lev_3,lev_4 };
                    else
                        subRow = new string[] { "", srNo.ToString(), descr, mod.Isnull(dr3["FormAccess"].ToString(),"N") , 
                        mod.Isnull(dr3["bAdd"].ToString(),"N"), mod.Isnull(dr3["bUpdate"].ToString(),"N"), mod.Isnull(dr3["bDelete"].ToString(),"N"),  
                        mod.Isnull(dr3["bPrint"].ToString(),"N"), lev_2, lev_3,lev_4 };
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow;
                    if (previousTabIndex == 4)
                        subRow = new string[] { "", srNo.ToString(), descr, "N", lev_2, lev_3, lev_4 };
                    else
                        subRow = new string[] { "", srNo.ToString(), descr, "N" , 
                            "N","N","N","N", lev_2, lev_3,lev_4};
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }




        private void Level2_YN(int srNo, string descr, string parentIDTemp, DataGridView tempGrid)
        {
            try
            {
                SqlCommand str3 = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' AND user_id='" +
                         Userlist.SelectedValue + "' AND lev1_id='" + (previousTabIndex) +
                         "' AND lev2_id='" + parentIDTemp + "' AND flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader dr4 = str3.ExecuteReader();
                if (dr4.HasRows)
                {
                    dr4.Read();
                    string[] subRow;
                    if (previousTabIndex == 3)
                        subRow = new string[] {srNo.ToString(),"","","", descr, mod.Isnull(dr4["FormAccess"].ToString(),"N") , 
                        mod.Isnull(dr4["bshow"].ToString(),"N"), mod.Isnull(dr4["bPrint"].ToString(),"N"),  
                        parentIDTemp, "","" };
                    else if (previousTabIndex == 5)
                        subRow = new string[] { srNo.ToString(), "", descr, mod.Isnull(dr4["FormAccess"].ToString(), "N"), parentIDTemp, "",""};
                    else
                        subRow = new string[] {srNo.ToString(),"", descr, mod.Isnull(dr4["FormAccess"].ToString(),"N") , 
                        mod.Isnull(dr4["bAdd"].ToString(),"N"), mod.Isnull(dr4["bUpdate"].ToString(),"N"), mod.Isnull(dr4["bDelete"].ToString(),"N"),  
                        mod.Isnull(dr4["bPrint"].ToString(),"N"), parentIDTemp, "",""};
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow;
                    if (previousTabIndex == 3)
                        subRow = new string[] { srNo.ToString(), "", "", "", descr, "N", "N", "N", parentIDTemp, "", "" };
                    else if (previousTabIndex == 4)
                        subRow = new string[] { srNo.ToString(), "", descr, "N", parentIDTemp, "","" };
                    else
                        subRow = new string[] { srNo.ToString(), "", descr, "N", "N", "N", "N", "N", parentIDTemp, "","" };
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void reportGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Userlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGridData();
        }
        private void fillGridData()
        {
            try
            {
                if (tabUtility.SelectedIndex == 0)
                {
                    previousTabIndex = 1;
                    fillgrid(Globalvariable.flagchar, 1, mastGrid);
                }
                else if (tabUtility.SelectedIndex == 1)
                {
                    previousTabIndex = 2;
                    fillgrid(Globalvariable.flagchar, 2, transGrid);
                }
                else if (tabUtility.SelectedIndex == 2)
                {
                    previousTabIndex = 3;
                    fillGridReport(Globalvariable.flagchar, 3, reportGrid);
                    //fillGrid(flagChar, 3, reportGrid);
                }
                else if (tabUtility.SelectedIndex == 3)
                {
                    previousTabIndex = 4;
                    fillgrid(Globalvariable.flagchar, 4, utilityGrid );        
                }
                else if (tabUtility.SelectedIndex == 4)
                {
                    previousTabIndex = 5;
                    FillApplication(applicationgrid);
                }
               
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void tabUtility_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           tabSaveinfo();
           fillGridData();
        }

        private void Save(DataGridView tempgrid)
        {
            try
            {
                if (previousTabIndex == 2)
                {
                    ctype = "R";
                }
                strSQL = "DELETE FROM tbUserAccess WHERE user_id='" + Userlist.SelectedValue + "' AND branch_code='" +
                          Globalvariable.bcode + "' AND flag='" + Globalvariable.flagchar + "' AND lev1_id='" + (previousTabIndex) + "'";
                SqlCommand cmd = new SqlCommand(strSQL, con.connect());
                cmd.CommandTimeout = 60;
                cmd.ExecuteNonQuery();
             //   command.CommandText = strSQL;
               // command.ExecuteNonQuery();
                for (int cnt = 0; cnt < tempgrid.Rows.Count; cnt++)
                {
                    strSQL = "INSERT INTO tbUserAccess(user_id,sMenuName,cType,lev1_id,lev2_id,lev3_id,lev4_id,module,FormAccess,bAdd," +
                           "bUpdate,bDelete,bPrint,branch_code,flag) VALUES('" + Userlist.SelectedValue + "','" + previousTabIndex + "','" + ctype + "'," +
                    " '" + (previousTabIndex) + "','" + tempgrid.Rows[cnt].Cells[8].Value + "', " +
                    "'" + mod.Isnull(tempgrid.Rows[cnt].Cells[9].Value.ToString(), "0") + "'," +
                    "'" + mod.Isnull(tempgrid.Rows[cnt].Cells[10].Value.ToString(), "0") + "'," +
                     " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[2].Value.ToString(), "0") + "', " +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[3].Value.ToString(), "N") + "', " +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[4].Value.ToString(), "N") + "', " +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[5].Value.ToString(), "N") + "'," +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[6].Value.ToString(), "N") + "'," +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[7].Value.ToString(), "N") + "','" + Globalvariable.bcode + "','" + Globalvariable.flagchar + "')";
                    command.CommandText = strSQL;
                    command.ExecuteNonQuery();
                    string st = mod.Isnull(tempgrid.Rows[cnt].Cells[10].Value.ToString(), "0");
                }
              
                string mytext = Userlist.Text;
                string msg = "User Accessibility Set For The User:" + mytext;
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void tabSaveinfo()
        {
            DialogResult mydialog;
            mydialog = MessageBox.Show("Want to Save and Proceed ???", "Save & Proceed", MessageBoxButtons.YesNo);
            if (mydialog == DialogResult.Yes)
            {
                if (previousTabIndex == 1)
                    Save(mastGrid);
                else if (previousTabIndex == 2)
                    Save(transGrid);
                else if (previousTabIndex == 3)
                    SaveReport(reportGrid);
                else if (previousTabIndex == 4)
                    SaveUtility(utilityGrid);
                else if (previousTabIndex == 5)
                    Saveapplication(applicationgrid);
            }
        }

        private void Saveapplication(DataGridView tempgrid)
        {
            try
            {
               
               
                for (int i = 0; i < applicationgrid.Rows.Count;i++ )
                {
                    strSQL = "DELETE FROM tbUserAccess WHERE user_id='" + Userlist.SelectedValue + "' AND branch_code='" +
                              Globalvariable.bcode + "' And Application_id='" + applicationgrid.Rows[i].Cells[4].Value + "'";
                    command =new SqlCommand(strSQL,con.connect()); ;
                    command.ExecuteNonQuery();
                }

                for (int cnt = 0; cnt < tempgrid.Rows.Count; cnt++)
                {

                    /* strSQL = "INSERT INTO tbUserAccess(user_id,lev1_id,lev2_id,FormAccess,module," +
                            "branch_code,flag) values('" + Userlist.SelectedValue + "', " +
                     " '" + (previousTabIndex) + "','" + tempgrid.Rows[cnt].Cells[4].Value + "', " +
                     "'" + mod.Isnull(tempgrid.Rows[cnt].Cells[5].Value.ToString(), "0") + "'," +
                     " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[3].Value.ToString(), "0") + "'," +
                      " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[2].Value.ToString(), "0") + "','" + Globalvariable.bcode + "','" + flagchar + "')";*/

                    strSQL = "INSERT INTO tbUserAccess(user_id,module,FormAccess,Application_id,branch_code) values('" + Userlist.SelectedValue + "'," +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[2].Value.ToString(), "N") + "'," +
                     " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[3].Value.ToString(), "0") + "','" + mod.Isnull(tempgrid.Rows[cnt].Cells[4].Value.ToString(), "0") + "','" + Globalvariable.bcode + "')";
                    command.CommandText = strSQL;
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Saved Successfully!!!!");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }


        private void SaveReport(DataGridView tempgrid)
        {
            try
            {
                SqlConnection connection = con.connect();
                SqlCommand command = new SqlCommand();
               
                strSQL = "delete from tbUserAccess where user_id='" + Userlist.SelectedValue + "' and branch_code='" +
                            Globalvariable.bcode + "' and flag='" + Globalvariable.flagchar + "' and lev1_id='" + (previousTabIndex) + "'";
                command =new SqlCommand( strSQL,con.connect());
                command.ExecuteNonQuery();
                for (int cnt = 0; cnt < tempgrid.Rows.Count; cnt++)
                {
                    strSQL = "insert into tbUserAccess(user_id,lev1_id,lev2_id,lev3_id,lev4_id,FormAccess," +
                           "bPrint,branch_code,flag) values('" + Userlist.SelectedValue + "', " +
                    " '" + (previousTabIndex) + "','" + tempgrid.Rows[cnt].Cells[8].Value + "', " +
                    "'" + mod.Isnull(tempgrid.Rows[cnt].Cells[9].Value.ToString(), "0") + "'," +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[10].Value.ToString(), "0") + "', " +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[3].Value.ToString(), "0") + "', " +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[7].Value.ToString(), "0") + "','" +
                    Globalvariable.bcode + "','" + Globalvariable.flagchar + "')";
                    command.CommandText = strSQL;
                    command.ExecuteNonQuery();
                }
               MessageBox.Show("Saved Successfully!!!!");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void SaveUtility(DataGridView tempgrid)
        {
            try
            {
                
                strSQL = "DELETE FROM tbUserAccess WHERE user_id='" + Userlist.SelectedValue + "' AND branch_code='" +
                          Globalvariable.bcode + "' and flag='AL' and lev1_id='" + (previousTabIndex) + "'";
                command.CommandText = strSQL;
                command.ExecuteNonQuery();
                for (int cnt = 0; cnt < tempgrid.Rows.Count; cnt++)
                {

                   /* strSQL = "INSERT INTO tbUserAccess(user_id,lev1_id,lev2_id,FormAccess,module," +
                           "branch_code,flag) values('" + Userlist.SelectedValue + "', " +
                    " '" + (previousTabIndex) + "','" + tempgrid.Rows[cnt].Cells[4].Value + "', " +
                    "'" + mod.Isnull(tempgrid.Rows[cnt].Cells[5].Value.ToString(), "0") + "'," +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[3].Value.ToString(), "0") + "'," +
                     " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[2].Value.ToString(), "0") + "','" + Globalvariable.bcode + "','" + flagchar + "')";*/

                    strSQL = "INSERT INTO tbUserAccess(user_id,FormAccess,module,branch_code,flag,sMenuName,lev1_id,lev2_id) values('" + Userlist.SelectedValue + "'," +
                    " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[3].Value.ToString(), "N") + "'," +
                     " '" + mod.Isnull(tempgrid.Rows[cnt].Cells[2].Value.ToString(), "0") + "','" + Globalvariable.bcode + "','AL','" + previousTabIndex + "','" + previousTabIndex + "','" +  mod.Isnull(tempgrid.Rows[cnt].Cells[5].Value.ToString(), "0") + "')";
                    command.CommandText = strSQL;
                    command.ExecuteNonQuery();
                }
              MessageBox.Show("Saved Successfully!!!!");
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BoldAlignGrid(DataGridView tempGrid)
        {
            try
            {
                foreach (DataGridViewRow row in tempGrid.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (row.Cells[0].Value.ToString() != "")
                            style.Font = new Font(tempGrid.Font, FontStyle.Bold);
                        //else if (previousTabIndex==3 && row.Cells[1].Value.ToString() != "")
                        //    style.Font = new Font(tempGrid.Font, FontStyle.Bold);
                        else
                            style.Font = new Font(tempGrid.Font, FontStyle.Regular);
                        cell.Style.ApplyStyle(style);
                        if (cell.ColumnIndex == 2 && previousTabIndex != 3)
                            cell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        else if (previousTabIndex == 3 && cell.ColumnIndex == 4)
                            cell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        else
                            cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }

        }

        private void fillGridReport(string strFlag, int parentId, DataGridView tempGrid)
        {
            try
            {
                tempGrid.Rows.Clear();
                SqlCommand strSQL1 = new SqlCommand("SELECT * FROM tbLevel_2 WHERE flag='" + strFlag +
                          "' AND lev1_id='" + parentId + "' ORDER BY lev2_fieldname", con.connect());
                SqlDataReader dr = strSQL1.ExecuteReader();
                int cnt = 0, subCnt = 0, subChildCnt = 0;
                style.Font = new Font(tempGrid.Font, FontStyle.Bold);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cnt = cnt + 1;
                        SqlCommand strSQL2 = new SqlCommand("SELECT * FROM tbLevel_3 WHERE lev2_id='" +
                                   dr["lev2_id"] + "'", con.connect());
                        SqlDataReader dr1 = strSQL2.ExecuteReader();
                        subCnt = 0;
                        if (dr1.HasRows)
                        {
                            string[] row = new string[] { cnt.ToString(), "", "", "", dr["lev2_fieldname"].ToString(), "", "", "", dr["lev2_id"].ToString(), "", "" };
                            tempGrid.Rows.Add(row);
                            while (dr1.Read())
                            {
                                subCnt = subCnt + 1;
                                SqlCommand strSQL = new SqlCommand("SELECT * FROM tbLevel_4 WHERE lev3_id='" +
                                           dr1["lev3_id"] + "'", con.connect());
                                SqlDataReader drChild = strSQL.ExecuteReader();
                                if (drChild.HasRows)
                                {
                                    subChildCnt = 0;
                                    row = new string[] {"", subCnt.ToString(), "", "", dr1["lev3_fieldname"].ToString(), "", "", "", dr["lev2_id"].ToString(),
                                    dr1["lev3_id"].ToString(),""};
                                    tempGrid.Rows.Add(row);
                                    while (drChild.Read())
                                    {
                                        subChildCnt = subChildCnt + 1;
                                        ChildAccessReport_YN(drChild["lev4_fieldname"].ToString(), dr["lev2_id"].ToString(), dr1["lev3_id"].ToString(), drChild["lev4_id"].ToString(), subChildCnt, tempGrid);
                                    }
                                }
                                else
                                {
                                    ParentAccessReport_YN(subCnt, dr1["lev3_fieldname"].ToString(), dr["lev2_id"].ToString(), dr1["lev3_id"].ToString(), tempGrid);
                                }
                            }
                        }
                        else
                        {
                            ParentAccess_YN(cnt, dr["lev2_fieldname"].ToString(), dr["lev2_id"].ToString(), tempGrid);
                        }
                    }
                    BoldAlignGrid(tempGrid);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void ParentAccess_YN(int srNo, string descr, string parentIDTemp, DataGridView tempGrid)
        {
            try
            {
                SqlCommand strSQL1 = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' AND user_id='" +
                         Userlist.SelectedValue + "' and lev1_id='" + (previousTabIndex) +
                         "' and lev2_id='" + parentIDTemp + "' and flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader tempDr = strSQL1.ExecuteReader();
                if (tempDr.HasRows)
                {
                    string[] subRow;
                    if (previousTabIndex == 3)
                        subRow = new string[] {srNo.ToString(),"","","", descr, mod.Isnull(tempDr["FormAccess"].ToString(),"N") , 
                        mod.Isnull(tempDr["bshow"].ToString(),"N"), mod.Isnull(tempDr["bPrint"].ToString(),"N"),  
                        parentIDTemp, "","" };
                    else if (previousTabIndex == 5)
                        subRow = new string[] { srNo.ToString(), "", descr, mod.Isnull(tempDr["FormAccess"].ToString(), "N"), parentIDTemp, "" };
                    else
                        subRow = new string[] {srNo.ToString(),"", descr, mod.Isnull(tempDr["FormAccess"].ToString(),"N") , 
                        mod.Isnull(tempDr["bAdd"].ToString(),"N"), mod.Isnull(tempDr["bUpdate"].ToString(),"N"), mod.Isnull(tempDr["bDelete"].ToString(),"N"),  
                        mod.Isnull(tempDr["bPrint"].ToString(),"N"), parentIDTemp, "" };
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow;
                    if (previousTabIndex == 3)
                        subRow = new string[] { srNo.ToString(), "", "", "", descr, "N", "N", "N", parentIDTemp, "", "" };
                    else if (previousTabIndex == 5)
                        subRow = new string[] { srNo.ToString(), "", descr, "N", parentIDTemp, "" };
                    else
                        subRow = new string[] { srNo.ToString(), "", descr, "N", "N", "N", "N", "N", parentIDTemp, "" };
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void ChildAccess_YN(string descr, string parentIDTemp, string childIDTemp, int srNo, DataGridView tempGrid)
        {
            try
            {
                SqlCommand strSQL = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' AND user_id='" +
                          Userlist.SelectedValue + "' AND lev1_id='" + (previousTabIndex) +
                          "' AND lev2_id='" + parentIDTemp + "' AND lev3_id='" + childIDTemp + "' AND flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader tempDr = strSQL.ExecuteReader();
                if (tempDr.HasRows)
                {
                    string[] subRow;
                    if (previousTabIndex == 5)
                        subRow = new string[] { "", srNo.ToString(), descr, mod.Isnull(tempDr["FormAccess"].ToString(),"N") , 
                       parentIDTemp, childIDTemp };
                    else
                        subRow = new string[] { "", srNo.ToString(), descr, mod.Isnull(tempDr["FormAccess"].ToString(),"N") , 
                        mod.Isnull(tempDr["bAdd"].ToString(),"N"), mod.Isnull(tempDr["bUpdate"].ToString(),"N"), mod.Isnull(tempDr["bDelete"].ToString(),"N"),  
                        mod.Isnull(tempDr["bPrint"].ToString(),"N"), parentIDTemp, childIDTemp };
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow;
                    if (previousTabIndex == 5)
                        subRow = new string[] { "", srNo.ToString(), descr, "N", parentIDTemp, childIDTemp };
                    else
                        subRow = new string[] { "", srNo.ToString(), descr, "N" , 
                            "N","N","N","N", parentIDTemp, childIDTemp };
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void ChildAccessReport_YN(string descr, string parentIDTemp, string childIDTemp, string SubChildTemp, int srNo, DataGridView tempGrid)
        {
            try
            {
                SqlCommand strSQL = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' AND user_id='" +
                          Userlist.SelectedValue + "' AND lev1_id='" + (previousTabIndex) +
                          "' AND lev2_id='" + parentIDTemp + "' AND lev3_id='" + childIDTemp +
                          "' AND lev4_id='" + SubChildTemp + "' AND flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader tempDr = strSQL.ExecuteReader();
                if (tempDr.HasRows)
                {
                    string[] subRow = new string[] { "","", srNo.ToString(),"", descr, mod.Isnull(tempDr["FormAccess"].ToString(),"N") , 
                    mod.Isnull(tempDr["bshow"].ToString(),"N"), mod.Isnull(tempDr["bPrint"].ToString(),"N"),  
                    parentIDTemp, childIDTemp,SubChildTemp };
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow = new string[] {"","", srNo.ToString(),"", descr, "N" , 
                    "N","N", parentIDTemp, childIDTemp,SubChildTemp };
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message"); 
            }
        }

        private void ParentAccessReport_YN(int srNo, string descr, string parentIDTemp, string childIDTemp, DataGridView tempGrid)
        {
            try
            {
                SqlCommand strSQL = new SqlCommand("SELECT * FROM tbUserAccess WHERE branch_code='" + Globalvariable.bcode + "' AND user_id='" +
                         Userlist.SelectedValue + "' AND lev1_id='" + (previousTabIndex) +
                         "' AND lev2_id='" + parentIDTemp + "' AND lev3_id='" + childIDTemp + "' AND flag='" + Globalvariable.flagchar + "'", con.connect());
                SqlDataReader tempDr = strSQL.ExecuteReader();
                if (tempDr.HasRows)
                {
                    string[] subRow = new string[] {"",srNo.ToString(),"","", descr, mod.Isnull(tempDr["FormAccess"].ToString(),"N") , 
                    mod.Isnull(tempDr["bshow"].ToString(),"N"), mod.Isnull(tempDr["bPrint"].ToString(),"N"),  
                    parentIDTemp,childIDTemp, "" };
                    tempGrid.Rows.Add(subRow);
                }
                else
                {
                    string[] subRow = new string[] { "", srNo.ToString(), "", "", descr, "N", "N", "N", parentIDTemp, childIDTemp, "" };
                    tempGrid.Rows.Add(subRow);
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void mastGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cellDoubleClick(mastGrid);

        }

        private void cellDoubleClick(DataGridView tempGrid)
        {
            int rowIndex = tempGrid.CurrentCell.RowIndex;
            int colIndex = tempGrid.CurrentCell.ColumnIndex;
            if (colIndex == 3 || colIndex == 4 || colIndex == 5 || colIndex == 6 || colIndex == 7)
            {
                if (tempGrid.Rows[rowIndex].Cells[colIndex].Value.ToString() == "Y")
                    tempGrid.Rows[rowIndex].Cells[colIndex].Value = "N";
                else if (tempGrid.Rows[rowIndex].Cells[colIndex].Value.ToString() == "N")
                    tempGrid.Rows[rowIndex].Cells[colIndex].Value = "Y";
            }
        }

        private void transGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            cellDoubleClick(transGrid);
        }

        private void reportGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cellDoubleClick(reportGrid);
        }

        private void utilityGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cellDoubleClick(utilityGrid);
        }

        private void tabUtility_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 3)
                previousTabIndex = 4;
            else
                previousTabIndex = e.TabPageIndex + 1;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (tabUtility.SelectedIndex == 0)
                Save(mastGrid);
            else if (tabUtility.SelectedIndex == 1)
                Save(transGrid);
            else if (tabUtility.SelectedIndex == 2)
                SaveReport(reportGrid);
            else if (tabUtility.SelectedIndex == 3)
                SaveUtility(utilityGrid);
            else if (tabUtility.SelectedIndex == 4)
                Saveapplication(applicationgrid);
        }

        private void SelectRemoveSel(DataGridView tempgrid, char flag)
        {
            try
            {
                foreach (DataGridViewRow row in tempgrid.Rows)
                {
                    foreach (DataGridViewCell col in row.Cells)
                    {
                        if (tabUtility.SelectedIndex == 2)
                        {
                            if ((col.ColumnIndex == 5 || col.ColumnIndex == 6 || col.ColumnIndex == 7) && (row.Cells[col.ColumnIndex].Value.ToString() != ""))
                                row.Cells[col.ColumnIndex].Value = flag;
                        }
                        else if (previousTabIndex == 5)
                        {
                            if ((col.ColumnIndex == 3) && (row.Cells[col.ColumnIndex].Value.ToString() != ""))
                            {
                                row.Cells[col.ColumnIndex].Value = flag;
                            }
                        }
                        else
                        {
                            if ((col.ColumnIndex == 3 || col.ColumnIndex == 4 || col.ColumnIndex == 5 ||
                                col.ColumnIndex == 6 || col.ColumnIndex == 7) && (row.Cells[col.ColumnIndex].Value.ToString() != ""))
                            {
                                row.Cells[col.ColumnIndex].Value = flag;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabUtility.SelectedIndex == 0)
                    SelectRemoveSel(mastGrid, 'N');
                else if (tabUtility.SelectedIndex == 1)
                    SelectRemoveSel(transGrid, 'N');
                else if (tabUtility.SelectedIndex == 2)
                    SelectRemoveSel(reportGrid, 'N');
                else if (tabUtility.SelectedIndex == 3)
                    SelectRemoveSel(utilityGrid, 'N');
                else if (tabUtility.SelectedIndex == 4)
                    SelectRemoveSel(applicationgrid, 'N');
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabUtility.SelectedIndex == 0)
                    SelectRemoveSel(mastGrid, 'Y');

                else if (tabUtility.SelectedIndex == 1)
                    SelectRemoveSel(transGrid, 'Y');
                else if (tabUtility.SelectedIndex == 2)
                    SelectRemoveSel(reportGrid, 'Y');
                else if (tabUtility.SelectedIndex == 3)
                    SelectRemoveSel(utilityGrid, 'Y');
                else if (tabUtility.SelectedIndex == 4)
                    SelectRemoveSel(applicationgrid, 'Y');
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }

        private void applicationgrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cellDoubleClick(applicationgrid);
        }

        private void FrmUserAccess_Shown(object sender, EventArgs e)
        {
            Userlist.Focus();
        }

    }
}
