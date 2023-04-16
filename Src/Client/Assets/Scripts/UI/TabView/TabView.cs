using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabView : MonoBehaviour {

	public TabButton[] tabButtons;
	public GameObject[] tabPage;

	public int index = -1;
	// Use this for initialization
	IEnumerator Start () {
        for (int i = 0; i < tabButtons.Length; i++)
        {
			tabButtons[i].tabView = this;
			tabButtons[i].tabIndex = i;
        }
		yield return new WaitForEndOfFrame();
		SelectTab(0);
	}
	
	public void SelectTab(int index)
    {
        if (this.index != index)
        {
            for (int i = 0; i < tabButtons.Length; i++)
            {
				tabButtons[i].Select(i == index);
				tabPage[i].SetActive(i == index);
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
