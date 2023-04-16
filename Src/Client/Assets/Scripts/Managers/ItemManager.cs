using Common.Data;
using Models;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class ItemManager : Singleton<ItemManager>
	{
		public Dictionary<int, Item> Items = new Dictionary<int, Item>();
		internal void Init(List<NItemInfo> items)
        {
			this.Items.Clear();
            foreach (var info in items)
            {
				Item item = new Item(info);
				this.Items.Add(item.Id, item);

				Debug.LogFormat("ItemManager: Init[{0}]", item);
            }
        }


		public ItemDefine GetItem(int itenId)
        {
			return null;
        }

		public bool UseItem(int itemId)
        {
			return false;
		}

		public bool UseItem(ItemDefine itemId)
		{
			return false;
		}

	}
}
