using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINameBar : MonoBehaviour {


	public Text avatarName;
	public Character character;
	// Use this for initialization
	void Start () 
	{
		if(this.character != null)
        {
			
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.UpdateInfo();
		this.transform.forward = Camera.main.transform.forward;
	}

	void UpdateInfo()
    {
        if (this.character != null)
        {
			string name = "Lv." + this.character.Info.Level + this.character.Name;
			if (name != this.avatarName.text)
            {
				this.avatarName.text = name;
            }
        }
    }
}
