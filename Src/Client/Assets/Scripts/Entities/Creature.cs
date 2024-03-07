using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Battle;
using Battle;
using Common.Battle;
using Common.Data;
using SkillBridge.Message;

using UnityEngine;

namespace Entities
{
    public class Creature : Entity
    {
        public NCharacterInfo Info;

        public CharacterDefine Define;

        public Attributes Attributes;

        public SkillManager SkillMgr;
        public BuffManager BuffMgr;
        public EffectManager EffectMgr;

        public Action<Buff> OnBuffAdd;
        public Action<Buff> OnBuffRemove;

        bool battleState = false;
        public bool BattleStatus
        {
            get { return battleState; } 
            set 
            {
                if(battleState != value)
                {
                    battleState = value;
                    this.SetStandby(value);
                }
            }
        }

        public int Id
        {
            get { return this.Info.Id; }
        }

        public Skill CastringSkill = null;

        public string Name
        {
            get
            {
                if (this.Info.Type == CharacterType.Player)
                    return this.Info.Name;
                else
                    return this.Define.Name;
            }
        }

        public bool IsPlayer
        {
            get 
            {
                return this.Info.Type == CharacterType.Player;
            }
        }

        public bool IsCurrentPlayer
        {
            get
            {
                if (!IsPlayer) return false;
                return this.Info.Id == Models.User.Instance.CurrentCharacterInfo.Id;
            }
        }

        public Creature(NCharacterInfo info) : base(info.Entity)
        {
            this.Info = info;
            this.Define = DataManager.Instance.Characters[info.ConfigId];
            this.Attributes = new Attributes();
            this.Attributes.Init(this.Define, this.Info.Level, this.GetEquips(), this.Info.attrDynamic);
            this.SkillMgr = new SkillManager(this);
            this.BuffMgr = new BuffManager(this);
            this.EffectMgr = new EffectManager(this);
        }

        public void UpdateInfo(NCharacterInfo info)
        {
            this.SetEntityData(info.Entity);
            this.Info = info;
            this.Attributes.Init(this.Define, this.Info.Level, this.GetEquips(), this.Info.attrDynamic);
            this.SkillMgr.UpdateSkills();
        }

        public virtual List<EquipDefine> GetEquips()
        {
            return null;
        }

        public void MoveForward()
        {
            Debug.LogFormat("MoveForward");
            this.speed = this.Define.Speed;
        }

        public void MoveBack()
        {
            Debug.LogFormat("MoveBack");
            this.speed = -this.Define.Speed;
        }

        public void Stop()
        {
            Debug.LogFormat("Stop");
            this.speed = 0;
        }

        public void SetDirection(Vector3Int direction)
        {
            Debug.LogFormat("SetDirection:{0}", direction);
            this.direction = direction;
        }

        public void SetPosition(Vector3Int position)
        {
            Debug.LogFormat("SetPosition:{0}", position);
            this.position = position;
        }

        public void CastSkill(int skillId, Creature target, NVector3 pos)
        {
            this.SetStandby(true);
            var skill = this.SkillMgr.GetSkill(skillId);
            skill.BeginCast(target, pos);
        }

        public void PlayAnim(string name)
        {
            if(this.Controller != null)
                this.Controller.PlayAnim(name);
        }

        public void SetStandby(bool standBy)
        {
            if (this.Controller != null)
                this.Controller.SetStandby(standBy);
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            this.SkillMgr.OnUpdate(delta);
            this.BuffMgr.OnUpdate(delta);
        }

        public void DoDamage(NDamageInfo damage, bool playHurt)
        {
            Debug.LogFormat("DoDamage:{0} DMG:{1} CRT:{2}", this.Name, damage.Damage, damage.Crit);
            this.Attributes.HP -= damage.Damage;
            if(playHurt) this.PlayAnim("Hurt");
            if(this.Controller != null)
            {
                UIWorldElementManager.Instance.ShowPopupText(PopupType.Damage, this.Controller.GetTransform().position + this.GetPopupOffset(), -damage.Damage, damage.Crit);
            }
        }

        internal void DoSkillHit(NSkillHitInfo hit)
        {
            var skill = this.SkillMgr.GetSkill(hit.skillId);
            skill.DoHit(hit);
        }

        internal int Distance(Creature target)
        {
            return (int)Vector3Int.Distance(this.position, target.position);
        }

        internal void DoBuffAction(NBuffInfo buff)
        {
            switch (buff.Action)
            {
                case BuffAction.None:
                    break;
                case BuffAction.Add:
                    this.AddBuff(buff.buffId, buff.buffType, buff.casterId);
                    break;
                case BuffAction.Remove:
                    this.RemoveBuff(buff.buffId);
                    break;
                case BuffAction.Hit:
                    this.DoDamage(buff.Damage, false);
                    break;
                default:
                    break;
            }
        }

        private void AddBuff(int buffId, int buffType, int casterId)
        {
            var buff = this.BuffMgr.AddBuff(buffId, buffType, casterId);
            if(buff != null && this.OnBuffAdd != null)
            {
                this.OnBuffAdd(buff);
            }
        }

        public void RemoveBuff(int buffId)
        {
            var buff = this.BuffMgr.RemoveBuff(buffId);
            if(buff != null && this.OnBuffRemove != null)
            {
                this.OnBuffRemove(buff);
            }
        }


        internal void AddBuffEffect(BuffEffect effect)
        {
            this.EffectMgr.AddEffect(effect);
        }

        internal void RemoveBuffEffect(BuffEffect effect)
        {
            this.EffectMgr.RemoveEffect(effect);
        }

        internal void FaceTo(Vector3Int position)
        {
            this.SetDirection(GameObjectTool.WorldToLogic(GameObjectTool.LogicToWorld(position - this.position).normalized));
            this.UpdateEntityData();
            if(this.Controller != null)
                this.Controller.UpdateDirection();
        }

        internal void PlayEffect(EffectType type, string name, Creature target, float duration = 0)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (this.Controller != null)
                this.Controller.PlayEffect(type, name, target, duration);
        }

        public void PlayEffect(EffectType type, string name, NVector3 position)
        {
            if(string.IsNullOrEmpty(name)) return;
            if (this.Controller != null)
                this.Controller.PlayEffect(type, name, position, 0);
        }

        public Vector3 GetPopupOffset()
        {
            return new Vector3(0, this.Define.Height, 0);
        }

        internal Vector3 GetHitOffset()
        {
            return new Vector3(0, this.Define.Height * 0.8f, 0);
        }
    }
}
