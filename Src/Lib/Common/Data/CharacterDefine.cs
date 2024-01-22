using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillBridge.Message;

namespace Common.Data
{
    public class CharacterDefine
    {
        public int TID { get; set; }
        public string Name { get; set; }
        public CharacterClass Class { get; set; }
        public string Resource { get; set; }
        public string Description { get; set; }
        //基本属性
        public int Speed { get; set; }
        //生命
        public float MaxHP {  get; set; }
        //法力
        public float MaxMP { get; set; }
        //力量成长
        public float GrowthSTR { get; set; }
        //智力成长
        public float GrowthINT { get; set; }
        //敏捷成长
        public float GrowthDEX { get; set; }
        //力量
        public float STR { get; set; }
        //智力
        public float INT { get; set; }
        //敏捷
        public float DEX { get; set; }
        //物理攻击
        public float AD { get; set; }
        //法术攻击
        public float AP { get; set; }
        //物理防御
        public float DEF { get; set; }
        //魔法防御
        public float MDEF { get; set; }
        //攻击速度
        public float SPD { get; set; }
        //暴击概率
        public float CRI { get; set; }
    }
}
