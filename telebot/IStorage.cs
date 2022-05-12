using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public interface IStorage
    {
        public StorageEntity GetEntity();
        public void StoreEntity(StorageEntity storageEntity);

        public StorageEntity[] GetEntityList();
    }
}
