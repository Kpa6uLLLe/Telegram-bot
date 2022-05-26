using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace telebot
{
    public class MemoryStorage : IStorage
    {
        private Dictionary<long, Dictionary<string, StorageEntity>> entities = new Dictionary<long, Dictionary<string, StorageEntity>>();

        public MemoryStorage()
        {
        }
        public StorageEntity GetEntity(string name, long userId)
        {
            if (!entities.ContainsKey(userId))
                return null;
            if (entities[userId].ContainsKey(name))
                return entities[userId][name];
            return null;
        }

        public Dictionary<long, Dictionary<string, StorageEntity>> GetEntityList()
        {
            return entities;
        }
        public string GetEntityNames(long userId)
        {
            string names = "\n";
            foreach (System.Collections.Generic.KeyValuePair<string, StorageEntity> entity in entities[userId])
            {
                names += entity.Key + "\n";
            }
            return names;
        }
        public string GetLinkList(string categoryName, long userId)
        {
            StorageEntity storageEntity = GetEntity(categoryName, userId);
            if (storageEntity == null) return null;
            return storageEntity.GetLinksString();
        }
        public string GetEntityList(string categoryName, long userId)
        {
            string result = "\nВсе ссылки:\n";
            foreach (System.Collections.Generic.KeyValuePair<string, StorageEntity> entity in entities[userId])
            {
                result += "\t" + entity.Value.GetLinksString() + "\n";
            }
            return result;
        }
        public void StoreEntity(StorageEntity storageEntity, long userId)
        {
            if (!entities.ContainsKey(userId))
                entities.Add(userId, new Dictionary<string, StorageEntity>());
            if (!entities[userId].ContainsKey(storageEntity.Name))
            {
                entities[userId].Add(storageEntity.Name, storageEntity);
            }
            else
                entities[userId][storageEntity.Name] = storageEntity;

        }
        public void CreateNewUser(long userId)
        {
            if (!entities.ContainsKey(userId))
                entities.Add(userId, new Dictionary<string, StorageEntity>());
        }
    }
}
