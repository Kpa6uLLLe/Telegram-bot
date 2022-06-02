﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Data.SqlClient;
namespace telebot
{
    public class DBSqlServerStorage : IStorage
    {
        private Dictionary<long, Dictionary<string, StorageEntity>> _entities = new Dictionary<long, Dictionary<string, StorageEntity>>();
        private SqlConnection _connection;
        private const string ALL_LINKS = "Все";

        public DBSqlServerStorage(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }
        private List<string> GetLinksDB(string categoryName, long userId)
        {
            Open();
            List<string> list = new List<string>(0);
            string sqlc = $"SELECT Url FROM Links WHERE CategoryName = {categoryName} AND UserId = {userId}";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                list.Add(reader[0].ToString());
            }
            reader.Close();
            Close();
            return list;
        }
        private List<string>  GetCategoriesListDB(long userId)
        {
            Open();
            List<string> list = new List<string>(0);
            list.Add(ALL_LINKS);
            string sqlc = $"SELECT Name FROM Categories WHERE UserId = {userId}";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader[0].ToString());
            }
            reader.Close();
            Close();
            return list;
        }
        private void Open()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
        }
        private void Close()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
        public bool UserExist(long userId)
        {
            bool result = true;
            Open();
            string sqlc = $"SELECT UserId FROM Users WHERE UserId = {userId}";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (!reader.HasRows)
                result = false;
            
            reader.Close();
            Close();
            return result;
        }
        private bool CategoryExist(string name, long userId)
        {
            Open();
            string sqlc = $"SELECT Name FROM Categories WHERE UserId = {userId} AND Name = {name}";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.FieldCount == 0)
                return false;
            reader.Close();
            Close();
            return true;
        }
        public StorageEntity GetEntity(string name, long userId)
        {
            if (!UserExist(userId))
                return null;
            if (CategoryExist(name, userId))
            {
                StorageEntity entity = new StorageEntity();
                entity.AddLinks(GetLinksDB(name, userId));
                entity.Name = name;
                return entity;
            }
            return null;
        }

        public Dictionary<long, Dictionary<string, StorageEntity>> GetEntityList()
        {
            return _entities;
        }
        public string GetEntityNames(long userId)
        {
            string names = "\n";
            Open();
            string sqlc = $"SELECT Name FROM Categories WHERE UserId = {userId}";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                names += reader[0].ToString() + "\n";
            }
            reader.Close();
            Close();
            return names;
        }
        public string GetLinkList(string categoryName, long userId)
        {
            StorageEntity storageEntity = GetEntity(categoryName, userId);
            if (storageEntity == null) return null;
            return storageEntity.GetLinksString();
        }
        public string GetEntityList(long userId)
        {
            string result = "\nВсе ссылки:\n";
            List<string> categories = GetCategoriesListDB(userId);
            foreach (string category in categories)
            {
                result += "\t" + GetEntity(category, userId).GetLinksString() + "\n";
            }
            /*foreach (System.Collections.Generic.KeyValuePair<string, StorageEntity> entity in entities[userId])
            {
                result += "\t" + entity.Value.GetLinksString() + "\n";
            }*/
            return result;
        }
        public void StoreEntity(StorageEntity storageEntity, long userId)
        {
            List<string> linkList = storageEntity.GetLinks();
            if (!UserExist(userId))
                return;
            if(!CategoryExist(storageEntity.Name,userId))
            {
                Open();
                string sqlc = $"INSERT INTO Categories VALUES ({storageEntity.Name},{userId});";
                SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
                sqlCommand.ExecuteNonQuery();
                foreach(string link in linkList)
                {
                    sqlc = $"INSERT INTO Links(Url,CategoryName,UserId) VALUES ({link},{storageEntity.Name},{userId});";
                    sqlCommand = new SqlCommand(sqlc, _connection);
                    sqlCommand.ExecuteNonQuery();
                }
                Close();
            }
            else
            {
                Open();
                string sqlc = $"SELECT Url FROM Links WHERE UserId = {userId} AND CategoryName = {storageEntity.Name}";
                SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string url = reader[0].ToString();
                    if (!linkList.Contains(url))
                    {
                        sqlc = $"INSERT INTO Links(Url,CategoryName,UserId) VALUES ({url},{storageEntity.Name},{userId});";
                        sqlCommand = new SqlCommand(sqlc, _connection);
                        sqlCommand.ExecuteNonQuery();
                    }

                }
                reader.Close();
                Close();
                }
            /*if (!entities[userId].ContainsKey(storageEntity.Name))
            {
                entities[userId].Add(storageEntity.Name, storageEntity);
            }
            else
                entities[userId][storageEntity.Name] = storageEntity;
            */
        }
        public void CreateNewUser(long userId, string firstName, string lastName, string nickname, string password)
        {
            if (!UserExist(userId))
            {
                Open();
                string sqlc = $"INSERT INTO Users VALUES (@UserId,@FirstName,@LastName,@Nickname,@Password)";
                SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                sqlCommand.Parameters.AddWithValue("@Nickname", nickname);
                sqlCommand.Parameters.AddWithValue("@Password", password);
                sqlCommand.ExecuteNonQuery();
                Close();

            }
        }
    }
}
