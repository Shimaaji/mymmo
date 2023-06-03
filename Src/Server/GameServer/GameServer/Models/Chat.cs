using GameServer.Entities;
using GameServer.Managers;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    class Chat
    {
        Character Owner;

        public int localIdx;
        public int worldIdx;
        public int systemIdx;
        public int teamIdx;
        public int guildIdx;

        public Chat(Character owner)
        {
            this.Owner = owner;
        }

        public void PostProcess(NetMessageResponse message)
        {
            if (message.Chat == null)
            {
                message.Chat = new ChatResponse();
                message.Chat.Result = Result.Success;
            }
            this.localIdx = ChatManager.Instance.GetLocalMessage(this.Owner.Info.mapId, this.localIdx, message.Chat.localMessages);
            this.worldIdx = ChatManager.Instance.GetWolrdMessage(this.worldIdx, message.Chat.worldMessages);
            this.systemIdx = ChatManager.Instance.GetSystemMessage(this.systemIdx, message.Chat.systemMessages);
            if (this.Owner.Team != null)
            {
                this.teamIdx = ChatManager.Instance.GetTeamMessage(this.Owner.Team.Id, this.teamIdx, message.Chat.teamMessages);
            }
            if (this.Owner.Guild != null)
            {
                this.guildIdx = ChatManager.Instance.GetGuildMessage(this.Owner.Guild.Id, this.guildIdx, message.Chat.guildMessages);
            }
        }
    }
}
