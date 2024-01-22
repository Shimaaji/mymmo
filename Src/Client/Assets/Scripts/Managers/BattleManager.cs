using Battle;
using Entities;
using Services;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    public class BattleManager : Singleton<BattleManager>
    {
        public delegate void TargetChangedHandler(Creature target);
        public event TargetChangedHandler OnTargetChanged;

        private Creature currentTarget;
        public Creature CurrentTarget
        {
            get { return currentTarget; }
            set
            {
                this.SetTarget(value);
            }
        }

        private NVector3 currentPosition;
        public NVector3 CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                this.SetPosition(value);
            }
        }

        public void Init()
        {

        }

        public void SetTarget(Creature target)
        {
            if (this.currentTarget != target && this.OnTargetChanged != null)
                this.OnTargetChanged(target);

            this.currentTarget = target;
            Debug.LogFormat("BattleManager SetTarget:[{0}:{1}]", target.entityId, target.Name);
        }

        public void SetPosition(NVector3 position)
        {
            this.currentPosition = position;
            Debug.LogFormat("BattleManager SetTarget:[{0}]", position);
        }

        public void CastSkill(Skill skill)
        {
            int target = currentTarget != null ? currentTarget.entityId : 0;
            BattleService.Instance.SendSkillCast(skill.Define.ID, skill.Owner.entityId, target, currentPosition);
        }
    }
}
