﻿using System;
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
            MySqlCommand commm = new MySqlCommand("insert into usermodel (Username, Password, Health, " +
                "Score) values ('"+user.Username+"' , '"+user.Password+"', 150, 0)", conn);
            

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
            MySqlCommand comm = new MySqlCommand("select Username, Health, Score from usermodel where Username = '"+UserSession.GetUserSession()+"'", conn);
            MySqlDataReader reader = comm.ExecuteReader();

            PlayerStatModel player = new PlayerStatModel();
            if (reader.HasRows)
            {   
                while (reader.Read())
                {
                    player.Health = Convert.ToSingle(reader[1]);
                    player.Score = Convert.ToInt32(reader[2]); 
                }   
            }
            else
                System.Diagnostics.Debug.WriteLine("No rows founds");

            conn.Close();
            return player;
        }

        public void SetPlayerStats(PlayerStatModel player)
        {
            System.Diagnostics.Debug.WriteLine("Saved");
            conn.Open();
            MySqlCommand command = new MySqlCommand("update usermodel set Health = '" + player.Health + "', Score = '"+player.Score+ "' where Username = '" + UserSession.GetUserSession() + "'; ", conn);

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
