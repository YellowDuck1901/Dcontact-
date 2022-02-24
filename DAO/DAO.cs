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
        SqlDataReader dataReader;
        SqlCommand command;
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
        void Close()
        {
            this.cnn.Close();
            this.dataReader.Close();
        }
        public SqlDataReader DB_ExcuteQuery(string sql)
        {

            SqlDataReader dataReader;


            command = new SqlCommand(sql, this.cnn);
            try
            {
                dataReader = command.ExecuteReader();  //thuc hien cau lenh reader
                command.Dispose();
                return dataReader;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public bool DB_Login(String username, String password)
        {
            try
            {
                string sql = $"exec Pro_Login @Username = '{username}', @Password = '{Util.MD5.CreateMD5(password)}'";
                dataReader= this.DB_ExcuteQuery(sql);
                dataReader.Close();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }

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

            dataReader.Close();
            return false;
        }

        public bool DB_SignUp(String Username, String Email, String Password)
        {
            try
            {
                String sql = $"exec Pro_SignUp @ID = '{Util.MD5.CreateMD5(Username)}', @Username = '{Username}', @Email = '{Email}', @Password = '{Util.MD5.CreateMD5(Password)}'";
                dataReader = this.DB_ExcuteQuery(sql);
                dataReader.Close();
                return true;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }

            return false;

        }

        public bool DB_UpdateProfile(String id, String email)
        {
            try
            {
                String sql = $"exec Pro_UpdateProfile @ID = '{id}', @Email = '{email}'";
                this.DB_ExcuteQuery(sql);

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public Bean.User DB_getUser(string username)
        {
            User user = null;
            string id = Util.MD5.CreateMD5(username);
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
                    user.dcontact = this.DB_GetDcontact(id);
                }
                dataReader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            return user;
        }
        public Dcontact DB_GetDcontact(string id)
        {
            Dcontact dcontact = null;
            List<Row> rows = new List<Row>();
            string sql = $"exec Pro_getDContact @ID = '{id}'";
            dataReader = DB_ExcuteQuery(sql);
            if (dataReader.Read())
            {
                dcontact = new Dcontact();
                dcontact.numerView = (string)dataReader.GetValue(0).ToString();
                dcontact.avt = (string)dataReader.GetValue(1);
                dcontact.background = (string)dataReader.GetValue(2);

                dataReader.Close();

                string sqlr = $"select r.[text] , r.font , r.link, r.bullet, r.click, r.code, r.birth  from dbo.[Row] as r  where id_contact = '{id}'";
                dataReader = DB_ExcuteQuery(sqlr);
                List<Row> r = new List<Row>();

                while (dataReader.Read())
                {
                    Row a = new Row();
                    a.text = (string)dataReader.GetValue(0);
                    a.font = (string)dataReader.GetValue(1);
                    a.link = (string)dataReader.GetValue(2);
                    a.bullet = (string)dataReader.GetValue(3);
                    a.click = (string)dataReader.GetValue(4).ToString();
                    a.code = (string)dataReader.GetValue(5).ToString();
                    a.birth = (string)dataReader.GetValue(6).ToString();
                    r.Add(a);
                }
                dcontact.rows = r;
                dataReader.Close();
            }
            return dcontact;
        }
    }


}
