using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIList : MonoBehaviour
{
	public GameObject prototypePrefab;
	private List<GameObject> uiList = new List<GameObject>();

	public GameObject GetItem(int index)
	{
		if (index < 0 || index >= uiList.Count)
		{
			return null;
		}
		return uiList[index];
	}

	public void ClearList()
	{
		foreach (var item in uiList)
		{
			Destroy(item);
		}
	}
	
	public void DrawList(int count, Action<int, GameObject> callback)
	{
		prototypePrefab.SetActive(false);
		ClearList();
		for (int index = 0; index < count; index++)
		{
			//transform
			GameObject listItem = Instantiate(prototypePrefab,transform); //以后看看能不能改用泛型
			listItem.SetActive(true);
			uiList.Add(listItem);
			callback.Invoke(index,listItem);
		}
	}

}
