using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;

public class UILogin : MonoBehaviour {

	public InputField username;
	public InputField password;
	public Button buttonLogin;

	// Use this for initialization
	void Start () {
		UserService.Instance.OnLogin = this.OnLogin;
	}
	
	void OnLogin(SkillBridge.Message.Result result, string msg)
    {
		MessageBox.Show(string.Format("结果：" + result + " msg：" + msg));
    }
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickLogin()
    {
		UserService.Instance.SendLogin(this.username.text, this.password.text);
    }
}
