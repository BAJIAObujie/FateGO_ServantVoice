using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DlgSelect : MonoBehaviour {

	//一般最好不要在start里写吧 因为这个函数是内部调用的 
	
	//!!不要在这里写 会有加载顺序的问题
	void Start () {

	}

	public void OnShow()
	{
		//data
		ServantData data = ServantManager.getInstace().getData();
		
		UIList list = transform.Find("Group_ServantSelect/ScrollView/Viewport/Content").GetComponent<UIList>();
		list.DrawList(data.nameToServant.Count, (index,listItem) =>
		{
			Servant servant = data.nameToServant[index];
			listItem.transform.Find("UIButton").GetComponent<Button>().onClick.AddListener(() =>
			{
				GameObject uiDialogShop = UIManager.getInstace().ShowDialog("DlgShop");
				uiDialogShop.GetComponent<DlgShop>().OnShow(servant.servantEName);
			});
			listItem.transform.Find("UIText").GetComponent<Text>().text = servant.servantEName;
		});
	}
	
}
