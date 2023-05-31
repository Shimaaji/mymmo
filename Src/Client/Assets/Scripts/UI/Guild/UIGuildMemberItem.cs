﻿using Common.Utils;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuildMemberItem : ListView.ListViewItem {

	public Text nickName;
	public Text @class;
	public Text level;
	public Text title;
	public Text joinTime;
	public Text status;

	public Image background;
	public Sprite normalBg;
	public Sprite selectedBg;

    public override void onSelected(bool selected)
    {
		this.background.overrideSprite = selected ? selectedBg : normalBg;
    }

	public NGuildMemberInfo Info;
	public void SetGuildMemberInfo(NGuildMemberInfo item)
    {
		this.Info = item;
		if (this.nickName != null) this.nickName.text = this.Info.Info.Name;
		if (this.@class != null) this.@class.text = this.Info.Info.Class.ToString();
		if (this.level != null) this.level.text = this.Info.Info.Level.ToString();
		if (this.title != null) this.title.text = this.Info.Title.ToString();
		if (this.joinTime != null) this.joinTime.text = TimeUtil.GetTime(this.Info.joinTime).ToShortDateString();
		if (this.status != null) this.status.text = this.Info.Status == 1 ? "在线" : TimeUtil.GetTime(this.Info.lastTime).ToShortDateString();
	}
    
}
