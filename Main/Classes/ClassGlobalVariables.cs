using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TimeAttendanceManager.Main.Classes.ClassGlobalFunctions;

namespace TimeAttendanceManager.Main.Classes
{
   public static class ClassGlobalVariables
    {
        // Database connection strings
        public static string mySQLDataBasePathNgp = Properties.Settings.Default.ConnectionStringNGP;
        public static string mySQLDataBasePathMdk = Properties.Settings.Default.ConnectionStringMDK;
        public static string mySQLDataBasePathBmt = Properties.Settings.Default.ConnectionStringBMT;

        public static string myPubConnectionStringSynergyMDK = Properties.Settings.Default.DbSynergyConnectionStringMDK;
        public static string myPubConnectionStringSynergyNGP = Properties.Settings.Default.DbSynergyConnectionStringNGP;
        public static string myPubConnectionStringSynergyBMT = Properties.Settings.Default.DbSynergyConnectionStringBMT;

        // Programmer info
        public const string pubProgrammer = "Anil K. Waghmare / M Maheswara Rao / Satish Maturu";
        public const string pubProgrammerEmails = "mailto:anil@gtnindustries.com;maheswararao@imperialgarments.in;satish_m@gtnengineering.net;";

        // Database settings
        public static string myDbProvider = Properties.Settings.Default.DatabaseServer;
        public static string myDataBaseName = Properties.Settings.Default.DatabaseName;

        // Network information
        public static string pubHostIPAddress = GetLocalIPV4();

        // User settings
        public static string pubUnitCode = Properties.Settings.Default.PlantCode;
        public static string pubUserLoginName = Properties.Settings.Default.UserLoginName;
        public static string pubUserRole = null;
        public static int? pubLoginUserRowId = null;
        public static string pubLoginUserRowGuid = string.Empty;
        public static string pubConnectionString = string.Empty;
        public static string pubOrganisationName = string.Empty;

        // System information
        public static string pubMachineName = Environment.MachineName;
        public static string pubDNSHostName = string.Empty;

        // Database info
        public static string pubDatabaseName = string.Empty;

        // Collections
        public static Dictionary<string, ClassUserSession> pubUserDictionary = new Dictionary<string, ClassUserSession>();

        // Connection mapping
        public static readonly Dictionary<string, string> unitConnectionMap = new Dictionary<string, string>
        {
            {"1400", Properties.Settings.Default.ConnectionStringNGP},
            {"1500", Properties.Settings.Default.ConnectionStringBMT},
            {"3000", Properties.Settings.Default.ConnectionStringMDK}
        };

        // UI settings
        public static bool pubShowConfirmation = false;
        public static int pubShowNotification = 30;

        // Enum
        public enum PubYeNoStatus
        {
            No = 0,
            Yes = 1
        }

        // sql server system date
        public static DateTime? SqlServerTodayDate;
    }
}
