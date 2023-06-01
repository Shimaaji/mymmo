using Managers;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuildApplyList : UIWindow {

	public GameObject itemPrefeb;
	public ListView listMain;
	public Transform itemRoot;

	// Use this for initialization
	void Start () {
		GuildService.Instance.OnGuildUpdate += UpdateList;
		GuildService.Instance.SendGuildListRequest();
		this.UpdateList();
	}
	
	private void OnDestroy()
    {
		GuildService.Instance.OnGuildUpdate -= UpdateList;
    }

	// Update is called once per frame
	void UpdateList () {
		ClearList();
		InitItems();
	}

    private void InitItems()
    {
        foreach (var item in GuildManager.Instance.guildInfo.Applies)
        {
			GameObject go = Instantiate(itemPrefeb, listMain.transform);
			UIGuildApplyItem ui = go.GetComponent<UIGuildApplyItem>();
			ui.SetItemInfo(item);
			this.listMain.AddItem(ui);
        }
    }

    private void ClearList()
    {
		this.listMain.RemoveAll();
    }
}
