using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using ASPL.CLASSES;
using System.IO;
using System.Net;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ASPL.LODGE.REPORTS.CrystalReports;


namespace ASPL.LODGE.REPORTS
{
    public partial class FrmReportShow : Form
    {
       // ReportRoomList rptRoom = new ReportRoomList();
        
        Globalvariable gv = new Globalvariable();
        Design ObjDes = new Design();
        Connection ObjCon = new Connection();
        Module mod = new Module();
        string address, address1, address2, address3, contact, contact1, contact2;
        public FrmReportShow()
        {
            InitializeComponent();
        }


        private void FrmReportShow_Load(object sender, EventArgs e)
        {
            //ObjDes.SetScreenResolution(this);      
            try
            {
                if (Globalvariable.frmName == "Room List")
                {
                    RptRoomList rptRoom = new RptRoomList();
                    crystalReportViewer1.ReportSource = rptRoom;
                    rptRoom.Refresh();
                    rptRoom.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Tax List")
                {
                    RptTaxList rpt = new RptTaxList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Party List")
                {
                    RptPartyList rpt = new RptPartyList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Groups Rpt List")
                {
                    RptGrpList rpt = new RptGrpList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Employee List")
                {
                    RptEmployeeList rpt = new RptEmployeeList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Item Coding List")
                {
                    RptItemCodingList rpt = new RptItemCodingList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Discount List")
                {
                    RptDiscountList rpt = new RptDiscountList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Guest List")
                {
                    RptGuestList rpt = new RptGuestList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "General Ledger Rpt List")
                {
                    RptGeneralLedgList rpt = new RptGeneralLedgList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
                else if (Globalvariable.frmName == "Day Book Rpt List")
                {
                    RptDayBookList rpt = new RptDayBookList();
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    rpt.SetDatabaseLogon("sa", "aryan05");
                    WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception ex)
            {
                string str = "Message:" + ex.Message;
                MessageBox.Show(str, "Error Message");
            }
        }
    }
}
