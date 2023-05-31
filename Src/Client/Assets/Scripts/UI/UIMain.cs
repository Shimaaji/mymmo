using Managers;
using Models;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMain : MonoSingleton<UIMain> {

	public Text avatarName;
	public Text avatarLevel;
	public GameObject[] avatarImage;

	public UITeam TeamWindow;

	
	protected override void OnStart() 
	{
		this.UpdateAvatar();
	}
	
	void UpdateAvatar()
	{
		this.avatarName.text = string.Format("{0}[{1}]", User.Instance.CurrentCharacter.Name, User.Instance.CurrentCharacter.Id);
		this.avatarLevel.text = User.Instance.CurrentCharacter.Level.ToString();
		for (int i = 0; i < 3; i++)
		{
			avatarImage[i].SetActive(i == User.Instance.CurrentCharacter.ConfigId - 1);
		}
	}


	public void BachToCharSelect()
    {
		SceneManager.Instance.LoadScene("CharSelect");
		UserService.Instance.SendGameLeave();
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

    }
	public void OnClickSetting()
	{

	}

	public void OnClickSkill()
	{

	}


	public void ShowTeamUI(bool show)
	{
		TeamWindow.ShowTeam(show);
	}
}
