using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

	public void DrawItem(string content, UnityAction action)
	{
		Transform uiText = transform.Find("UIText");
		if (uiText != null)
		{
			uiText.GetComponent<Text>().text = content;
		}
		
		Transform uiButton = transform.Find("UIButton");
		if (uiButton != null)
		{
			uiButton.GetComponent<Button>().onClick.AddListener(action);
		}
	}
}
