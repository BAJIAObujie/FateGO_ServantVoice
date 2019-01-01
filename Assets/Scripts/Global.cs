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
		
		managers.Add(ServantVoiceManager.getInstace());
		
		foreach (var manager in managers)
		{
			manager.Init();
		}

		if (AudioManager.getInstace().audioPlayer == null)
		{
			Debug.Log("audioPlayer == null");
		}
		
//		发现了一个问题 反射只能获取到类型的信息，也可以调用方法生成一个实例，但是没办法生成单例
//      用继承自单例泛型 就已经实现了单例模式 
//      java中可以利用枚举保证生成一个单例，但是c#呢，调用CreateInstance 但是并没有办法赋值。
//		managers = new List<IManager>();
//		foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
//		{
//			bool bImplementIManager = typeof(IManager).IsAssignableFrom(type);
////			bool bExtendsSingleTon = typeof(SingleTon<>).IsAssignableFrom(type);
//			if (bImplementIManager && !type.IsInterface)
//			{
//				IManager m = (IManager)Activator.CreateInstance(type);
//				m.Init();
//				managers.Add(m);
//			}
//		}

		//通过反射初始化各个模块管理器单例的原因是，
		//1. 如果用static修饰各个管理器 总感觉在多线程情况下不是很安全。实际上应该是单线程使用，很难会发生问题 总感觉怪怪的
		//2. 用static修饰类 则类是没办法实现接口的！
		//reflection
	}
}
