using GameServer.Core;
using GameServer.Entities;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Battle
{
    public class BattleContext
    {
        public Battle Battle;
        public Creature Caster;
        public Creature Target;

        public NSkillCastInfo CastSkill;

        public Vector3Int Position { get { return this.CastSkill.Position; } } 
        public SkillResult Result;

        public BattleContext(Battle battle)
        {
            this.Battle = battle;
        }
    }
}
