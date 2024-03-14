using Common.Battle;
using Common.Data;
using GameServer.Battle;
using GameServer.Core;
using GameServer.Managers;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Entities
{
    public class Creature : Entity
    {

        public int Id { get; set; }
        public string Name { get { return this.Info.Name; } }
        
        public NCharacterInfo Info;
        public CharacterDefine Define;

        public Attributes Attributes;
        public SkillManager SkillMgr;
        public BuffManager BuffMgr;
        public EffectManager EffectMgr;

        public bool IsDeath = false;

        //战斗状态（是否在战斗）
        public BattleState BattleState;

        //移动状态（是否在移动）
        public CharacterState State;

        public Creature(CharacterType type, int configId, int level, Vector3Int pos, Vector3Int dir) :
           base(pos, dir)
        {
            this.Define = DataManager.Instance.Characters[configId];

            this.Info = new NCharacterInfo();
            this.Info.Type = type;
            this.Info.Level = level;
            this.Info.ConfigId = configId;
            this.Info.Entity = this.EntityData;
            this.Info.EntityId = this.entityId;
            this.Info.Name = this.Define.Name;
            this.InitSkills();
            this.InitBuffs();
            this.Attributes = new Attributes();
            this.Attributes.Init(this.Define, this.Info.Level, this.GetEquips(), this.Info.attrDynamic);
            this.Info.attrDynamic = this.Attributes.DynamicAttr;
        }

        internal int Distance(Creature target)
        {
            return (int)Vector3Int.Distance(this.Position, target.Position);
        }

        internal int Distance(Vector3Int position)
        {
            return (int)Vector3Int.Distance(this.Position, position);
        }

        internal void DoDamage(NDamageInfo damage, Creature source)
        {
            this.BattleState = BattleState.InBattle;
            this.Attributes.HP -= damage.Damage;
            if (this.Attributes.HP < 0)
            {
                this.IsDeath = true;
                damage.WillDead = true;
            }
            this.OnDamage(damage, source);
        }

        private void InitSkills()
        {
            SkillMgr = new SkillManager(this);
            this.Info.Skills.AddRange(this.SkillMgr.Infos);
        }

        private void InitBuffs()
        {
            BuffMgr = new BuffManager(this);
            EffectMgr = new EffectManager(this);
        }

        public virtual List<EquipDefine> GetEquips()
        {
            return null;
        }

        internal void CastSkill(BattleContext context, int skillId)
        {
            Skill skill = this.SkillMgr.GetSkill(skillId);
            context.Result = skill.Cast(context);
            if(context.Result == SkillResult.Ok)
            {
                this.BattleState = BattleState.InBattle;
            }

            //CastSkill如果是客户端传送过来的，即玩家释放的，那么一定不为空，如果是怪物CastSkill，因为怪物CastSkill是服务端new出来的，所以一定为空
            if(context.CastSkill == null)
            {
                //判断怪物技能是否释放成功
                if(context.Result == SkillResult.Ok)
                {
                    context.CastSkill = new NSkillCastInfo()
                    {
                        casterId = this.entityId,
                        targetId = context.Target.entityId,
                        skillId = skill.Define.ID,
                        Position = new NVector3(),
                        Result = context.Result,
                    };
                    context.Battle.AddCastSkillInfo(context.CastSkill);
                }
            }
            else
            {
                context.CastSkill.Result = context.Result;
                context.Battle.AddCastSkillInfo(context.CastSkill);
            }
        }

        public override void Update()
        {
            this.SkillMgr.Update();
            this.BuffMgr.Update();
        }

        internal void AddBuff(BattleContext context, BuffDefine buffDefine)
        {
            this.BuffMgr.AddBuff(context, buffDefine);
        }
        protected virtual void OnDamage(NDamageInfo damage, Creature source)
        {
            
        }

    }
}
