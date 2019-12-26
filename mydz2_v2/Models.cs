using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace mydz2_v2
{
    public class Model
    {
        protected bool inDB = false;
        protected int lol = 5;
        public int id;

        protected Model()
        {
        }

        public void delete()
        {
            string query = $"Delete from {this.GetType().Name} WHERE id = {id}";
            DBConnection.execute(query);
            inDB = false;
        }
        public bool isInDB() { return inDB; }

    }

    public class Users:Model
    {
        public string debug;

        //public int id;
        public string ulogin;
        public string fname;
        public string lname;
        public string email;
        private float cash = 0;
        private bool super_user = false;
        //private bool inDB = false;
        public string upassword;

        public Users() {
      
        }
        public Users(int id1)
        {
            DataTable dt = (DBConnection.getDT($"Select * from [Users] " +
                                       $"WHERE id={id1}"));
            if (dt.Rows.Count != 0)
            {
                id = dt.Rows[0].Field<int>(0);
                ulogin = dt.Rows[0].Field<String>(1);
                upassword = dt.Rows[0].Field<String>(2);
                fname = dt.Rows[0].Field<String>(3);
                lname = dt.Rows[0].Field<String>(4);
                email = dt.Rows[0].Field<String>(5);
                super_user = dt.Rows[0].Field<bool>(6);
                cash = dt.Rows[0].Field<int>(7);

                inDB = true;
            }
        }
        public Users(string login1)
        {
            DataTable dt = (DBConnection.getDT($"Select * from [Users] " +
                                       $"WHERE ulogin='{login1}'"));
            if (dt.Rows.Count != 0)
            {
                id = dt.Rows[0].Field<int>(0);
                ulogin = dt.Rows[0].Field<String>(1);
                upassword = dt.Rows[0].Field<String>(2);
                fname = dt.Rows[0].Field<String>(3);
                lname = dt.Rows[0].Field<String>(4);
                email = dt.Rows[0].Field<String>(5);
                super_user = dt.Rows[0].Field<bool>(6);
                cash = dt.Rows[0].Field<int>(7);
                inDB = true;
            }
        }


        public void save()
        {
            string query = "";
            if (!inDB)
            {
                query = $"INSERT INTO {this.GetType().Name} VALUES('{ulogin}', '{upassword}', " +
                                                                    $"'{fname}', '{lname}', '{email}', 0, 0)";
                debug = query;
                inDB = true;
            }
            else
            {
                query = $"UPDATE TABLE {this.GetType().Name} SET ($ulogin ={ulogin}," +
                                                                $"upassword={upassword}" +
                                                                $"fist_name= '{fname}'," +
                                                                $"last_name= '{lname}'," +
                                                                $"email={email}," +
                                                                $"super_user=0" +
                                                                $"cash= 0" +
                                                            $") WHERE id={id.ToString()}";
            }
            DBConnection.execute(query);
        }
        /*
        public void delete()
        {
            string query = $"Delete from {this.GetType().Name} WHERE id = {id}";
            DBConnection.execute(query);
            inDB = false;
        }
        */
        public static DataTable showAll()
        {
            return (DBConnection.getDT($"Select id,ulogin, fname, lname, email, super_user, cash from [Users]"));
        }
        public static DataTable findByValue(string column, string value)
        {
            return (DBConnection.getDT($"Select ulogin, fname, lname, email, super_user, cash from [Users] " +
                                        $"WHERE {column}={value}"));
        }

        public bool isSuperUser() { return super_user; }
        /*
        public string getQuery()
        {
            return ($"INSERT INTO {this.GetType().Name} VALUES('{ulogin}', '{upassword}', " +
                                                                    $"'{fname}', '{lname}', '{email}', 0, 0)");
        }
        */

    }

    public class Event:Model
    {
        //public int id;
        public string eventType;
        public string place;
        public DateTime date;
        //private bool inDB = false;
        private DataTable asDT;

        public Event() { }
        public Event(int id1)
        {
            DataTable dt = (DBConnection.getDT($"Select * from [Event] " +
                           $"WHERE id={id1}"));
            asDT = dt;
            if (dt.Rows.Count != 0)
            {
                id = dt.Rows[0].Field<int>(0);
                eventType = dt.Rows[0].Field<String>(1);
                place = dt.Rows[0].Field<String>(2);
                date = dt.Rows[0].Field<DateTime>(3);


                inDB = true;
            }
        }

        public void save()
        {
            string query = "";
            if (!inDB)
            {
                query = $"INSERT INTO {this.GetType().Name} VALUES('{eventType}', '{place}', " +
                                                                    $"'{date.ToString("yyyy-MM-dd HH:mm:ss")}')";

            }
            else
            {
                query = $"UPDATE {this.GetType().Name} SET eventType='{eventType}'," +
                                                           $"place='{place}'," +
                                                           $"date= '{date.ToString("yyyy-MM-dd HH:mm")}'" +
                                                           $" WHERE id={id.ToString()}";
            }
            DBConnection.execute(query);
            inDB = true;
        }
        /*
        public void delete()
        {
            string query = $"Delete from {this.GetType().Name} WHERE id = {id}";
            DBConnection.execute(query);
            inDB = false;
        }
        */

        public static DataTable showAll()
        {
            DataTable dt = DBConnection.getDT($"Select * from [Event]");
            return dt;
        }

        public DataTable asDataTable()
        {
            return asDT;
        }

    }
    class Test
    {
        public int id;
        public string name;
        private bool inDB = false;

        public Test(){}
        public Test(string name_)
        {
            name = name_;
        }

        public void save()
        {
            string query = "";
            if (!inDB)
            {
                query = $"INSERT INTO TEST VALUES('{name}')";
                inDB = true;
            }
            else
            {
                query = @"ALTER TABLE TEST SET name= '" + name + @"') WHERE id=" + id.ToString();
            }
            //string query = @"INSERT INTO TEST VALUES(""als"")";
            DBConnection.execute(query);
        }
        public void delete()
        {
            string query = $"Delete from {this.GetType().Name} WHERE id = {id}";
            DBConnection.execute(query);
        }
        public static DataTable showAll()
        {
            return (DBConnection.getDT(@"Select * from [Test]"));
        }

    }
}
