using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;
using System.Configuration;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnection( DatabaseType db )
        {
            if ( db == DatabaseType.Sql )
            {
                //TODO: setup SQL connector
                SQLConnector sql = new SQLConnector();
                Connection = sql;
            }
            else if ( db == DatabaseType.TextFile )
            {
                //TODO: setup Text connector
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        public static string CnnString( string name )
        {
            return ConfigurationManager.ConnectionStrings[ name ].ConnectionString;
        }
    }
}