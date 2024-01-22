using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


namespace Managers
{
    class CharacterManager : Singleton<CharacterManager>, IDisposable
    {
        public Dictionary<int, Creature> Characters = new Dictionary<int, Creature>();


        public UnityAction<Creature> OnCharacterEnter;
        public UnityAction<Creature> OncharacterLeave;
        public CharacterManager()
        {

        }

        public void Dispose()
        {
        }

        public void Init()
        {

        }

        public void Clear()
        {
            int[] keys = this.Characters.Keys.ToArray();
            foreach (var key in keys)
            {
                this.RemoveCharacter(key);
            }
            this.Characters.Clear();
        }

        public void AddCharacter(Character character)
        {
            Debug.LogFormat("AddCharacter:{0}:{1} Map:{2} Entity:{3}", character.Id, character.Name, character.Info.mapId, character.Info.Entity);
            this.Characters[character.entityId] = character;
            EntityManager.Instance.AddEntity(character);
            if (OnCharacterEnter!=null)
            {
                OnCharacterEnter(character);
            }
        }


        public void RemoveCharacter(int entityId)
        {
            Debug.LogFormat("RemoveCharacter:{0}", entityId);
            if (this.Characters.ContainsKey(entityId))
            {
                EntityManager.Instance.RemoveEntity(this.Characters[entityId].Info.Entity);
                if (OncharacterLeave != null)
                {
                    OncharacterLeave(this.Characters[entityId]);
                }
                this.Characters.Remove(entityId);
            }
        }

        public Creature GetCharacter(int id)
        {
            Creature character;
            this.Characters.TryGetValue(id, out character);
            return character;
        }
    }
}
