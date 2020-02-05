using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MiniBillingServer.Model
{
    class SilkDB
    {
        private static SilkDB m_instance = null;
        private SqlConnection cnn;

        // Dirty singleton antipattern <3
        public static SilkDB Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new SilkDB();
                }

                return m_instance;
            }
        }

        public SilkData GetSilkData(int UserJID)
        {

            string query = "DECLARE @ReturnValue int ";
            query += "DECLARE @SilkOwn int ";
            query += "DECLARE @SilkGift int ";
            query += "DECLARE @Mileage int ";
            query += "EXEC @ReturnValue = _GetSilkDataForGameServer " + UserJID + ", @SilkOwn OUTPUT, @SilkGift OUTPUT, @Mileage OUTPUT ";
            query += "SELECT @ReturnValue, @SilkOwn, @SilkGift, @Mileage";

            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    reader.Read();

                    if ((int)reader[0] != 0)
                    {
                        throw new Exception("Error getting value");
                    }

                    Console.WriteLine("DB Result ({0}): {1} {2} {3} {4}", UserJID, (int)reader[0], (int)reader[1], (int)reader[2], (int)reader[3]);

                    return new SilkData((int)reader[1], (int)reader[2], (int)reader[3]);
                }
            }
        }

        private SilkDB()
        {
            DatabaseConfiguration dbcfg = new DatabaseConfiguration("Settings/config.ini");
            
            string connectionString = "Data Source=" + dbcfg.Host + ";Initial Catalog=" + dbcfg.Database + ";User ID=" + dbcfg.Username + ";Password=" + dbcfg.Password;
            cnn = new SqlConnection(connectionString);

            try { 
                cnn.Open();
                Console.WriteLine("Connection to DB successful.");
            } 
            catch (Exception ex) {
                Console.WriteLine("Error: Could not connect to database");
                throw ex;
            }
        }

        public void Init()
        {
            // We do nothing here, the singleton will do all the work
            // We just need a func to call
        }
    }
}
