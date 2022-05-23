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
         private Dictionary<string, StorageEntity> entities = new Dictionary<string, StorageEntity>();

        public MemoryStorage()
        {
        }
        public StorageEntity GetEntity(string name)
        {
            if(entities.ContainsKey(name))
            return entities[name];
            return null;
        }

        public Dictionary<string, StorageEntity> GetEntityList()
        {
            return entities;
        }
        public string GetEntityNames()
        {
            string names = "\n";
            foreach(System.Collections.Generic.KeyValuePair<string, StorageEntity> entity in entities)
            {
                names += entity.Key + "\n";
            }
            return names;
        }
        public string GetLinkList(string categoryName)
        {
            StorageEntity storageEntity = GetEntity(categoryName);
            if (storageEntity==null) return null;
            return storageEntity.GetLinksString();
        }
        public string GetEntityList(string categoryName)
        {
            string result = "\nВсе ссылки:\n";
            foreach (System.Collections.Generic.KeyValuePair<string, StorageEntity> entity in entities)
            {
                result+= "\t" + entity.Value.GetLinksString() + "\n";
            }
            return result;
        }
            public void StoreEntity(StorageEntity storageEntity)
        {
            if(!entities.ContainsKey(storageEntity.Name))
                entities.Add(storageEntity.Name, storageEntity);
            else
                entities[storageEntity.Name] = storageEntity;
            
        }

    }
}
