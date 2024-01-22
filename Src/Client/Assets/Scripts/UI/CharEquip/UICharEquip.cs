using Common.Battle;
using Managers;
using Models;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharEquip : UIWindow {

	public Text title;
	public Text money;

	public GameObject itemPrefab;
	public GameObject itemEquipPrefab;
    
    public Transform itemListRoot;

	public List<Transform> slots;

    public Text hp;
    public Slider hpBar;

    public Text mp;
    public Slider mpBar;

    public Text[] attrs;

	// Use this for initialization
	void Start () {
		RefreshUI();
		EquipManager.Instance.OnEquipChanged += RefreshUI;
	}

	private void OnDestroy()
    {
		EquipManager.Instance.OnEquipChanged -= RefreshUI;
    }

	private void RefreshUI()
	{
		ClearAllEquipList();
		InitAllEquipItems();
		ClearEquipedList();
		InitEquipedItems();
		this.money.text = User.Instance.CurrentCharacterInfo.Gold.ToString();

        InitAttributes();
	}

    private void InitAllEquipItems()
    {
        foreach (var kv in ItemManager.Instance.Items)
        {
            if (kv.Value.Define.Type == ItemType.Equip && kv.Value.Define.LimitClass == User.Instance.CurrentCharacterInfo.Class)
            {
                //已装备的不显示
                if (EquipManager.Instance.Contains(kv.Key))
                    continue;
                GameObject go = Instantiate(itemPrefab, itemListRoot);
                UIEquipItem ui = go.GetComponent<UIEquipItem>();
                ui.SetEquipItem(kv.Key, kv.Value, this, false);
            }
        }
    }

    private void ClearAllEquipList()
    {
        foreach (var item in itemListRoot.GetComponentsInChildren<UIEquipItem>())
        {
            Destroy(item.gameObject);
        }
    }

    private void ClearEquipedList()
    {
        foreach (var item in slots)
        {
            if (item.childCount > 0)
                Destroy(item.GetChild(0).gameObject);
        }
    }

    private void InitEquipedItems()
    {
        for (int i = 0; i < (int)EquipSlot.SlotMax; i++)
        {
            var item = EquipManager.Instance.Equips[i];
            {
                if (item != null)
                {
                    GameObject go = Instantiate(itemEquipPrefab, slots[i]);
                    UIEquipItem ui = go.GetComponent<UIEquipItem>();
                    ui.SetEquipItem(i, item, this, true);
                }
            }
        }
    }

    public void DoEquip(Item item)
    {
        EquipManager.Instance.EquipItem(item);
    }

    public void UnEquip(Item item)
    {
        EquipManager.Instance.UnEquipItem(item);
    }

    private void InitAttributes()
    {
        var charattr = User.Instance.CurrentCharacter.Attributes;
        this.hp.text = string.Format("{0}/{1}", charattr.HP, charattr.MaxHP);
        this.mp.text = string.Format("{0}/{1}", charattr.MP, charattr.MaxMP);
        this.hpBar.maxValue = charattr.MaxHP;
        this.hpBar.value = charattr.HP;
        this.mpBar.maxValue = charattr.MaxMP;
        this.mpBar.value = charattr.MP;

        for(int i = (int)AttributeType.STR; i < (int)AttributeType.MAX; i++)
        {
            //判断暴击，百分比显示
            if (i == (int)AttributeType.CRI)
                this.attrs[i - 2].text = string.Format("{0:f2}%", charattr.Final.Data[i] * 100);
            else
                this.attrs[i - 2].text = ((int)charattr.Final.Data[i]).ToString();
        }
        
    }
}
