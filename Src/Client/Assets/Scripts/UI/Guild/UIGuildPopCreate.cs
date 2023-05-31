using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuildPopCreate : UIWindow {

	public InputField inputName;
	public InputField inputNotice;
	
	private void Start()
    {
        GuildService.Instance.OnGuildCreateResult = OnGuildCreate;
    }

	private void OnDestroy()
    {
        GuildService.Instance.OnGuildCreateResult = null;
    }

    public override void OnYesClick()
    {
        if (string.IsNullOrEmpty(inputName.text))
        {
            MessageBox.Show("请输入公会名", "错误", MessageBoxType.Error);
            return;
        }
        if (inputName.text.Length < 4 || inputName.text.Length > 10)
        {
            MessageBox.Show("公会名称为4-10个字符", "错误", MessageBoxType.Error);
            return;
        }
        if (string.IsNullOrEmpty(inputNotice.text))
        {
            MessageBox.Show("请输入公会名", "错误", MessageBoxType.Error);
            return;
        }
        if (inputNotice.text.Length < 3 || inputNotice.text.Length > 50)
        {
            MessageBox.Show("公会名称为3-50个字符", "错误", MessageBoxType.Error);
            return;
        }

        GuildService.Instance.SendGuildCreate(inputName.text, inputNotice.text);
    }

    void OnGuildCreate(bool result)
    {
        if (result)
            this.Close(WindowResult.Yes);
    }
}
