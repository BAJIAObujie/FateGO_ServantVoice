//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
////继承自此单例Mono不可以用new继续生成新的实例！
//public abstract class SingleTonMonobehavior<T> : MonoBehaviour where T : SingleTonMonobehavior<T>
//{
//
//	private static T m_Instance;
//	public static T getInstace()
//	{
//		return generateInstance(null);
//	}
//	private static readonly Object lockObject = new Object();
//
//	public static T generateInstance(GameObject parent)
//	{
//		if (m_Instance == null)
//		{
//			lock (lockObject)
//			{
//				if (m_Instance == null)
//				{
//					if (parent != null)
//					{
//						m_Instance = parent.AddComponent<T>();
//					}
//					else
//					{
//						GameObject obj = new GameObject(typeof(T).Name);
//						DontDestroyOnLoad(obj);
//						m_Instance = obj.AddComponent<T>();
//					}
//				}
//			}
//		}
//		return m_Instance;
//	}
//}
