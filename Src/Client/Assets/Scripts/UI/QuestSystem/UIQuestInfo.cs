using Common.Data;
using Models;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestInfo : MonoBehaviour {

	public Text title;

	public Text[] targets;

	public Text overview;
	public Text dialog;

	public UIIconItem[] rewardItems;

	public Text rewardMoney;
	public Text rewardExp;


	// Use this for initialization
	void Start () {
		
	}

	public void SetQuestInfo(Quest quest)
    {
		this.title.text = string.Format("[{0}]{1}", quest.Define.Type, quest.Define.Name);
		if (this.dialog != null) this.dialog.text = quest.Define.Dialog.Replace("XXX",User.Instance.CurrentCharacter.Name);

        if (this.overview != null)
        {
			if (quest.Info == null)
			{
				this.overview.text = quest.Define.Overview;
			}
			else
			{
				if (quest.Info.Status == QuestStatus.Complated)
				{
					this.overview.text = quest.Define.Overview;
				}
			}
		}

		
		//TODO:任务信息面板的任务目标部分待完善
        if (quest.Define.Target2 == QuestTarget.None)
        {
			this.targets[0].text = quest.Define.Overview;
			this.targets[1].enabled = false;
			this.targets[2].enabled = false;
        }
		
		
		//TODO:任务奖励道具图标及数量的加载
		if (quest.Define.RewardItem1 == 0)
		{
			rewardItems[0].gameObject.SetActive(false);
			rewardItems[1].gameObject.SetActive(false);
			rewardItems[2].gameObject.SetActive(false);
		}
		if (quest.Define.RewardItem1 != 0 && quest.Define.RewardItem2 == 0)
        {
			rewardItems[0].gameObject.SetActive(true);
			rewardItems[1].gameObject.SetActive(false);
			rewardItems[2].gameObject.SetActive(false);
			rewardItems[0].SetMainIcon(DataManager.Instance.Items[quest.Define.RewardItem1].Icon, quest.Define.RewardItem1Count.ToString());
		}
		if (quest.Define.RewardItem2 != 0 && quest.Define.RewardItem3 == 0)
		{
			rewardItems[0].gameObject.SetActive(true);
			rewardItems[1].gameObject.SetActive(true);
			rewardItems[2].gameObject.SetActive(false);
			rewardItems[0].SetMainIcon(DataManager.Instance.Items[quest.Define.RewardItem1].Icon, quest.Define.RewardItem1Count.ToString());
			rewardItems[1].SetMainIcon(DataManager.Instance.Items[quest.Define.RewardItem2].Icon, quest.Define.RewardItem2Count.ToString());
		}
		if (quest.Define.RewardItem3 != 0)
        {
			rewardItems[0].SetMainIcon(DataManager.Instance.Items[quest.Define.RewardItem1].Icon, quest.Define.RewardItem1Count.ToString());
			rewardItems[1].SetMainIcon(DataManager.Instance.Items[quest.Define.RewardItem2].Icon, quest.Define.RewardItem2Count.ToString());
			rewardItems[2].SetMainIcon(DataManager.Instance.Items[quest.Define.RewardItem3].Icon, quest.Define.RewardItem3Count.ToString());
		}
		
		this.rewardMoney.text = quest.Define.RewardGold.ToString();
		this.rewardExp.text = quest.Define.RewardExp.ToString();

        foreach (var fitter in this.GetComponentsInChildren<ContentSizeFitter>())
        {
			fitter.SetLayoutVertical();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickAbandon()
    {

    }
}
