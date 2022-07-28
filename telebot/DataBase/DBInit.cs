using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;
namespace telebot
{
    public class DBInit
    {
        public ULinksContext UserDBContext;
        public DBSqlServerStorage _storage;
        public DBInit()
        {
            UserDBContext = new ULinksContext();
            SqlConnection connection = new SqlConnection(UserDBContext.DbPath);
             _storage = new DBSqlServerStorage(connection);
        }

    }
}
