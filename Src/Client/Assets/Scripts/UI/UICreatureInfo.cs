using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreatureInfo : MonoBehaviour {

	public Text Name;
	public Slider HPBar;
	public Slider MPBar;
	public Text HPText;
	public Text MPText;

	// Use this for initialization
	void Start () {
		
	}

	private Creature target;
	public Creature Target
	{
		get { return target; }
		set
		{
			this.target = value;
			this.UpdateUI();
		}
	}

    private void UpdateUI()
    {
		if (this.target == null) return;
		this.Name.text = string.Format("{0} Lv.{1}", target.Name, target.Info.Level);

		this.HPBar.maxValue = target.Attributes.MaxHP;
		this.HPBar.value = target.Attributes.HP;
		this.HPText.text = string.Format("{0}/{1}", target.Attributes.HP, target.Attributes.MaxHP);

		this.MPBar.maxValue = target.Attributes.MaxMP;
		this.MPBar.value = target.Attributes.MP;
        this.MPText.text = string.Format("{0}/{1}", target.Attributes.MP, target.Attributes.MaxMP);
    }

    // Update is called once per frame
    void Update () {
		UpdateUI();
    }
}
