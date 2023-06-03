﻿using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UIWindow {

	public void BackToCharSelect()
    {
		SceneManager.Instance.LoadScene("CharSelect");
		UserService.Instance.SendGameLeave();
    }

	public void ExitGame()
    {
        UserService.Instance.SendGameLeave(true);
    }
}
