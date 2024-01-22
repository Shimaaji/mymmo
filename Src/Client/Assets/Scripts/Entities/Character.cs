using Common.Data;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Character : Creature
    {
        public Character(NCharacterInfo info) : base(info)
        {
        }

        public override List<EquipDefine> GetEquips()
        {
            return EquipManager.Instance.GetEquipedDefines();
        }
    }
}
