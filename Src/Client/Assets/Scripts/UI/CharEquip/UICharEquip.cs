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
		this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
	}

    private void InitAllEquipItems()
    {
        foreach (var kv in ItemManager.Instance.Items)
        {
            if (kv.Value.Define.Type == ItemType.Equip && kv.Value.Define.LimitClass == User.Instance.CurrentCharacter.Class)
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
        EquipManager.Instance.EquiItem(item);
    }

    public void UnEquip(Item item)
    {
        EquipManager.Instance.UnEquipItem(item);
    }
}
