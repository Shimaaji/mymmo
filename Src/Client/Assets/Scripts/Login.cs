using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//初始化链接
		Network.NetClient.Instance.Init("127.0.0.1",8000);
		Network.NetClient.Instance.Connect();

		//发送消息
		SkillBridge.Message.NetMessage msg = new SkillBridge.Message.NetMessage();
		msg.Request = new SkillBridge.Message.NetMessageRequest();
		msg.Request.FirstTestRequest = new SkillBridge.Message.FirstTestRequest();
		msg.Request.FirstTestRequest.Helloworld = "HelloWorld";
		Network.NetClient.Instance.SendMessage(msg);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
