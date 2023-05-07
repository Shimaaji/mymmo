using Common.Data;
using Managers;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestSystem : UIWindow {

	public Text title;

	public GameObject itemPrefab;

	public TabView Tabs;
	public ListView listMain;
	public ListView listBranch;

	public UIQuestInfo questInfo;

	private bool showAvailableList = false;
	// Use this for initialization
	void Start () {
		this.listMain.onItemSelected += this.OnQuestSelected;
		this.listBranch.onItemSelected += this.OnQuestSelected;
		this.Tabs.OnTabSelect += OnSelectTab;
		RefreshUI();
	}

    void OnSelectTab(int idx)
    {
		showAvailableList = idx == 1;
		RefreshUI();
    }
	private void RefreshUI()
	{
		ClearAllQuestList();
		InitAllQuestList();
	}

	private void ClearAllQuestList()
	{
		this.listMain.RemoveAll();
		this.listBranch.RemoveAll();
	}

	private void InitAllQuestList()
    {
        foreach (var kv in QuestManager.Instance.allQuests)
        {
            if (showAvailableList)
            {
				if (kv.Value.Info != null)
					continue;
            }
            else
            {
				if (kv.Value.Info == null)
					continue;
            }

			GameObject go = Instantiate(itemPrefab, kv.Value.Define.Type == QuestType.Main ? this.listMain.transform : this.listBranch.transform);
			UIQuestItem ui = go.GetComponent<UIQuestItem>();
			ui.SetQuestInfo(kv.Value);
			/*
			if (kv.Value.Define.Type == QuestType.Main)
				this.listMain.AddItem(ui);
			else
				this.ListBranch.AddItem(ui);
			*/
			this.listMain.AddItem(ui);
			this.listBranch.AddItem(ui);
        }
    }

	/*
	UIQuestItem questItem;
	private void OnQuestSelected(ListView.ListViewItem item)
	{
        if (questItem != null)
        {
			questItem.owner.ClearSelectedItem();
			//questItem.owner.SelectedItem.Selected = false;
        }
		questItem = item as UIQuestItem;
		this.questInfo.SetQuestInfo(questItem.quest);
	}
	*/

	private void OnQuestSelected(ListView.ListViewItem item)
	{
		UIQuestItem questItem = item as UIQuestItem;
		this.questInfo.SetQuestInfo(questItem.quest);
	}
}
