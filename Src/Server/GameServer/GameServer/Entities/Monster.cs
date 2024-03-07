using GameServer.Core;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Entities
{
    public class Monster : Creature
    {
        Creature Target;
        public Monster(int tid, int level, Vector3Int pos, Vector3Int dir) : base(CharacterType.Monster, tid, level, pos, dir)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        protected override void OnDamage(NDamageInfo damage, Creature source)
        {
            if(this.Target == null)
            {
                this.Target = source;
            }
        }
    }
}
