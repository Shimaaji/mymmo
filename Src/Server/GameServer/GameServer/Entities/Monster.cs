using Common.Battle;
using GameServer.AI;
using GameServer.Battle;
using GameServer.Core;
using GameServer.Models;
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
        AIAgent AI;

        public Map Map;
        private Vector3Int moveTarget;
        Vector3 movePosition;
        public Monster(int tid, int level, Vector3Int pos, Vector3Int dir) : base(CharacterType.Monster, tid, level, pos, dir)
        {
            this.AI = new AIAgent(this);
        }

        public void OnEnterMap(Map map)
        {
            this.Map = map;
        }

        public override void Update()
        {
            base.Update();
            this.UpdateMovement();
            this.AI.Update();
        }

        public Skill FindSkill(BattleContext context, SkillType type)
        {
            Skill canCast = null;
            foreach (var skill in this.SkillMgr.Skills)
            {
                if ((skill.Define.Type & type) != skill.Define.Type) continue;
                var result = skill.CanCast(context);
                if (result == SkillResult.Casting)
                    return null;
                if(result == SkillResult.Ok)
                {
                    canCast = skill;
                    break;
                }
            }

            return canCast;
        }

        protected override void OnDamage(NDamageInfo damage, Creature source)
        {
            if(this.AI != null)
            {
                this.AI.OnDamage(damage, source);
            }
        }

        internal void MoveTo(Vector3Int position)
        {
            if(State == CharacterState.Idle)
            {
                State = CharacterState.Move;
            }
            if(this.moveTarget != position)
            {
                this.moveTarget = position;
                this.movePosition = Position;
                var dist = (this.moveTarget - this.Position);

                this.Direction = dist.normalized;
                this.Speed = this.Define.Speed;

                NEntitySync sync = new NEntitySync();
                sync.Entity = this.EntityData;
                sync.Event = EntityEvent.MoveFwd;
                sync.Id = this.entityId;

                this.Map.UpdateEntity(sync);
            }
        }

        private void UpdateMovement()
        {
            if(State == CharacterState.Move)
            {
                if(this.Distance(this.moveTarget) < 50)
                {
                    this.StopMove();
                }

                if(this.Speed > 0)
                {
                    Vector3 dir = this.Direction;
                    this.movePosition += dir * this.Speed * Time.deltaTime / 100;
                    this.Position = this.movePosition;
                }
            }
        }

        internal void StopMove()
        {
            this.State = CharacterState.Idle;
            this.moveTarget = Vector3Int.zero;
            this.Speed = 0;

            NEntitySync sync = new NEntitySync();
            sync.Entity = this.EntityData;
            sync.Event = EntityEvent.Idle;
            sync.Id = this.entityId;

            this.Map.UpdateEntity(sync);
        }


    }
}
