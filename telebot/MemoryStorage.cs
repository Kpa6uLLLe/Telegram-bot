using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public class MemoryStorage : IStorage
    {
         private List<StorageEntity> storageEntities = new List<StorageEntity>(0);

        public StorageEntity GetEntity()
        {


            return new StorageEntity();
        }

        public StorageEntity[] GetEntityList()
        {
            return new StorageEntity[0];
        }
        public void StoreEntity(StorageEntity storageEntity)
        {
            storageEntities.Add(storageEntity);
        }
    }
}
