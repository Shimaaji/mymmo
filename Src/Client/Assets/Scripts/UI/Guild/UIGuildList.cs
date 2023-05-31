using Services;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuildList : UIWindow {

	public GameObject itemPrefab;
	public ListView listMain;
	public Transform itemRoot;
	public UIGuildInfo uiInfo;
	public UIGuildItem selectedItem;

	// Use this for initialization
	void Start () {
		this.listMain.onItemSelected += this.OnGuildMemberSelected;
		this.uiInfo.Info = null;
		GuildService.Instance.OnGuildListResult += UpdateGuildList;
		GuildService.Instance.SendGuildListRequest();
	}

    private void OnDestroy()
    {
		GuildService.Instance.OnGuildListResult -= UpdateGuildList;
    }

	// Update is called once per frame
	void UpdateGuildList(List<NGuildInfo> guilds) 
	{
		ClearList();
		InitItems(guilds);
	}

	private void OnGuildMemberSelected(ListView.ListViewItem item)
	{
		this.selectedItem = item as UIGuildItem;
		this.uiInfo.Info = this.selectedItem.Info;
	}

	private void InitItems(List<NGuildInfo> guilds)
    {
        foreach (var item in guilds)
        {
			GameObject go = Instantiate(itemPrefab, this.listMain.transform);
			UIGuildItem ui = go.GetComponent<UIGuildItem>();
			ui.SetGuildInfo(item);
			this.listMain.AddItem(ui);
        }
    }

    private void ClearList()
    {
		this.listMain.RemoveAll();
    }

	public void OnClickJoin()
    {
        if (selectedItem == null)
        {
			MessageBox.Show("请选择要加入的公会");
			return;
        }
        else
        {
			
			MessageBox.Show(string.Format("确定要加入公会【{0}】吗？", selectedItem.Info.GuildName), "申请加入公会", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
			{
				GuildService.Instance.SendGuildJoinRequest(this.selectedItem.Info.Id);
			};
			
        }
    }
}
