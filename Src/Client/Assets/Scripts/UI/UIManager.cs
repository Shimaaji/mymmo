using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }

    private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();

    public UIManager()
    {
        this.UIResources.Add(typeof(UITest), new UIElement() { Resources = "UI/UITest", Cache = true });
        this.UIResources.Add(typeof(UIBag), new UIElement() { Resources = "UI/UIBag", Cache = false });
        this.UIResources.Add(typeof(UIShop), new UIElement() { Resources = "UI/UIShop",Cache = false });
        this.UIResources.Add(typeof(UICharEquip), new UIElement() { Resources = "UI/UICharEquip", Cache = false });
        this.UIResources.Add(typeof(UIQuestSystem), new UIElement() { Resources = "UI/UIQuestSystem", Cache = false });
        this.UIResources.Add(typeof(UIQuestDialog), new UIElement() { Resources = "UI/UIQuestDialog", Cache = false });
        this.UIResources.Add(typeof(UIFriend), new UIElement() { Resources = "UI/UIFriend", Cache = false });
        this.UIResources.Add(typeof(UIGuild), new UIElement() { Resources = "UI/Guild/UIGuild", Cache = false });
        this.UIResources.Add(typeof(UIGuildList), new UIElement() { Resources = "UI/Guild/UIGuildList", Cache = false });
        this.UIResources.Add(typeof(UIGuildPopNoGuild), new UIElement() { Resources = "UI/Guild/UIGuildPopNoGuild", Cache = false });
        this.UIResources.Add(typeof(UIGuildPopCreate), new UIElement() { Resources = "UI/Guild/UIGuildPopCreate", Cache = false });
        this.UIResources.Add(typeof(UIGuildApplyList), new UIElement() { Resources = "UI/Guild/UIGuildApplyLost", Cache = false });
    }

    ~UIManager()
    {

    }

    public T Show<T>()
    {
        Type type = typeof(T);
        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            //判断当前是否有此UI实例，如果有，直接激活，没有则从资源中创建
            if (info.Instance != null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if (prefab == null)
                {
                    return default(T);
                }

                info.Instance = (GameObject)GameObject.Instantiate(prefab);
            }

            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }

    public void Close(Type type)
    {
        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if (info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }
}
