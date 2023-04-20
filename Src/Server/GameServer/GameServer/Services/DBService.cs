using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace GameServer.Services
{
    class DBService : Singleton<DBService>
    {
        ExtremeWorldEntities entities;

        public ExtremeWorldEntities Entities
        {
            get { return this.entities; }
        }

        public void Init()
        {
            entities = new ExtremeWorldEntities();
        }

        
        //同步或异步保存
        public void Save(bool async = false)
        {
            if (async)
                entities.SaveChangesAsync();
            else
                entities.SaveChanges();

        }
    }
}
