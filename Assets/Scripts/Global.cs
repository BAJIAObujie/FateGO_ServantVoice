using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Global : MonoBehaviour
{
	public static Global Instace;
	public static List<IManager> managers;
	
	public void generateManager()
	{
		print("generateManager");
	}
	
	static Global()
	{
		GameObject go = GameObject.Find("DontDestroyOnLoad");
		DontDestroyOnLoad(go);
		Instace = go.AddComponent<Global>();

		//Init Managers
		managers = new List<IManager>();
		managers.Add(AudioManager.getInstace());
		managers.Add(ResourceManager.getInstace());
		managers.Add(ConfigManager.getInstace());
		managers.Add(UIManager.getInstace());
		
		managers.Add(ServantManager.getInstace());
		foreach (var manager in managers)
		{
			manager.Init();
		}
	}
}
