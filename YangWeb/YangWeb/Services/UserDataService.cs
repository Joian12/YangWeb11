using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using YangWeb.Models;

namespace YangWeb.Services
{
    public class UserDataService
    {
        public MySqlConnection conn; 

        public UserDataService()
        {
            conn = new MySqlConnection("Data Source=127.0.0.1;Initial Catalog=test;User Id=root;Password=;");   
        }

        public void RegisterUser(UserModel user)
        {
            conn.Open();
            MySqlCommand commm = new MySqlCommand("insert into usermodel (Username, Password) values ('"+user.Username+"' , '"+user.Password+"')", conn);
            int check = commm.ExecuteNonQuery();
            conn.Close();
        }

        public void GetData(string username)
        {
            conn.Open();
            MySqlCommand comm = new MySqlCommand("select * from usermodel");
            comm.ExecuteNonQuery();
            conn.Close();
        }

    }
}
