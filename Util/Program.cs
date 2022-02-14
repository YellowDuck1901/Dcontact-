using Util;
using System.Data.SqlClient;

SqlDataReader dataReader;
DAO dao = new DAO();
string sql = @"SELECT * FROM [dbo].[Login]";
dataReader = dao.DB_ExcuteQuery(sql);
while (dataReader.Read())
{
    //MessageBox.Show(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
    Console.WriteLine($"your username is: {dataReader.GetValue(0)}");
    Console.WriteLine($"your password is: {dataReader.GetValue(1)}");
}
dao.cnn.Close();
dataReader.Close();
