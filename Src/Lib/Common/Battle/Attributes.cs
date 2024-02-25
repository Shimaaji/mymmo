using Common.Data;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common.Battle
{
    public class Attributes
    {
        AttributeData Initial = new AttributeData();
        AttributeData Growth = new AttributeData();
        AttributeData Equip = new AttributeData();
        public AttributeData Basic = new AttributeData();//Basic 指 角色的初始属性 + 成长的属性 + 所穿装备的属性 所得到的初始属性
        public AttributeData Buff = new AttributeData();
        public AttributeData Final = new AttributeData();//Final指 Basic通过Buff属性的运算得到的最终属性

        int Level;
        public NAttributeDynamic DynamicAttr;
        public float HP
        {
            get { return DynamicAttr.Hp; }
            set { DynamicAttr.Hp = (int)Math.Min(MaxHP, value); }
        }

        public float MP
        {
            get { return DynamicAttr.Mp; }
            set { DynamicAttr.Mp = (int)Math.Min(MaxMP, value); }
        }

        /// <summary>
        /// 最大生命
        /// </summary>
        public float MaxHP { get { return this.Final.MaxHP; } }
        /// <summary>
        /// 最大法力
        /// </summary>
        public float MaxMP { get { return this.Final.MaxMP; } }
        /// <summary>
        /// 力量
        /// </summary>
        public float STR { get { return this.Final.STR; } }
        /// <summary>
        /// 智力
        /// </summary>
        public float INT { get { return this.Final.INT; } }
        /// <summary>
        /// 敏捷
        /// </summary>
        public float DEX { get { return this.Final.DEX; } }
        /// <summary>
        /// 物理攻击
        /// </summary>
        public float AD { get { return this.Final.AD; } }
        /// <summary>
        /// 法术攻击
        /// </summary>
        public float AP { get { return this.Final.AP; } }
        /// <summary>
        /// 物理防御
        /// </summary>
        public float DEF { get { return this.Final.DEF; } }
        /// <summary>
        /// 法术防御
        /// </summary>
        public float MDEF { get { return this.Final.MDEF; } }
        /// <summary>
        /// 攻击速度
        /// </summary>
        public float SPD { get { return this.Final.SPD; } }
        /// <summary>
        /// 暴击概率
        /// </summary>
        public float CRI { get { return this.Final.CRI; } }

        /// <summary>
        /// 初始化角色属性
        /// </summary>
        /// <param name="define">角色配置</param>
        /// <param name="level">等级</param>
        /// <param name="equips">当前装备</param>
        /// <param name="dynamicAttr">当前动态属性</param>
        public void Init(CharacterDefine define, int level, List<EquipDefine> equips, NAttributeDynamic dynamicAttr)
        {
            //当前的动态属性
            this.DynamicAttr = dynamicAttr;
            //加载初始属性
            this.LoadInitAttribute(this.Initial, define);
            //加载成长属性
            this.LoadGrowthAttribute(this.Growth, define);
            //加载装备属性
            this.LoadEquipAttributes(this.Equip, equips);
            
            this.Level = level;

            //计算基础属性，包括力量、智力、敏捷
            this.InitBasicAttributes();
            //计算二级属性
            this.InitSecondaryAttributes();
            //TODO:计算Buff
            //this.InitBuffAttributes();
            this.InitFinalAttributes();
            if (this.DynamicAttr == null)
            {
                this.DynamicAttr = new NAttributeDynamic();
                this.HP = this.MaxHP;
                this.MP = this.MaxMP;
            }
            else
            {
                this.HP = this.DynamicAttr.Hp;
                this.MP = this.DynamicAttr.Mp;
            }
        }

        private void InitBasicAttributes()
        {
            //先将基础属性的值付给Basic
            for(int i = (int)AttributeType.MaxHP; i < (int)AttributeType.MAX; i++)
            {
                this.Basic.Data[i] = this.Initial.Data[i];
            }

            for(int i = (int)AttributeType.STR; i <= (int)AttributeType.DEF; i++)
            {
                //计算一级属性成长，包括力量、智力、敏捷
                this.Basic.Data[i] = this.Initial.Data[i] + this.Growth.Data[i] * (this.Level - 1);
                //计算装备的一级属性,二级属性是根据一级的属性进行计算的
                this.Basic.Data[i] += this.Equip.Data[i];
            }
        }

        private void InitSecondaryAttributes()
        {
            //二级属性成长（包括装备）
            this.Basic.MaxHP = this.Basic.STR * 10 + this.Initial.MaxHP + this.Equip.MaxHP;
            this.Basic.MaxMP = this.Basic.INT * 10 + this.Initial.MaxMP + this.Equip.MaxMP;

            this.Basic.AD = this.Basic.STR * 5 + this.Initial.AD + this.Equip.AD;
            this.Basic.AP = this.Basic.INT * 5 + this.Initial.AP + this.Equip.AP;
            this.Basic.DEF = this.Basic.STR * 2 + this.Basic.DEX * 1 + this.Initial.DEF + this.Equip.DEF;
            this.Basic.MDEF = this.Basic.INT * 2 + this.Basic.DEX * 1 + this.Initial.MDEF + this.Equip.MDEF;

            this.Basic.SPD = this.Basic.DEX * 0.2f + this.Initial.SPD + this.Equip.SPD;
            this.Basic.CRI = this.Basic.DEX * 0.0002f + this.Initial.CRI + this.Equip.CRI;
        }

        private void InitBuffAttributes()
        {
            Debug.Log("待完成");
        }

        public void InitFinalAttributes()
        {
            for (int i = (int)AttributeType.MaxHP; i < (int)AttributeType.MAX; i++)
            {
                this.Final.Data[i] = this.Basic.Data[i] + this.Buff.Data[i];
            }
        }

        private void LoadInitAttribute(AttributeData attr, CharacterDefine define)
        {
            attr.MaxHP = define.MaxHP;
            attr.MaxMP = define.MaxMP;

            attr.STR = define.STR;
            attr.INT = define.INT;
            attr.DEX = define.DEX;
            attr.AD = define.AD;
            attr.AP = define.AP;
            attr.DEF = define.DEF;
            attr.MDEF = define.MDEF;
            attr.SPD = define.SPD;
            attr.CRI = define.CRI;
        }

        private void LoadGrowthAttribute(AttributeData attr, CharacterDefine define)
        {
            attr.STR = define.GrowthSTR;
            attr.INT = define.GrowthINT;
            attr.DEX = define.GrowthDEX;
        }

        private void LoadEquipAttributes(AttributeData attr, List<EquipDefine> equips)
        {
            attr.Reset();
            if (equips == null) return;
            foreach (var define in equips)
            {
                attr.MaxHP += define.MaxHP;
                attr.MaxMP += define.MaxMP;
                attr.STR += define.STR;
                attr.INT += define.INT;
                attr.DEX += define.DEX;
                attr.AD += define.AD;
                attr.AP += define.AP;
                attr.DEF += define.DEF;
                attr.MDEF += define.MDEF;
                attr.SPD += define.SPD;
                attr.CRI += define.CRI;
            }
        }
    }
}
