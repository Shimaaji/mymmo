using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    class NPCManager : Singleton<NPCManager>
    {
        public delegate bool NpcActionHandler(NpcDefine npc);

        Dictionary<NpcFunction, NpcActionHandler> eventMap = new Dictionary<NpcFunction, NpcActionHandler>();
        Dictionary<int, Vector3> npcPositions = new Dictionary<int, Vector3>();

        public void RegisterNpcEvent(NpcFunction function, NpcActionHandler action)
        {
            if (!eventMap.ContainsKey(function))
            {
                eventMap[function] = action;
            }
            else
            {
                eventMap[function] += action;
            }
        }

        public NpcDefine GetNpcDefine(int npcID)
        {
            NpcDefine npc = null;
            DataManager.Instance.Npcs.TryGetValue(npcID, out npc);
            return npc;
        }

        public bool Interactive(int npcId)
        {
            if (DataManager.Instance.Npcs.ContainsKey(npcId))
            {
                var npc = DataManager.Instance.Npcs[npcId];
                return Interactive(npc);
            }
            return false;
        }

        public bool Interactive(NpcDefine npc)
        {
            if (DoTaskInteractive(npc))
            {
                return true;
            }
            else if (npc.Type == NpcType.Functional)
            {
                return DoFunctionInteractice(npc);
            }
            return false;
        }

        private bool DoTaskInteractive(NpcDefine npc)
        {
            var status = QuestManager.Instance.GetQuestStatusByNpc(npc.ID);
            if (status == NpcQuestStatus.None)
            {
                return false;
            }
            return QuestManager.Instance.OnpenNpcQuest(npc.ID);
        }

        private bool DoFunctionInteractice(NpcDefine npc)
        {
            if (npc.Type != NpcType.Functional)
            {
                return false;
            }
            if (!eventMap.ContainsKey(npc.Function))
            {
                return false;
            }

            return eventMap[npc.Function](npc);
        }

        internal void UpdateNpcPosition(int npc, Vector3 pos)
        {
            this.npcPositions[npc] = pos;
        }
        internal Vector3 GetNpcPosition(int npc)
        {
            return this.npcPositions[npc];
        }

    }
}
