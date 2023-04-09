using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("abc:" + abc());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static int abc()
    {
		int[] i = {1,2 };
        try
        {
			return i[1];
        }
        finally
        {
			i[1] = 11;
			Debug.Log("i = " + i[1]);
        }
    }
}
