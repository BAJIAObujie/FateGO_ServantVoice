using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DlgShop : MonoBehaviour {

//	// Use this for initialization
//	void Start ()
//	{
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	
	
    //data
	private ServantVoiceData data;
	private float length;
	private float leftlength;
	private Sprite initImage;
	//ui
	private Transform Group_Conversation;
	private Transform Group_ServantVoice;

	private Transform servantWord;
	private Text Text_Speaker;
	private Text Text_Content;
	private Image Image_ServantImage;
	
	//action
	private Action<ServantVoice> PlayVoice;
	
	public void init()
	{
		//data
		data = ServantVoiceManager.getInstace().getData();
		List<ServantVoice> voices = data.getServantVoice();
		
		//ui
		Group_Conversation = transform.Find("Group_Conversation");
		Group_ServantVoice = transform.Find("Group_ServantVoice");
		Image_ServantImage = Group_Conversation.Find("Image_ServantImage").GetComponent<Image>();
		
		Button Button_ServantImage = Group_Conversation.Find("Image_ServantImage").GetComponent<Button>();
		initImage = Image_ServantImage.sprite;
		servantWord = Group_Conversation.Find("Group_ServantWord");
		servantWord.gameObject.SetActive(false);
		
		Text_Speaker = servantWord.transform.Find("Group_SpeakerBg/Text_Speaker").GetComponent<Text>();
		Text_Content = servantWord.transform.Find("Group_ContentBg/Text_Content").GetComponent<Text>();
	
		//action
		PlayVoice = voice =>
		{
			StopAllCoroutines();
			Image_ServantImage.sprite = initImage;
			AudioPlayer audioPlayer = AudioManager.getInstace().Play2DSound(voice.getVoiceId(), 
				() =>
				{
					servantWord.gameObject.SetActive(true);
					Text_Speaker.text = voice.getServantName();
					Text_Content.text = voice.getVoiceContent();
				},
				() =>
				{
					servantWord.gameObject.SetActive(false);
				}
			);
			Dictionary<float, string> timeToExpression = voice.getTimeToExpression();
			foreach (KeyValuePair<float,string> entry in timeToExpression)
			{
				if(entry.Key >= audioPlayer.length - 0.1f || entry.Key <= 0)
				{
					continue;
				}
				IEnumerator expressionCoroutine = ShowExpression(entry.Key, entry.Value , null);
				StartCoroutine(expressionCoroutine);
			}
			IEnumerator expressionEndCoroutine = ShowExpression(audioPlayer.length, null, initImage);
			StartCoroutine(expressionEndCoroutine);
		};
	
		
		Button_ServantImage.onClick.AddListener(() =>
		{
			int index = UnityEngine.Random.Range(0, voices.Count);
			ServantVoice voice = voices[index];
			
//			StopAllCoroutines();
//			Image_ServantImage.sprite = initImage;

			PlayVoice.Invoke(voice);
			
			//为了保证音频与台词的同步 应该在这里预先加载所有的资源在进行
			//ResourceManager.getInstace().preLoadResource();
//			AudioPlayer audioPlayer = AudioManager.getInstace().Play2DSound(voice.getVoiceId(), 
//				() =>
//				{
//					servantWord.gameObject.SetActive(true);
//					Text_Speaker.text = voice.getServantName();
//					Text_Content.text = voice.getVoiceContent();
//				},
//				() =>
//				{
//					servantWord.gameObject.SetActive(false);
//				}
//			);
			//expression voice.getTimeToExpression
			
//			Dictionary<float, string> timeToExpression = voice.getTimeToExpression();
//			foreach (KeyValuePair<float,string> entry in timeToExpression)
//			{
//				if(entry.Key >= audioPlayer.length - 0.1f || entry.Key <= 0)
//				{
//					continue;
//				}
//				IEnumerator expressionCoroutine = ShowExpression(entry.Key, entry.Value , null);
//				StartCoroutine(expressionCoroutine);
//			}
//			IEnumerator expressionEndCoroutine = ShowExpression(audioPlayer.length, null, initImage);
//			StartCoroutine(expressionEndCoroutine);
		});
		
		//右边
		Transform Tran_DropdownVoiceType = Group_ServantVoice.Find("Dropdown_VoiceType");
		Transform Tran_Content = Group_ServantVoice.Find("ScrollView/Viewport/Content");
		Dropdown dropDown = Tran_DropdownVoiceType.GetComponent<Dropdown>();
		List<Dropdown.OptionData> temp = new List<Dropdown.OptionData>();
//		foreach (int type in Enum.GetValues(typeof(CommonEnum.VoiceType)))
//		{
//			
//		}
		temp.Add(new Dropdown.OptionData("战斗"));
		temp.Add(new Dropdown.OptionData("召唤与强化"));
		temp.Add(new Dropdown.OptionData("个人空间"));
		temp.Add(new Dropdown.OptionData("活动"));
		dropDown.options = temp;
		dropDown.onValueChanged.AddListener(index =>
			{
				List<ServantVoice> typeVoices = data.getVoicesByType(index);
				Tran_Content.GetComponent<UIList>().DrawList(typeVoices.Count, (btnindex, obj) =>
				{
					ServantVoice voice = typeVoices[btnindex];
					obj.GetComponent<UIItem>().DrawItem(voice.getVoiceTitle(), () =>
					{
						PlayVoice.Invoke(voice);
//						AudioManager.getInstace().Play2DSound(v.getVoiceId(),
//							() =>
//							{
//								servantWord.gameObject.SetActive(true);
//								Text_Speaker.text = v.getServantName();
//								Text_Content.text = v.getVoiceContent();
//							},
//							() => { servantWord.gameObject.SetActive(false); });
					});
				});
			});
		dropDown.value = 3;
	}

	

	
	
	
	IEnumerator ShowExpression(float delayTime, string expression = null, Sprite sprite = null)
	{
		
		yield return new WaitForSeconds(delayTime);
		if (sprite == null)
		{
			Image_ServantImage.sprite = ResourceManager.getInstace().getSprite(expression);
		}
		else
		{
			Image_ServantImage.sprite = sprite;
		}
	}

	
	
	
	
	
	
	
	// 1、图集资源加载情况必须先了解清楚 加入resouremanager里
	// 2、资源管理器做一个预先加载的 保证播放时候 音频和台词是同步的
	// 3、如果资源尚未加载完成此时继续点击下一个，应该有一个遮罩 让当前界面不可以点击 
	// 4、通过配置读取资源的还未完成 
	// 23可以放在一个协程里做
	// audioPlayer很奇怪 不知道为什么会这样 必须在研究研究
}
