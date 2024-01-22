using Managers;
using Models;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFriend : UIWindow {

	public GameObject itemPrefab;
	public ListView listMain;
	public Transform itemRoot;
	public UIFriendItem selectedItem;
	// Use this for initialization
	void Start () {
		FriendService.Instance.OnFriendUpdate = RefreshUI;
		this.listMain.onItemSelected += this.OnFriendSelected;
		RefreshUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnFriendSelected(ListView.ListViewItem item)
    {
		this.selectedItem = item as UIFriendItem;
    }

	public void OnClickAdd()
    {
		InputBox.Show("输入要添加的好友名称或ID", "添加好友").OnSubmit += OnFriendAddSubmit;
    }

    private bool OnFriendAddSubmit(string input, out string tips)
    {
		tips = "";
		int friendId = 0;
		string friendName = "";
		if (!int.TryParse(input, out friendId))
			friendName = input;
        if (friendId == User.Instance.CurrentCharacterInfo.Id || friendName == User.Instance.CurrentCharacterInfo.Name)
        {
			tips = "不能添加自己为好友";
			return false;
        }

		FriendService.Instance.SendFriendAddRequest(friendId, friendName);
		return true;
    }

	public void OnClickFriendChat()
    {
		//MessageBox.Show("暂未开放"); 
		if (this.selectedItem == null)
        {
			MessageBox.Show("请选择要私聊的好友", "私聊", MessageBoxType.Error);
			return;
        }
        else
        {
			ChatManager.Instance.StartPrivateChate(this.selectedItem.Info.friendInfo.Id,this.selectedItem.Info.friendInfo.Name);
			this.Close(WindowResult.No);
		}
    }

	public void OnClickFriendTeamInvite()
    {
        if (selectedItem == null)
        {
			MessageBox.Show("请选择要邀请的好友");
			return;
        }
        if (selectedItem.Info.Status == 0)
        {
			MessageBox.Show("请选择在线的好友");
			return;
        }
		MessageBox.Show(string.Format("确定邀请好友【{0}】加入队伍吗？",selectedItem.Info.friendInfo.Name),"邀请好友组队",MessageBoxType.Confirm,"邀请","取消").OnYes = () =>
        {
			TeamService.Instance.SendTeamInviteRequest(this.selectedItem.Info.friendInfo.Id, this.selectedItem.Info.friendInfo.Name);
        };
    }

	public void OnClickFriendRemove()
    {
        if (selectedItem == null)
        {
			MessageBox.Show("请选择要删除的好友");
			return;
        }
		MessageBox.Show(string.Format("确定要删除好友【{0}】吗？", selectedItem.Info.friendInfo.Name), "删除好友", MessageBoxType.Confirm, "删除", "取消").OnYes = () =>
		{
			FriendService.Instance.SendFriendRemoveRequest(this.selectedItem.Info.Id, this.selectedItem.Info.friendInfo.Id);
		};
		

	}

	void RefreshUI()
    {
		ClearFriendList();
		InitFriendItems();
    }

    private void InitFriendItems()
    {
        foreach (var item in FriendManager.Instance.allFriends)
        {
			GameObject go = Instantiate(itemPrefab, this.listMain.transform);
			UIFriendItem ui = go.GetComponent<UIFriendItem>();
			ui.SetFriendInfo(item);
			this.listMain.AddItem(ui);
        }
    }
	private void ClearFriendList()
	{
		this.listMain.RemoveAll();
	}

}
