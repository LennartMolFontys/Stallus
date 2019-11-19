﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Main_computer
{
    public class Database
    {
        public string ConnectionString { get; private set; }
        private MySqlConnection Connection;

        public Database(string connectionString = "Server=studmysql01.fhict.local;Uid=dbi413213;Database=dbi413213;Pwd=helmond;")
        {
            ConnectionString = connectionString;
            Connection = new MySqlConnection(connectionString);
        }
        
        public DbCheck IsDatabaseReachable()
        {
            try
            {
                Connection.Open();
                Connection.Close();
                return new DbCheck(true);
            }
            catch (MySqlException ex)
            {
                return new DbCheck(false, ex);
            }
        }

        public bool Registrate(string firstName, string lastName, DateTime date_of_birth, string email_address, string password, Address address)
        {
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO users (`first_name`, `last_name`, `date_of_birth`, `email_address`, `password`, `physical_address`) VALUES (@1, @2, @3, @4, @5, @6);";
            cmd.Parameters.AddWithValue("@1", firstName);
            cmd.Parameters.AddWithValue("@2", lastName);
            cmd.Parameters.AddWithValue("@3", date_of_birth);
            cmd.Parameters.AddWithValue("@4", email_address);
            cmd.Parameters.AddWithValue("@5", password);
            cmd.Parameters.AddWithValue("@6", address);
            Connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Connection.Close();
                return true;
            }
            else
            {
                Connection.Close();
                return false;
            }
        }

        public string RetrieveUserID(string email_address)
        {
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT `userid` FROM `users` WHERE email_address = @1;";
            cmd.Parameters.AddWithValue("@1", email_address);
            Connection.Open();
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            string userid;
            if (reader.HasRows)
            {
                reader.Read();
                userid = reader.GetString(0);
            }
            else
            {
                userid = "null";
            }
            Connection.Close();
            return userid;
        }

        public bool EmailAlreadyInUse(string email_address)
        {
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT `email_address` FROM `users` WHERE email_address LIKE @1;";
            cmd.Parameters.AddWithValue("@1", email_address);
            Connection.Open();
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Connection.Close();
                return true;
            }
            Connection.Close();
            return false;
        }

        public string RetrievePassword(string email_address)
        {
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT `password` FROM `users` WHERE email_address LIKE @1;";
            cmd.Parameters.AddWithValue("@1", email_address);
            Connection.Open();
            var reader = cmd.ExecuteReader();
            string password;
            if (reader.HasRows)
            {
                reader.Read();
                password = reader.GetString(0);
            }
            else
            {
                password = "null";
            }
            Connection.Close();
            return password;
        }

        public bool UpdateUserDetails(string userid, string[] columnNames, string[] newValues)
        {
            string query = "UPDATE `users` SET ";
            for (int i = 0; i < columnNames.Length; i++)
            {
                if ((i + 1) == columnNames.Length)
                {
                    query += $"{columnNames[i]} = '{newValues[i]}' ";
                }
                else
                {
                    query += $"{columnNames[i]} = '{newValues[i]}', ";
                }
            }
            query += $"WHERE userid = {userid}";
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = query;
            Connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected > 0;
        }


    }
}
