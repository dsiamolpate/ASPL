using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ASPL.STARTUP.FORMS; 
using ASPL.CLASSES;
using ASPL.HOTEL.Transactions;


namespace ASPL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmRegistration());
            
           
        }
    }
}
