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
            catch (Exception)
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
                this.dataReader = this.DB_ExcuteQuery(sql);
                this.dataReader.Close();
                return true;
            }
            catch (SqlException)
            {
                throw;

            }

        }


        public bool DB_checkExistedEmail(string email)
        {
            try
            {
                String sql = $"exec Pro_existedEmail @email = '{email}'";
                this.dataReader = this.DB_ExcuteQuery(sql);
                this.dataReader.Close();
                return true;
            }
            catch (SqlException)
            {
                throw;

            }
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
            catch (SqlException)
            {
                throw;

            }

            return false;

        }

        public bool DB_UpdateProfile(String id, String email)
        {
            try
            {
                String sql = $"exec Pro_UpdateProfile @ID = '{id}', @Email = '{email}'";
                this.dataReader = this.DB_ExcuteQuery(sql);
                this.dataReader.Close();
                return true;
            }
            catch (SqlException)
            {
                throw;

            }
        }
        public Bean.User DB_getUser(string username)
        {
            User user = null;
            string id = Util.MD5.CreateMD5(username);
            String sql = $"execute Pro_getUser @ID = '{id}'";
            try
            {
                this.dataReader = this.DB_ExcuteQuery(sql);
                if (dataReader.Read())
                {
                    user = new User();
                    user.id = dataReader.GetValue(0).ToString();
                    user.username = username;
                    user.email = dataReader.GetValue(1).ToString();
                    user.isban = dataReader.GetBoolean(2);
                    this.dataReader.Close();
                    user.dcontact = this.DB_GetDcontact(user.id);
                }
                //this.dataReader.Close();
            }
            catch (SqlException)
            {
                throw;

            }
            return user;
        }
        public Dcontact DB_GetDcontact(string id)
        {
            Dcontact dcontact = null;
            List<Row> rows = new List<Row>();
            string sql = $"exec Pro_getDContact @ID = '{id}'";
            this.dataReader = DB_ExcuteQuery(sql);
            if (this.dataReader.Read())
            {
                dcontact = new Dcontact();
                dcontact.numerView = dataReader.GetValue(0).ToString();
                dcontact.avt = dataReader.GetValue(1).ToString();
                dcontact.background = dataReader.GetValue(2).ToString();

                this.dataReader.Close();

                string sqlr = $"select r.ID, r.[text] , r.font , r.link, r.bullet, r.click, r.code, r.birth,r.rowColor  from dbo.[Row] as r  where id_contact = '{id}'";
                this.dataReader = DB_ExcuteQuery(sqlr);
                List<Row> r = new List<Row>();

                while (dataReader.Read())
                {
                    Row a = new Row();
                    a.ID = dataReader.GetValue(0).ToString();
                    a.text = dataReader.GetValue(1).ToString();
                    a.font = dataReader.GetValue(2).ToString();
                    a.link = dataReader.GetValue(3).ToString();
                    a.bullet = dataReader.GetValue(4).ToString();
                    a.click = dataReader.GetValue(5).ToString();
                    a.code = dataReader.GetValue(6).ToString();
                    a.birth = dataReader.GetValue(7).ToString();
                    a.color = dataReader.GetValue(8).ToString();
                    r.Add(a);
                }
                dcontact.rows = r;
                this.dataReader.Close();
            }
            return dcontact;
        }

        public bool DB_ChangePass(string email, string newPass)
        {
            try
            {
                string sql = $"execute PRO_ChangePassword @email='{email}', @password ='{MD5.CreateMD5(newPass)}' ";
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
        }

        public bool DB_DelRow(string idRow, string idDcontact)
        {
            try
            {
                string sql = $"execute Pro_deleteRow @idRow='{idRow}', @idContact='{idDcontact}' ";
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
        }

        public bool DB_UpdateRow(string idRow, string idContact, string text, string font, string rowColor,
            string link, string bullet, string code, string birth, string click)
        {
            try
            {
                string sql = $"execute Pro_UpdateRow @idRow='{idRow}', @idContact='{idContact}'," +
                    $"@text='{text}',@font='{font}', @rowColor='{rowColor}', @link = '{link}', @bullet = '{bullet}'," +
                    $"@code = {code}, @birth = '{birth}', @click = {click}";
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
        }

        public bool DB_AddRow(string idRow, string idContact, string text, string font, string rowColor,
            string link, string bullet, string code, string birth, string click)
        {
            try
            {
                string sql = $"exec Pro_AddRow @idRow = '{idRow}', @idContact = '{idContact}', @text = '{text}', @font = '{font}'," +
                            $"@rowColor = '{rowColor}', @link = '{link}', @bullet = '{bullet}', @code = {code}, " +
                            $"@birth = '{birth}', @click = {click}";
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
        }
        public bool DB_AddOrder(string id_user, string address, string phone, string number, string credit, string cvv, string expdate, string price, string data)
        {
            string sql = $"EXECUTE dbo.PRO_AddOrder @ID_User='{id_user}',  @address ='{address}',  @phone='{phone}',  @number='{number}',  @credit='{credit}',  @cvv='{cvv}',  @expdate='{expdate}', @price='{price}',  @data='{data}'";
            try
            {
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool DB_IsAdmin(String username)
        {
            bool b = false;
            try
            {
                string sql = $"select isAdmin from dbo.Account where username = '{username}'";
                this.dataReader = this.DB_ExcuteQuery(sql);
                if (dataReader.Read())
                {
                    b = (bool)dataReader.GetBoolean(0);
                    this.dataReader.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return b;
        }

        public Bean.User DB_getAdmin(string username)
        {
            User admin = null;
            string id = Util.MD5.CreateMD5(username);
            string sql = $"select u.ID, u.Email, u.isBan, a.username ,a.isAdmin from [User] as u, [Account] as a where u.ID = a.ID and u.ID = '{id}'";
            try
            {
                this.dataReader = this.DB_ExcuteQuery(sql);
                if (dataReader.Read())
                {
                    admin = new User();
                    admin.id = (string)dataReader.GetValue(0);
                    admin.email = (string)dataReader.GetValue(1);
                    admin.username = (string)dataReader.GetValue(3);
                    admin.isAdmin = (bool)dataReader.GetBoolean(4);
                    this.dataReader.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return admin;
        }
        public bool DB_updateAvt(string id, string path)
        {
            string sql = $"EXECUTE dbo.PRO_updateAvt @ID ='{id}', @path = '{path}' ";
            try
            {
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public bool DB_updateTemplate(string id, string path)
        {
            string sql = $"EXECUTE dbo.PRO_updateTemplate @ID ='{id}', @path = '{path}' ";
            try
            {
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public List<string> DB_loadTemplate(string id)
        {
            string sql = $"select background from template where ID_User = '{id}'";
            List<string> paths = new List<string>();
            try
            {
                this.dataReader = DB_ExcuteQuery(sql);
                while (this.dataReader.Read())
                {
                    paths.Add(dataReader.GetValue(0).ToString());
                }
                this.dataReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return paths;
        }

        public string DB_updateCodeRow(string id_row, string code)
        {
            string sql = $"update dbo.[row] set code = '{code}' where id = '{id_row}'";
            try
            {
                this.dataReader = DB_ExcuteQuery(sql);
                this.dataReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return sql;
        }
    }
}
