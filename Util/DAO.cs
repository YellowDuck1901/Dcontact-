using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Bean;

namespace Util
{
    public class DAO
    {
        string stringConnection = @"Data Source = localhost;    Initial Catalog = Dcontact; User ID = sa; Password=123456 ; integrated security = True; Encrypt=False";
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
                status = false;
                throw new Exception($"Util.DAO 30: connect fail!\n{ex.StackTrace}");
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
                throw new Exception();
            }
            return null;
        }

        public bool DB_Login(String username, String password)
        {
            try
            {
                string sql = $"exec Pro_Login @Username = '{username}', @Password = '{password}'";
                this.DB_ExcuteQuery(sql);
                this.cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Util.DAO 63: \n{ex.StackTrace}");

            }
            this.cnn.Close();
            return false;
        }

        public bool DB_OrderDCard(String id)
        {
            String sql = @"select * from dbo.Order_list where ID_user = '" + id + "'";
            dataReader = this.DB_ExcuteQuery(sql);
            if (dataReader.Read())
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

            }
            catch (Exception ex)
            {
                Console.WriteLine("102: " + ex.Message);
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
                this.DB_ExcuteQuery(sql);
                this.cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("120: " + ex.Message);
            }
            this.cnn.Close();
            return false;
        }
        public Bean.User DB_getUser(string id, string username)
        {
            User user = null;
            String sql = $"execute Pro_getUser @ID = '{id}'";
            try
            {
                dataReader = this.DB_ExcuteQuery(sql);
                if (dataReader.Read())
                {
                    user = new User();
                    user.id = (string)dataReader.GetValue(0);

                    user.username = username;
                    user.email = (string)dataReader.GetValue(1);
                    user.isban = (bool)dataReader.GetValue(2);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("142:" + ex.Message);
            }
            this.cnn.Close();
            dataReader.Close();
            return user;
        }

        public bool
    }


}
