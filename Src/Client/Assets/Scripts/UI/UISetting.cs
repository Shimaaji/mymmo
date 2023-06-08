using Managers;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UIWindow {

    public void SystemConfig()
    {
        UIManager.Instance.Show<UISystemConfig>();
        this.Close();
    }

	public void BackToCharSelect()
    {
		SceneManager.Instance.LoadScene("CharSelect");
        SoundManager.Instance.PlayMusic(SoundDefine.Music_Select);
		UserService.Instance.SendGameLeave();
    }

	public void ExitGame()
    {
        UserService.Instance.SendGameLeave(true);
    }
}
