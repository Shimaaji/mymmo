using GameServer.Entities;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Battle
{
    class BattleContext
    {
        public Battle battle;
        public Creature Caster;
        public Creature Target;

        public NSkillCastInfo CastSkill;
        public NDamageInfo Damage;

        public SkillResult Result;

        public BattleContext(Battle battle)
        {
            this.battle = battle;
        }
    }
}
