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
                Console.WriteLine("Error message: \n" + ex.StackTrace);
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

        public bool DB_Login(String username, String password)
        {
            //SqlDataReader dataReader;
            //DAO dao = new DAO();
            String sql = @"select * from dbo.Login where userName = '" + username + "' and password = '" + password + "'";
            dataReader = this.DB_ExcuteQuery(sql);
           if(dataReader.Read())
            {
                Console.WriteLine($"Username {dataReader.GetValue(0)}");
                Console.WriteLine($"Password {dataReader.GetValue(1)}");
                return true;
            }

            this.cnn.Close();
            dataReader.Close();
            return false;
        }

        public bool DB_OrderDCard(String id)
        {
            String sql = @"select * from dbo.Order_list where ID_user = '"+id+"'";
            dataReader = this.DB_ExcuteQuery(sql);
            if(dataReader.Read())
            {
                Console.WriteLine($"IDUser {dataReader.GetValue(0)}");
                Console.WriteLine($"NumberCard {dataReader.GetValue(1)}");
                Console.WriteLine($"ShippingAddress {dataReader.GetValue(2)}");
                Console.WriteLine($"ExportPrice {dataReader.GetValue(3)}");
                Console.WriteLine($"TradingCode {dataReader.GetValue(4)}");
                return true;
            }
            this.cnn.Close();
            dataReader.Close();
            return false;
        }
        
        public bool DB_SignUp(String Id, String Username, String Email, String Password)
        {
            try
            {
            String sql = $"exec Pro_SignUp @ID = '{Id}', @Username = '{Username}', @Email = '{Email}', @Password = '{Password}'";
            dataReader = this.DB_ExcuteQuery(sql);
          
                this.cnn.Close();
                dataReader.Close();
                return true;
            
            } catch (Exception ex)
            {
                Console.WriteLine("102: "+ex.Message);
            }
            this.cnn.Close();
            dataReader.Close();
            return false;

        }

        public bool DB_UpdateProfile(String id, String email)
        {
            try
            {
                String sql = $"exec Pro_UpdateProfile @ID = '{id}', @Email = '{email}'";
                dataReader = this.DB_ExcuteQuery(sql);
                this.cnn.Close();
                dataReader.Close();
                return true;
            } catch(Exception ex)
            {
                Console.WriteLine("120: " + ex.Message);
            }
            this.cnn.Close();
            return false;
        }
        

    }


}
