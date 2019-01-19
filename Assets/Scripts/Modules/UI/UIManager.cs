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
	private static Stack<GameObject> UIStack;
	private static GameObject currentDialog;
	public void Init()
	{
		UIRoot = GameObject.Find("Canvas");
		UIStack = new Stack<GameObject>();
		ShowDialog("DlgSelect");
		//UIStack.Push();
	}
	/* ------- 唯一数据 ------- */

	public GameObject getUIRoot()
	{
		return UIRoot;
	}
	
	public GameObject ShowDialog(string uiName)
	{
		if (currentDialog != null)
		{
			currentDialog.SetActive(false);
			UIStack.Push(currentDialog);
		}
		Transform findObj = UIRoot.transform.Find(uiName);
		if (findObj != null)
		{
			findObj.gameObject.SetActive(true);
			currentDialog = findObj.gameObject;
			return currentDialog;
		}
		GameObject prefab = ResourceManager.getInstace().getUI(uiName);
		currentDialog = Instantiate(prefab, UIRoot.transform);
		return currentDialog;
	}

	public void HideCurrentDialog()
	{
		if (currentDialog != null && currentDialog.activeSelf)
		{
			currentDialog.SetActive(false);
		}

		if (UIStack.Count != 0)
		{
			currentDialog = UIStack.Pop();
			currentDialog.SetActive(true);
		}
	}
	
}
