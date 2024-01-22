using Battle;
using Common.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillItem : ListView.ListViewItem {

	public Image icon;
	public Text title;
	public Text level;

	public Image background;
	public Sprite normalBg;
	public Sprite selectedBg;

    public override void onSelected(bool selected)
    {
		this.background.overrideSprite = selected ? selectedBg : normalBg;
    }

	public Skill item;

	public void SetItem(Skill item, UISkill owner, bool equiped)
	{
		this.item = item;
		if(this.title != null) this.title.text = this.item.Define.Name;
		if(this.level != null) this.level.text = item.Info.Level.ToString();
		if (this.icon != null) this.icon.overrideSprite = Resources.Load<Sprite>(this.item.Define.Icon);

	}

}
