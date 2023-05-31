using Models;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers
{
    class TeamManager : Singleton<TeamManager>
    {
        public void Init()
        {

        }

        public void UpdateTeamInfo(NTeamInfo item)
        {
            User.Instance.TeamInfo = item;
            ShowTeamUI(item != null);
        }

        public void ShowTeamUI(bool show)
        {
            if (UIMain.Instance != null)
            {
                UIMain.Instance.ShowTeamUI(show);
            }
        }
    }
}
