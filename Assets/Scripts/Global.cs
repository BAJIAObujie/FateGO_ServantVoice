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
		//uimanager always is the last one
		managers = new List<IManager>();
		managers.Add(AudioManager.getInstace());
		managers.Add(ResourceManager.getInstace());
		managers.Add(ConfigManager.getInstace());
		
		managers.Add(ServantManager.getInstace());
		
		managers.Add(UIManager.getInstace());
		foreach (var manager in managers)
		{
			manager.Init();
		}
	}
}
