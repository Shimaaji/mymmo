using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuildItem : ListView.ListViewItem {

	public Text guildId;
	public Text guildName;
	public Text guildNum;
	public Text leader;

	public Image background;
	public Sprite normalBg;
	public Sprite selectedBg;

    public override void onSelected(bool selected)
    {
        this.background.overrideSprite = selected ? selectedBg : normalBg;
    }

    public NGuildInfo Info;

    public void SetGuildInfo(NGuildInfo item)
    {
        this.Info = item;
        if (this.guildId != null) this.guildId.text = this.Info.Id.ToString();
        if (this.guildName != null) this.guildName.text = this.Info.GuildName;
        if (this.guildNum != null) this.guildNum.text = this.Info.memberCount.ToString();
        if (this.leader != null) this.leader.text = this.Info.leaderName;
    }
}
