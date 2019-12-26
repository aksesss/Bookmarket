using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace mydz2_v2
{
    class DBConnection
    {   
        private static string connection_str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\als\source\repos\mydz2_v2\mydz2_v2\Bookmark.mdf;Integrated Security=True";    //Строка подключения
        public static SqlConnection con = new SqlConnection(connection_str);
        //   private string dbstr = ''
        public DBConnection()
        {
        }

        public static void execute(string query)
        {
            con.Open();
            
            SqlCommand sqlcommand = new SqlCommand(query, con);
            sqlcommand.ExecuteNonQuery();
            //SqlDataAdapter da = new SqlDataAdapter(sqlcommand);
            //da.Fill();

            con.Close();

        }

        public static DataTable getDT(string query)
        {
            try
            {
                con.Open();
                SqlCommand sqlcommand = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(sqlcommand);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch(Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }

        }
    }
}
