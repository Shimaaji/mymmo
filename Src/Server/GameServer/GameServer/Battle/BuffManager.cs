using Common.Data;
using GameServer.Battle;
using GameServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Managers
{
    public class BuffManager
    {
        private Creature Owner;
        private List<Buff> Buffs = new List<Buff>();

        private int idx = 1;
        private int BuffID
        {
            get { return this.idx++; }
        }

        public BuffManager(Creature owner)
        {
            this.Owner = owner;
        }

        internal void AddBuff(BattleContext context, BuffDefine define)
        {
            Buff buff = new Buff(this.BuffID, this.Owner, define, context);
            this.Buffs.Add(buff);
        }

        internal void Update()
        {
            for(int i = 0; i < this.Buffs.Count; i++)
            {
                if (!this.Buffs[i].Stopped)
                {
                    this.Buffs[i].Update();
                }
            }
            this.Buffs.RemoveAll((b) => b.Stopped);
        }
    }
}
