using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DlgSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Find("Left").GetComponent<Button>().onClick.AddListener(() =>
		{	
			GameObject uiDialogShop = UIManager.getInstace().ShowDialog("DlgShop");
			uiDialogShop.GetComponent<DlgShop>().OnShow("Ereshkigal");
		});
		transform.Find("Right").GetComponent<Button>().onClick.AddListener(() =>
		{
			GameObject uiDialogShop = UIManager.getInstace().ShowDialog("DlgShop");
			uiDialogShop.GetComponent<DlgShop>().OnShow("Osakabehime");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	
	
}
