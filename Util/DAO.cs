using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Util
{
    public class DAO
    {
        string stringConnection = @"Data Source = localhost;    Initial Catalog = Dcontact; User ID = sa; Password=123456";
        public SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        public bool status;
        public DAO()
        {
            cnn = new SqlConnection(stringConnection);
            try
            {
                cnn.Open();
                Console.WriteLine("connect success!");
                status = true;
                //cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("connect fail!");
                Console.WriteLine("Error message: " + ex);
                status = false;
            }

        }

        public SqlDataReader DB_ExcuteQuery(string sql)
        {
            command = new SqlCommand(sql, this.cnn);
            try
            {
                dataReader = command.ExecuteReader();
                command.Dispose();
                return dataReader;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error 44: " + ex);
            }
            return null;
        }

    }
}
