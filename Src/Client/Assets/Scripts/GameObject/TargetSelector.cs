using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoSingleton<TargetSelector> {

	Projector projector;
	bool actived = false;

	Vector3 center;	//光圈中心点
	private float range;//技能释放范围
	private float size;//技能光圈大小
	Vector3 offset = new Vector3 (0f, 2f, 0f);//偏移量（防止被地面挡住，2f保证光圈始终在地面上方）

	protected Action<Vector3> selectPoint;

    protected override void OnStart()
    {
		projector = this.GetComponentInChildren<Projector>();
		projector.gameObject.SetActive(actived);
    }

	public void Active(bool active)
	{
		this.actived = active;
		if (projector == null) return;

		projector.gameObject.SetActive(this.actived);
		projector.orthographicSize = this.size * 0.5f;//取半径
	}

	void Update()
	{
		if (!actived) return;
		if(this.projector == null) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//需要将摄像机设为MainCamera
		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Terrain")))//只对地面，即"Terrain"做射线检测
        {
			//获取到射线的点
			Vector3 hitPoint = hitInfo.point;
            //计算了鼠标点击位置与角色中心点之间的向量距离。
            Vector3 dist = hitPoint - this.center;

			//判断鼠标移动会不会超出技能释放范围，超出做一些修正
			if(dist.magnitude > this.range)
			{
				hitPoint = this.center + dist.normalized * this.range;
			}

			this.projector.gameObject.transform.position = hitPoint + offset;
			if(Input.GetMouseButtonDown(0))
			{
				this.selectPoint(hitPoint);
				this.Active(false);
			}
		}
		if(Input.GetMouseButtonDown(1))
		{
			this.Active(false);
		}
	}

	public static void ShowSelector(Vector3Int center, int range, int size, Action<Vector3> onPositionSelected)
	{
		if (TargetSelector.Instance == null) return;
		TargetSelector.Instance.center = GameObjectTool.LogicToWorld(center);
        TargetSelector.Instance.range = GameObjectTool.LogicToWorld(range);
        TargetSelector.Instance.size = GameObjectTool.LogicToWorld(size);
		TargetSelector.Instance.selectPoint = onPositionSelected;
		TargetSelector.Instance.Active(true);
    }
}
