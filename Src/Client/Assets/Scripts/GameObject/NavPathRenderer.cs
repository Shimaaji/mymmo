using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPathRenderer : MonoSingleton<NavPathRenderer> {

	LineRenderer pathRenderer;
	NavMeshPath path;
	// Use this for initialization
	void Start () {
		pathRenderer = GetComponent<LineRenderer>();
		pathRenderer.enabled = false;
	}
	
	public void SetPath(NavMeshPath path, Vector3 target)
	{
		this.path = path;
		if(this.path == null)
		{
			pathRenderer.enabled = false;
			pathRenderer.positionCount = 0;
		}
		else
		{
			pathRenderer.enabled = true;
			pathRenderer.positionCount = path.corners.Length + 1;
			pathRenderer.SetPositions(path.corners);
			pathRenderer.SetPosition(pathRenderer.positionCount - 1, target);
			for (int i = 0; i < pathRenderer.positionCount; i++)
			{
				//将所有的点向上增加0.2的偏移量，否则渲染出来的点会贴着地面
				pathRenderer.SetPosition(i, pathRenderer.GetPosition(i) + Vector3.up * 0.2f);
			}
		}
	}
}
