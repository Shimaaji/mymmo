using Candlelight.UI;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChat : MonoBehaviour {

	public HyperText textArea;//聊天内容显示区域
	public TabView channelTab;
	public InputField chatText;//聊天输入控件
	public Text chatTarget;
	public Dropdown channelSelect;

	// Use this for initialization
	void Start () {
		this.channelTab.OnTabSelect += OnDisplayChannelSelected;
		ChatManager.Instance.OnChat += RefreshUI;
	}

	private void OnDestroy()
    {
		ChatManager.Instance.OnChat -= RefreshUI;
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.Instance.IsInputMode = chatText.isFocused;
	}

	public void OnDisplayChannelSelected(int idx)
    {
		ChatManager.Instance.displayChannel = (ChatManager.LocalChannel)idx;
		RefreshUI();
    }

	void RefreshUI()
    {
		this.textArea.text = ChatManager.Instance.GetCurrentMessages();
		this.channelSelect.value = (int)ChatManager.Instance.sendChannel - 1;
        if (ChatManager.Instance.SendChannel == ChatChannel.Private)
        {
			this.chatTarget.gameObject.SetActive(true);
            if (ChatManager.Instance.PrivateID != 0)
            {
				this.chatTarget.text = ChatManager.Instance.PrivateName + ":";
            }
            else
            {
				this.chatTarget.text = "<无>";
            }
        }
        else
        {
			this.chatTarget.gameObject.SetActive(false);
        }
    }

	public void OnClickChatLink(HyperText text, HyperText.LinkInfo link)
    {
		if (string.IsNullOrEmpty(link.Name))
			return;
		//HyperText的数据传输格式如下
		//<a name="c:1001:Name" class="player">Name</a>   角色信息
		//<a name="i:1001:Name" class="item">Name</a>   道具信息
		//拆分传输过来的数据，获取对应的信息
		if (link.Name.StartsWith("c:"))
        {
			string[] strs = link.Name.Split(":".ToCharArray());
			UIPopCharMenu menu = UIManager.Instance.Show<UIPopCharMenu>();
			menu.targetId = int.Parse(strs[1]);
			menu.targetName = strs[2];
        }
    }

	public void OnClickSend()
    {
		OnEndInput(this.chatText.text);
    }

	public void OnEndInput(string text)
    {
		if (!string.IsNullOrEmpty(text.Trim()))
			ChatManager.Instance.SendChat(text, ChatManager.Instance.PrivateID, ChatManager.Instance.PrivateName);
		this.chatText.text = "";
    }

	public void OnSendChannelChanged(int idx)
    {
		if (ChatManager.Instance.sendChannel == (ChatManager.LocalChannel)(idx + 1))
			return;
		if (!ChatManager.Instance.SetSendChannel((ChatManager.LocalChannel)idx + 1))
			this.channelSelect.value = (int)ChatManager.Instance.sendChannel - 1;
		else
			this.RefreshUI();
    }
}
