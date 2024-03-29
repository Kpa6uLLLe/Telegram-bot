﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Options;
namespace telebot
{
    public class DBSqlServerStorage : IStorage
    {
        
        private SqlConnection _connection;
        private const string ALL_LINKS = "Все";
        private ULinksContext _context;

        public DBSqlServerStorage(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
            _context = new ULinksContext();
        }
        private List<string> GetLinksDB(string categoryName, long userId)
        {
            List<string> list = new List<string>(0);
            Open();
            string sqlc = $"SELECT Url FROM Links WHERE CategoryName = '{categoryName}' AND UserId = '{userId}'";
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
            string sqlc = $"SELECT Name FROM Categories WHERE UserId = '{userId}'";
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
        public bool UserExist(string nickname)
        {
            nickname = nickname.ToLower();
            bool result = true;
            Open();
            string sqlc = $"SELECT UserId FROM Users WHERE Nickname = '{nickname}'";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (!reader.HasRows)
                result = false;

            reader.Close();
            Close();
            return result;
        }
            public bool UserExist(long userId)
        {
            bool result = true;
            Open();
            string sqlc = $"SELECT UserId FROM Users WHERE UserId = '{userId}'";
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
            bool result = true;
            Open();
            string sqlc = $"SELECT Name FROM Categories WHERE UserId = '{userId}' AND Name = '{name}'";
            SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (!reader.HasRows)
                result = false;

            reader.Close();
            Close();
            return result;
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
            return new Dictionary<long, Dictionary<string, StorageEntity>>();
        }
        public string GetEntityNames(long userId)
        {
            string names = "\n";
            Open();
            string sqlc = $"SELECT Name FROM Categories WHERE UserId = '{userId}'";
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
                StorageEntity entity = GetEntity(category, userId);
                if (entity != null)
                result += "\t" + entity.GetLinksString() + "\n";
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
            if (!CategoryExist(storageEntity.Name, userId))
            {
                Open();
                string sqlc = $"SELECT UserId FROM Users WHERE UserId = '{userId}'";
                //$"INSERT INTO Categories(Name,UserId) VALUES ('{storageEntity.Name}','{userId}')";
                SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    Close();
                    return;
                }
                reader.Close();
                sqlc = $"INSERT INTO Categories(Name,UserId) VALUES ('{storageEntity.Name}','{userId}')";
                sqlCommand = new SqlCommand(sqlc, _connection);
                sqlCommand.ExecuteNonQuery();
                foreach (string link in linkList)
                {
                    sqlc = $"INSERT INTO Links(Url,CategoryName,UserId) VALUES ('{link}','{storageEntity.Name}','{userId}')";
                    sqlCommand = new SqlCommand(sqlc, _connection);
                    sqlCommand.ExecuteNonQuery();
                }
                Close();
            }
            else
            {
                Open();
                string sqlc = $"SELECT Url FROM Links WHERE UserId = '{userId}' AND CategoryName = '{storageEntity.Name}'";
                SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string url = reader[0].ToString();
                    if (linkList.Contains(url))
                    {
                        linkList.Remove(url);

                    }

                }
                reader.Close();

                foreach (string link in linkList)
                {

                    sqlc = $"INSERT INTO Links(Url,CategoryName,UserId) VALUES ('{link}','{storageEntity.Name}','{userId}')";
                    sqlCommand = new SqlCommand(sqlc, _connection);
                    sqlCommand.ExecuteNonQuery();
                }
                
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
            string localNickname = nickname.ToLower();
            if (UserExist(localNickname) || UserExist(userId))
                return;
            if (!UserExist(userId))
            {
                AppSettings settings = new AppSettings();
                string domain = settings.GetDomainName();
                string email = nickname + "@" + domain;
                Open();
                string sqlc = $"DELETE FROM AspNetUsers WHERE Id = '{userId.ToString()}'";
                SqlCommand sqlCommand = new SqlCommand(sqlc, _connection);
                sqlCommand.ExecuteScalar();
                sqlc =
                        $"INSERT INTO AspNetUsers(AccessFailedCount,TwoFactorEnabled,LockoutEnabled,PhoneNumberConfirmed, Id ,EmailConfirmed,BotAPIUserId,FirstName,LastName,Nickname,Password,UserName,NormalizedUserName,Email,NormalizedEmail,PasswordHash,SecurityStamp) VALUES ('{0}','FALSE','TRUE','FALSE','{userId.ToString()}','FALSE','{userId}','{firstName}','{lastName}','{nickname}','{password}','{email}','{email.ToUpper()}','{email}','{email.ToUpper()}','{HashingUnhashing.HashPassword(password)}','{DateTime.Now.ToString()}');";

                sqlCommand = new SqlCommand(sqlc, _connection);
                sqlCommand.ExecuteNonQuery();
                sqlc = $"SET IDENTITY_INSERT Users ON;INSERT INTO Users(UserId,FirstName,LastName,Nickname,Password) VALUES ('{userId}','{firstName}','{lastName}','{nickname}','{password}');SET IDENTITY_INSERT Users OFF";
                sqlCommand = new SqlCommand(sqlc, _connection);
                sqlCommand.ExecuteScalar();

                Close();

            }
        }
    }
}
