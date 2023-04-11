using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControlller : MonoBehaviour {

	public Collider minimapBoundingbox;

	// Use this for initialization
	void Start () {
		MinimapManager.Instance.UpdateMiniMap(minimapBoundingbox);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
