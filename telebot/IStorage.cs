using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public interface IStorage
    {
        public StorageEntity GetEntity(string name, long userId);
        public void StoreEntity(StorageEntity storageEntity, long userId);

        public Dictionary<long, Dictionary<string, StorageEntity>> GetEntityList();

        public string GetEntityNames(long userId);
        public string GetLinkList(string categoryName, long userId);
        public string GetEntityList(string categoryName, long userId);

        public void CreateNewUser(long userId);
    }
}
