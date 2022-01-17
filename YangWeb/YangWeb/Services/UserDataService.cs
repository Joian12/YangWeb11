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
            MySqlCommand commm = new MySqlCommand("insert into usermodel (Username, Password, Level, MaxExperience, CurrentExperience, Health, " +
                "Score, Armor, Damage) values ('"+user.Username+"' , '"+user.Password+"', 1, 150, 0, 250, 0, 1, 55)", conn);
            

            commm.ExecuteNonQuery();
            commm.Dispose();
            conn.Close();
        }

        public void GetData(string username)
        {
            conn.Open();
            MySqlCommand comm = new MySqlCommand("select * from usermodel");
            comm.ExecuteNonQuery();
            comm.Dispose();
            conn.Close();
        }

        public PlayerStatModel GetPlayerStats()
        {
            conn.Open();
            MySqlCommand comm = new MySqlCommand("select Username, Level, MaxExperience, CurrentExperience, Health, Score, Armor, Damage from usermodel where Username = '"+UserSession.GetUserSession()+"'", conn);
            MySqlDataReader reader = comm.ExecuteReader();

            PlayerStatModel player = new PlayerStatModel();
            if (reader.HasRows)
            {   
                while (reader.Read())
                {
                    player.Level = Convert.ToInt32(reader[1]);
                    player.MaxExperience = Convert.ToSingle(reader[2]);
                    player.CurrentExperience = Convert.ToSingle(reader[3]);
                    player.Health = Convert.ToSingle(reader[4]);
                    player.Score = Convert.ToInt32(reader[5]); 
                    player.Armor = Convert.ToSingle(reader[6]);
                    player.Damage = Convert.ToSingle(reader[7]);
                }   
            }
            else
                System.Diagnostics.Debug.WriteLine("No rows founds");

            conn.Close();
            return player;
        }

        public void SetPlayerStats(PlayerStatModel player)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("update usermodel " +
                "set Level = '"+player.Level+"'," +
                    "MaxExperience = '"+player.MaxExperience+"'," +
                    "CurrentExperience = '" + player.CurrentExperience + "'," +
                    "Health = '" +player.Health+"'," +
                    "Score = '"+player.Score+"'," +
                    "Armor = '"+player.Armor+"'," +
                    "Damage = '"+player.Damage+ "' where  Username = '" + UserSession.GetUserSession()+"'; ",conn);

            command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
        }

        public bool CheckLogin(UserModel user)
        {
            bool isValid = false;
            conn.Open();
            MySqlCommand command = new MySqlCommand("select * from usermodel where Username = '"+user.Username+"' and Password = '"+user.Password+"'", conn);
            MySqlDataReader reader = command.ExecuteReader();
            isValid = reader.HasRows ? true : false;
            command.Dispose();
            conn.Close();
           
            return isValid;
        }

    }
}
