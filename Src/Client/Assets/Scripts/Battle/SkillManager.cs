using Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SkillManager
    {
        Creature Owner;
        public List<Skill> Skills { get; private set; }

        public SkillManager(Creature owner)
        {
            this.Owner = owner;
            this.Skills = new List<Skill>();
            this.InitSkills();
        }

        private void InitSkills()
        {
            this.Skills.Clear();
            foreach (var skillInfo in this.Owner.Info.Skills)
            {
                Skill skill = new Skill(skillInfo, this.Owner);
                this.AddSkill(skill);
            }
        }

        public void UpdateSkills()
        {
            foreach (var skillInfo in this.Owner.Info.Skills)
            {
                Skill skill = this.GetSkill(skillInfo.Id);
                if (skill != null)
                    skill.Info = skillInfo;
                else
                    this.AddSkill(skill);
            }
        }

        public void AddSkill(Skill skill)
        {
            this.Skills.Add(skill);
        }

        public Skill GetSkill(int skillId)
        {
            for(int i = 0; i < this.Skills.Count; i++)
            {
                if (this.Skills[i].Define.ID == skillId)
                    return this.Skills[i];
            }
            return null;
        }

        public void OnUpdate(float delta)
        {
            for (int i = 0; i < this.Skills.Count; i++)
            {
                this.Skills[i].OnUpdate(delta);
            }
        }
    }

}
