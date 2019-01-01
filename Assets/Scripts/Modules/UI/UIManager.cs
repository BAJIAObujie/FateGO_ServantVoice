using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI命名规则
//一个界面由Dialog开头，底下元素根据功能划分组，每一组以Group开头。
//元素以主要功能命名例如List_Voices 是容纳voice的List列表 以List开头，并以下划线分开
//UI开头为基础UI组件。组件必须是可复用才可以以UI开头。例如UIItem_Voice UIList_Voices

public class UIManager : SingleTonManager<UIManager>, IManager
{
	private static GameObject UIRoot;
	
	public void Init()
	{
		UIRoot = GameObject.Find("Canvas");
	}
	/* ------- 唯一数据 ------- */

	public GameObject getUIRoot()
	{
		return UIRoot;
	}
	
	public GameObject ShowUI(string uiName)
	{
		Transform findObj = UIRoot.transform.Find(uiName);
		if (findObj != null)
		{
			return findObj.gameObject;
		}
		GameObject prefab = ResourceManager.getInstace().getUI(uiName);
		return Instantiate(prefab,UIRoot.transform);
	}

	
}
