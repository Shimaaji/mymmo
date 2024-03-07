using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Entities
{
    public interface IEntityController
    {
        Transform GetTransform();
        void PlayAnim(string name);
        void PlayEffect(EffectType type, string name, Creature target, float duration);
        void PlayEffect(EffectType type, string name, NVector3 position, float duration);
        void SetStandby(bool standBy);
        void UpdateDirection();
    }
}
