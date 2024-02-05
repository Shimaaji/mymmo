using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Entities;

namespace GameServer.Managers
{
    class EntityManager : Singleton<EntityManager>
    {
        private int idx = 0;
        //public List<Entity> AllEntities = new List<Entity>();
        public Dictionary<int, Entity> AllEntities = new Dictionary<int, Entity>();
        public Dictionary<int, List<Entity>> MapEntities = new Dictionary<int, List<Entity>>();

        public void AddEntity(int mapId, Entity entity)
        {
            //加入实体管理器，生成唯一ID
            entity.EntityData.Id = ++this.idx;
            AllEntities.Add(entity.EntityData.Id, entity);

            List<Entity> entities = null;
            if (!MapEntities.TryGetValue(mapId,out entities))
            {
                entities = new List<Entity>();
                MapEntities[mapId] = entities;
            }
            entities.Add(entity);
        }

        public void RemoveEntity(int mapId,Entity entity)
        {
            this.AllEntities.Remove(entity.entityId);
            this.MapEntities[mapId].Remove(entity);
        }

        public Entity GetEntity(int entityId)
        {
            Entity result = null;
            this.AllEntities.TryGetValue(entityId, out result);
            return result;
        }

        public Creature GetCreature(int entityId)
        {
            return GetEntity(entityId) as Creature;
        }
    }
}
