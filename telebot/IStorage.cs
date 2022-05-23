using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public interface IStorage
    {
        public StorageEntity GetEntity(string name);
        public void StoreEntity(StorageEntity storageEntity);

        public Dictionary<string, StorageEntity> GetEntityList();

        public string GetEntityNames();
        public string GetLinkList(string categoryName);
        public string GetEntityList(string categoryName);
    }
}
