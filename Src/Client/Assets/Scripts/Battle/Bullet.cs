using Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class Bullet
    {
        Skill skill;
        int hit = 0;
        float flyTime = 0;
        public float duration = 0;

        public bool Stopped = false;

        public Bullet(Skill skill)
        {
            this.skill = skill;
            var target = skill.Target;
            this.hit = skill.Hit;
            int distance = skill.Owner.Distance(target);
            duration = distance / this.skill.Define.BulletSpeed;
        }

        public void Update()
        {
            if(this.Stopped) return;
            this.flyTime += Time.deltaTime;
            if(this.flyTime > duration)
            {
                this.skill.DoHitDamages(this.hit);
                this.Stop();
            }
        }
        public void Stop()
        {
            this.Stopped = true;
        }
    }
}
