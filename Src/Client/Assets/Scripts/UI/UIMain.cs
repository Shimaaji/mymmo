using Entities;
using Managers;
using Models;
using System;
using UnityEngine;
using UnityEngine.UI;
public class UIMain : MonoSingleton<UIMain> {

	public Text avatarName;
	public Text avatarLevel;
	public GameObject[] avatarImage;

	public UITeam TeamWindow;

	public UICreatureInfo targetUI;

	protected override void OnStart() 
	{
		this.UpdateAvatar();
		this.targetUI.gameObject.SetActive(false);
		BattleManager.Instance.OnTargetChanged += OnTargetChanged;
	}


    void UpdateAvatar()
	{
		this.avatarName.text = string.Format("{0}[{1}]", User.Instance.CurrentCharacterInfo.Name, User.Instance.CurrentCharacterInfo.Id);
		this.avatarLevel.text = User.Instance.CurrentCharacterInfo.Level.ToString();
		for (int i = 0; i < 3; i++)
		{
			avatarImage[i].SetActive(i == User.Instance.CurrentCharacterInfo.ConfigId - 1);
		}
	}

	public void OnClickTest()
    {
		UITest test = UIManager.Instance.Show<UITest>();
		test.SetTitle("标题栏测试UI");
		test.OnClose += Test_OnClose;
    }

    private void Test_OnClose(UIWindow sender, UIWindow.WindowResult result)
    {
		MessageBox.Show("点击了对话框的："+ result ,"对话框响应结果：",MessageBoxType.Information);
    }

	public void OnClickBag()
    {
		UIManager.Instance.Show<UIBag>();
    }

	public void OnClickCharEquip()
    {
		UIManager.Instance.Show<UICharEquip>();
    }

	public void OnClickQuest()
    {
		UIManager.Instance.Show<UIQuestSystem>();
	}

	public void OnClickFriend()
    {
		UIManager.Instance.Show<UIFriend>();
    }

	public void OnClickGuild()
    {
		GuildManager.Instance.ShowGuild();
    }

	public void OnClickRide()
    {
		UIManager.Instance.Show<UIRide>();
    }
	public void OnClickSetting()
	{
		UIManager.Instance.Show<UISetting>();
	}

	public void OnClickSkill()
	{
        UIManager.Instance.Show<UISkill>();
    }

    public void ShowTeamUI(bool show)
	{
		TeamWindow.ShowTeam(show);
	}

    private void OnTargetChanged(Creature target)
    {
        if(target != null)
		{
			if (!targetUI.isActiveAndEnabled) targetUI.gameObject.SetActive(true);
			targetUI.Target = target;
		}
		else
		{
			targetUI.gameObject.SetActive(false);
		}
    }

}
