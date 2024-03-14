using Entities;
using Managers;
using Network;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Services
{
    public class BattleService : Singleton<BattleService>, IDisposable
    {
        public void Init()
        {

        }

        public BattleService()
        {
            MessageDistributer.Instance.Subscribe<SkillCastResponse>(this.OnSkillCast);
            MessageDistributer.Instance.Subscribe<SkillHitResponse>(this.OnSkillHit);
            MessageDistributer.Instance.Subscribe<BuffResponse>(this.OnBuff);
        }

        public void Dispose()
        {
            MessageDistributer.Instance.Unsubscribe<SkillCastResponse>(this.OnSkillCast);
            MessageDistributer.Instance.Unsubscribe<SkillHitResponse>(this.OnSkillHit);
            MessageDistributer.Instance.Unsubscribe<BuffResponse>(this.OnBuff);
        }

        public void SendSkillCast(int skillId, int casterId, int targetId, NVector3 position)
        {
            if (position == null) position = new NVector3();
            Debug.LogFormat("SendSkillCast: skill:{0} caster:{1} target:{2} pos:{3}",skillId,casterId,targetId,position.ToString());
            NetMessage message = new NetMessage();
            message.Request = new NetMessageRequest();
            message.Request.skillCast = new SkillCastRequest();
            message.Request.skillCast.castInfo = new NSkillCastInfo();
            message.Request.skillCast.castInfo.skillId = skillId;
            message.Request.skillCast.castInfo.casterId = casterId;
            message.Request.skillCast.castInfo.targetId = targetId;
            message.Request.skillCast.castInfo.Position = position;
            NetClient.Instance.SendMessage(message);
        }

        private void OnSkillCast(object sender, SkillCastResponse message)
        {
            if (message.Result == Result.Success)
            {
                foreach (var cast in message.castInfoes)
                {
                    Debug.LogFormat("OnSkillCast: skill:{0} caster:{1} target :{2} pos:{3} result:{4}", cast.skillId, cast.casterId, cast.targetId, cast.Position.String(), message.Result);

                    Creature caster = EntityManager.Instance.GetEntity(cast.casterId) as Creature;
                    if (caster != null)
                    {
                        Creature target = EntityManager.Instance.GetEntity(cast.targetId) as Creature;
                        caster.CastSkill(cast.skillId, target, cast.Position);
                    }
                }
            }
            else
            {
                ChatManager.Instance.AddSystemMessage(message.Errormsg);
            }
        }

        private void OnSkillHit(object sender, SkillHitResponse message)
        {
            Debug.LogFormat("OnSkillHit: count:{0}", message.Hits.Count);
            if(message.Result == Result.Success)
            {
                foreach (var hit in message.Hits)
                {
                    Creature caster = EntityManager.Instance.GetEntity(hit.casterId) as Creature;
                    if(caster != null)
                    {
                        caster.DoSkillHit(hit);
                    }
                }
            }
        }

        private void OnBuff(object sender, BuffResponse message)
        {
            Debug.LogFormat("OnBuff: count:{0}", message.Buffs.Count);

            foreach (var buff in message.Buffs)
            {
                Debug.LogFormat("      Buff:{0}:{1}[{2}]", buff.buffId, buff.buffType, buff.Action);
                Creature owner = EntityManager.Instance.GetEntity(buff.ownerId) as Creature;
                if(owner != null)
                {
                    owner.DoBuffAction(buff);
                }
            }
        }

    }
}
