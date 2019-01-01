//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Object = UnityEngine.Object;
//
//public abstract class SingleTon<T>
//{
//	private static T Instace;	
//	private static readonly Object lockObject = new Object();
//	public static T getInstace()
//	{
//		if (Instace == null)
//		{
//			lock (lockObject)
//			{
//				if (Instace == null)
//				{
//					Instace = Activator.CreateInstance<T>();
//				}
//			}
//		}
//		return Instace;
//	}
//	protected SingleTon()
//	{
//		
//	}
//
//}
