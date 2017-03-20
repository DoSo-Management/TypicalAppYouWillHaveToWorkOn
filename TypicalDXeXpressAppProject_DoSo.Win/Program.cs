using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            Tracing.Initialize();
            TypicalDXeXpressAppProject_DoSoWindowsFormsApplication winApplication = new TypicalDXeXpressAppProject_DoSoWindowsFormsApplication();

            string connectionString = SQLiteConnectionProvider.GetConnectionString("DoSo_SampleXAFApp7.db");
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);

            winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("DoSo_Logo.png");
            //if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            //{
            //    winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //}

            winApplication.ConnectionString = connectionString;

#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            if (System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
            try
            {
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.HandleException(e);
            }
        }
    }
}
