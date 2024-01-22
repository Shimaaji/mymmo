using GameServer.Entities;
using GameServer.Managers;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Battle
{
    public class SkillManager
    {
        private Creature Owner;

        public List<Skill> Skills { get; private set; }
        public List<NSkillInfo> Infos { get; private set; }

        public SkillManager(Creature owner) 
        {
            this.Owner = owner;
            this.Skills = new List<Skill>();//存放当前逻辑可以执行的Skill
            this.Infos  = new List<NSkillInfo>();//为了方便，存放用于往客户端发送的SkillInfos
            this.InitSkills();
        }

        private void InitSkills()
        {
            this.Skills.Clear();
            this.Infos.Clear();

            if (!DataManager.Instance.Skills.ContainsKey(this.Owner.Define.TID))
                return;

            foreach (var define in DataManager.Instance.Skills[this.Owner.Define.TID])
            {
                NSkillInfo info = new NSkillInfo();
                info.Id = define.Key;
                if(this.Owner.Info.Level >= define.Value.UnlockLevel)
                {
                    info.Level = 5;
                }
                else
                {
                    info.Level = 1;
                }
                this.Infos.Add(info);
                Skill skill = new Skill(info, this.Owner);
                this.AddSkills(skill);
            }
        }

        private void AddSkills(Skill skill)
        {
            this.Skills.Add(skill);
        }
    }
}
