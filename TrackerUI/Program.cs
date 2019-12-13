using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main( )
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );

            //Initialize database connection
            GlobalConfig.InitializeConnection( db: DatabaseType.TextFile );
            Application.Run( new CreateTeamForm( ) );

            //Application.Run( new TournamentDashboardForm( ) );
        }
    }
}