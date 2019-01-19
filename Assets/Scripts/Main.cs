using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	
	void Start () {
		Global.Instace.generateManager();
		setUILeft();
		//setUIRight();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnGUI()
//	{
//		if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
//		{
//			setUILeft();
//		}
//	}
	
	private void setUILeft()
	{
		
	}
}
