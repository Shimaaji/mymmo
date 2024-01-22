﻿using Managers;
using Common.Data;
using Entities;
using SkillBridge.Message;

namespace Battle
{
    public class Skill
    {
        public NSkillInfo Info;
        public Creature Owner;
        public SkillDefine Define;

        private float cd = 0;
        private float castTime = 0;

        public bool IsCasting = false;

        public float CD
        {
            get { return cd; }
        }

        public Skill(NSkillInfo info, Creature owner)
        {
            this.Info = info;
            this.Owner = owner;
            this.Define = DataManager.Instance.Skills[(int)this.Owner.Define.Class][this.Info.Id];
            this.cd = 0;
        }

        public SkillResult CanCast(Creature target)
        {
            
            //判断目标类型
            if(this.Define.CastTarget == Common.Battle.TargetType.Target)
            {
                if (target == null || target == this.Owner)
                    return SkillResult.InvalidTarget;
            }

            //判断释放位置
            if (this.Define.CastTarget == Common.Battle.TargetType.Position && BattleManager.Instance.CurrentPosition == null)
            {
                return SkillResult.InvalidTarget;
            }
            
            //判断当前MP
            if (this.Owner.Attributes.MP < this.Define.MPCost)
            {
                return SkillResult.OutOfMp;
            }

            //判断技能CD
            if(this.cd > 0)
            {
                return SkillResult.CoolDown;
            }
            
            return SkillResult.Ok;

        }

        public void BeginCast()
        {
            this.IsCasting = true;
            this.castTime = 0;
            this.cd = this.Define.CD;

            this.Owner.PlayAnim(this.Define.SkillAnim);
        }

        public void OnUpdate(float delta)
        {
            if (this.IsCasting)
            {
            }
            UpdateCD(delta);
        }

        private void UpdateCD(float delta)
        {
            if(this.cd > 0)
            {
                this.cd -= delta;
            }
            if(cd < 0)
            {
                this.cd = 0;
            }
        }
    }
}

