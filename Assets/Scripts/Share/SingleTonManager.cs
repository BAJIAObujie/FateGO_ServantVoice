using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class SingleTonManager<T> : MonoBehaviour where T : SingleTonManager<T> {

	private static T Instace;	
	private static readonly Object lockObject = new Object();
	public static T getInstace()
	{
		if (Instace == null)
		{
			lock (lockObject)
			{
				if (Instace == null)
				{
					GameObject manager = new GameObject(typeof(T).Name);
					Instace = manager.AddComponent<T>();
					Transform parent = GameObject.Find("DontDestroyOnLoad").transform.Find("Managers");
					manager.transform.SetParent(parent); //父物体DontDestroyOnLoad 子物体默认DontDestroyOnLoad
				}
			}
		}
		return Instace;
	}
}
