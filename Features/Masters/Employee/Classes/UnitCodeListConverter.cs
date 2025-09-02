using TimeAttendanceManager.Main.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static System.ComponentModel.TypeConverter;

namespace TimeAttendanceManager.Features.Masters.Employee.Classes
{
    public class UnitCodeListConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> unitCodes = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ClassGlobalVariables.pubConnectionString))
                {
                    conn.Open();
                    string sql = @"SELECT UnitCode 
                               FROM dbo.Companies 
                               WHERE IsActive = 1 AND IsDeleted = 0
                               ORDER BY UnitCode";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            unitCodes.Add(reader["UnitCode"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // fallback (or log error)
                unitCodes.Add("ERROR: " + ex.Message);
            }

            return new StandardValuesCollection(unitCodes);
        }
    }
}
