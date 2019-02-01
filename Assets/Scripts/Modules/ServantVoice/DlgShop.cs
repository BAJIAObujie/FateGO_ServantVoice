using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DlgShop : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		//ui
		initAnchor = new Vector2(0.25f,0);
		centerAnchor = new Vector2(0.5f,0);
		usedTime = 0;
		totalTime = 1;
		
		// Group
		Group_Bg = transform.Find("Group_Bg");
		Group_ServantImage = transform.Find("Group_ServantImage");
		Group_ServantWord = transform.Find("Group_ServantWord");
		Group_ServantVoice = transform.Find("Group_ServantVoice");
		Group_Setting = transform.Find("Group_Setting");
		
		// Expression
		Image_ServantImage = Group_ServantImage.Find("Image_ServantImage").GetComponent<Image>();
		RectTran_ServantImage = Image_ServantImage.GetComponent<RectTransform>();
		Transform_ServantExpression = Group_ServantImage.Find("Image_ServantImage/Image_ServantExpression");
		Image_ServantExpression = Transform_ServantExpression.GetComponent<Image>();
		
		// Button
		Button_ServantImage = Group_ServantImage.Find("Image_ServantImage").GetComponent<Button>();
		Button_DefaultTheme = Group_Setting.Find("Button_DefaultTheme").GetComponent<Button>();
		Button_ChangeTheme = Group_Setting.Find("Button_ChangeTheme").GetComponent<Button>();
		Button_BGMStatus = Group_Setting.Find("Button_BGMStatus").GetComponent<Button>();
		Button_ChangeStage = Group_Setting.Find("Button_ChangeStage").GetComponent<Button>();
		Button_Back = Group_Setting.Find("Button_Back").GetComponent<Button>();
		Button_Switch = transform.Find("Button_Switch").GetComponent<Button>();
		
		// Word
		servantWord_Narrow = Group_ServantWord.Find("Group_Narrow");
		servantWord_Wide = Group_ServantWord.Find("Group_Wide");
		servantWord_Narrow.gameObject.SetActive(false);
		servantWord_Wide.gameObject.SetActive(false);
		
		Text_Speaker_Narrow = servantWord_Narrow.transform.Find("Group_SpeakerBg/Text_Speaker").GetComponent<Text>();
		Text_Content_Narrow = servantWord_Narrow.transform.Find("Group_ContentBg/Text_Content").GetComponent<Text>();
		Text_Speaker_Wide = servantWord_Wide.transform.Find("Group_SpeakerBg/Text_Speaker").GetComponent<Text>();
		Text_Content_Wide = servantWord_Wide.transform.Find("Group_ContentBg/Text_Content").GetComponent<Text>();

		// BG & BGM
		BGMPlayer = Group_Bg.GetComponent<AudioSource>();
		BGSprite = Group_Bg.GetComponent<Image>();
		BGMAndBG = new List<KeyValuePair<string, string>>();
		BGMAndBG.Add(new KeyValuePair<string, string>("MyRoom","MyRoom"));
		BGMAndBG.Add(new KeyValuePair<string, string>("MyRoom_NewYear","HappyNewYear"));

		RegistStaticButton();
	}
	
	// Update is called once per frame
	void Update () {
		if (needUpdate)
		{
			usedTime += Time.deltaTime * 3; // 2是速度 总时间是1，2即代表在0.5秒内完成操作！
			Vector2 temp = Vector2.Lerp(from, to, usedTime);
			RectTran_ServantImage.anchorMin = temp;
			RectTran_ServantImage.anchorMax = temp;
			if (usedTime >= totalTime)
			{
				needUpdate = false;
				usedTime = 0;
				UIManager.getInstace().SetBCanInteract(true);
				if (uiAnimationEnd != null)
				{
					uiAnimationEnd.Invoke();
				}
			}
		}
	}
	
    //data
	private Servant servant;
	private ServantVoiceData data;
	
	private List<KeyValuePair<string,string>> BGMAndBG;
	private int currMode;
	private int currTheme;
	private int servantStage;
	
	//ui component
	private RectTransform RectTran_ServantImage;

	private Transform Group_Bg;
	private Image BGSprite;
	private AudioSource BGMPlayer;
	private AudioPlayer VoicePlayer;
	
	private Transform Group_ServantImage;
	private Transform Group_ServantWord;
	private Transform Group_ServantVoice;
	private Transform Group_Setting;

	private Button Button_ServantImage;
	private Button Button_DefaultTheme;
	private Button Button_ChangeTheme;
	private Button Button_BGMStatus;
	private Button Button_ChangeStage;
	private Button Button_Back;
	private Button Button_Switch;

	private Transform servantWord_Narrow;
	private Text Text_Speaker_Narrow;
	private Text Text_Content_Narrow;
	private Transform servantWord_Wide;
	private Text Text_Speaker_Wide;
	private Text Text_Content_Wide;
	private Image Image_ServantImage;
	private Image Image_ServantExpression;

	private Transform Transform_ServantExpression;
	
	//ui animation
	private bool needUpdate;
	private Vector2 initAnchor;
	private Vector2 centerAnchor;
	private Vector2 from;
	private Vector2 to;
	private float usedTime;
	private float totalTime;
	
	//action
	private Action<ServantVoice> PlayVoice;
	private Action uiAnimationEnd;

	private bool isPlayingVoice()
	{
		bool flag = VoicePlayer != null && VoicePlayer.audioSource.clip != null &&
		            VoicePlayer.audioSource.isPlaying;
	    return flag;
	}
	
	
	public void OnShow(string servantEName)
	{
		//animation
		needUpdate = false;
		
		//data
		servant = ServantManager.getInstace().getData().getServant(servantEName);
		data = servant.voiceData;
		List<ServantVoice> voices = data.getServantVoice();
		servantStage = 1;
		currMode = 1;
		currTheme = 0;
		
		//ui reinit
		RectTran_ServantImage.anchorMin = initAnchor;
		RectTran_ServantImage.anchorMax = initAnchor;
		Image_ServantImage.sprite = servant.getDefaultExpression(servantStage);
		Transform_ServantExpression.localPosition = servant.expressionLocation1;
		SetExpression(Image_ServantExpression,null);
		
		//刑部姬戴帽子 造型 stage1 x-2.1 y103
		//                stage2 x-3.55 y102.1
		//                        -2.09 109.9
		
		servantWord_Narrow.gameObject.SetActive(false);
		servantWord_Wide.gameObject.SetActive(false);
		//右边的界面的显隐也要控制
		
		//bg and bgm
		Group_Bg.GetComponent<Image>().sprite = ResourceManager.getInstace().getBGSprite(servant.BGSprite);
		BGMPlayer = Group_Bg.GetComponent<AudioSource>();
		BGMPlayer.clip = ResourceManager.getInstace().getBGM(servant.BGM);
		BGMPlayer.Play();
		
		//playVoice action
		PlayVoice = voice =>
		{
			//模仿对话界面 如果当前正在播放语音 那么关闭语音。如果没有播放语音则播放
			//想这两种对话框 其实应该提取出来 作为一个view的 不应该绑定在具体哪一个界面上
			if (isPlayingVoice())
			{
				ForceCloseVoice();
				return;
			}
			
			VoicePlayer = AudioManager.getInstace().Play2DSound(servantEName,voice.getVoiceId(), 
				() =>
				{
					if (currMode != 2)
					{
						servantWord_Narrow.gameObject.SetActive(true);
						Text_Speaker_Narrow.text = voice.getServantName();
						Text_Content_Narrow.text = voice.getVoiceContent();
					}
					else
					{
						servantWord_Wide.gameObject.SetActive(true);
						Text_Speaker_Wide.text = voice.getServantName();
						Text_Content_Wide.text = voice.getVoiceContent();
					}
					
				},
				() =>
				{
					SetExpression(Image_ServantExpression,null);
					if (currMode != 2)
					{
						servantWord_Narrow.gameObject.SetActive(false);
					}
					else
					{
						servantWord_Wide.gameObject.SetActive(false);
					}
				}
			);
			Dictionary<float, string> timeToExpression = voice.getTimeToExpression();
			foreach (KeyValuePair<float,string> entry in timeToExpression)
			{
				if(entry.Key >= VoicePlayer.length - 0.1f || entry.Key <= 0)
				{
					continue;
				}
				IEnumerator expressionCoroutine = ShowExpression(entry.Key, entry.Value);
				StartCoroutine(expressionCoroutine);
			}
//			IEnumerator expressionEndCoroutine = ShowExpression(VoicePlayer.length, null);
//			StartCoroutine(expressionEndCoroutine);
		};
		Button_ServantImage.onClick.RemoveAllListeners();
		Button_ServantImage.onClick.AddListener(() =>
		{
			int index = UnityEngine.Random.Range(0, voices.Count);
			ServantVoice voice = voices[index];
//			ServantVoice voice;
//			while (true)
//			{
//				voice = voices[index];
//				if (voice.getVoiceType() == 2 || voice.getVoiceType() == 3)
//				{
//					break;
//				}
//			}
            PlayVoice.Invoke(voice);
		});
		
		//右边
		Transform Tran_DropdownVoiceType = Group_ServantVoice.Find("Dropdown_VoiceType");
		Transform Tran_Content = Group_ServantVoice.Find("ScrollView/Viewport/Content");
		Dropdown dropDown = Tran_DropdownVoiceType.GetComponent<Dropdown>();
		dropDown.ClearOptions();
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
					});
				});
			});
		dropDown.value = 4;
		dropDown.value = 3;
	}

	IEnumerator ShowExpression(float delayTime, string expression)
	{
		yield return new WaitForSeconds(delayTime);
		SetExpression(Image_ServantExpression,expression);
	}
	private void SetExpression(Image image, string expression)
	{
		if (expression != null)
		{
			image.sprite = servant.getExpression(servantStage,expression);
		}
		else
		{
			image.sprite = ResourceManager.transparentPNG;
		}
	}

	
	private void SwitchMode()
	{
		//能被点击说明当前并没有在进行动画，因为有一个Mask会遮挡点击事件
		//动画期间或者正在播放语音期间不接收命令
		if (needUpdate || isPlayingVoice())
		{
			return;
		}
		// currMode在0-2之间循环
		currMode++;
		if (currMode == 3)
		{
			currMode = 0;
		}
		
		switch (currMode)
		{
			case 0:
			{
				needUpdate = true;
				UIManager.getInstace().SetBCanInteract(false);
				uiAnimationEnd = () =>
				{
					Group_ServantVoice.gameObject.SetActive(true);
				};
				from = centerAnchor;
				to = initAnchor;
				break;
			}
			case 1:
			{
				Group_ServantVoice.gameObject.SetActive(false);
				Group_Setting.gameObject.SetActive(true);
				needUpdate = false;
				uiAnimationEnd = null;
				break;
			}
			case 2:
			{
				Group_Setting.gameObject.SetActive(false);
				needUpdate = true;
				UIManager.getInstace().SetBCanInteract(false);
				uiAnimationEnd = null;
				from = initAnchor;
				to = centerAnchor;
				break;
			}
		}

	}

	private void RegistStaticButton()
	{
		Button_DefaultTheme.onClick.AddListener(() =>
		{
			ChangeTheme(servant.BGM, servant.BGSprite);
		});
		Button_ChangeTheme.onClick.AddListener(() =>
		{
			int count = BGMAndBG.Count;
			if (count == 0)
			{
				return;
			}
			currTheme++;
			
			if (currTheme == count)
			{
				currTheme = 0;
			}
			ChangeTheme(BGMAndBG[currTheme].Key, BGMAndBG[currTheme].Value);
		});
	    Button_BGMStatus.onClick.AddListener(ChangeBGMStatus);
	    Button_ChangeStage.onClick.AddListener(ChangeStage);
	    Button_Back.onClick.AddListener(OnHide);
		Button_Switch.onClick.AddListener(SwitchMode);
	}
	
	private void ChangeTheme(string BGM, string BG)
	{
		BGSprite.sprite = ResourceManager.getInstace().getBGSprite(BG);
		BGMPlayer.clip = ResourceManager.getInstace().getBGM(BGM);
		BGMPlayer.Play();
	}

	private void ChangeBGMStatus()
	{
		if (BGMPlayer.isPlaying)
		{
			BGMPlayer.Pause();
		}
		else
		{
			BGMPlayer.Play();
		}
	}

	private void ChangeStage()
	{
		ForceCloseVoice();
		servantStage++;
		if (servantStage > 3)
		{
			servantStage = 1;
		}

		if (servantStage == 1)
		{
			Transform_ServantExpression.localPosition = servant.expressionLocation1;
		}
		else if (servantStage == 2)
		{
			Transform_ServantExpression.localPosition = servant.expressionLocation2;
		}
		else if (servantStage == 3)
		{
			Transform_ServantExpression.localPosition = servant.expressionLocation3;
		}
		
		SetExpression(Image_ServantExpression,null);
		Image_ServantImage.sprite = servant.getDefaultExpression(servantStage);
	}
	
	
	
	
	//原来是这样 再次进入界面相当于 重复注册了一次功能！！！！！
	private void OnHide()
	{
		BGMPlayer.Stop();
		ForceCloseVoice();
		//gameObject.SetActive(false);
		//UIManager.getInstace().showbefore();
		UIManager.getInstace().HideCurrentDialog();
	}
	
	//通过返回按键等等 强制需要关闭当前正在进行的语音。
	private void ForceCloseVoice()
	{
		StopAllCoroutines();
		if (VoicePlayer != null)
		{
			VoicePlayer.reset();
		}
		SetExpression(Image_ServantExpression,null);
		servantWord_Narrow.gameObject.SetActive(false);
		servantWord_Wide.gameObject.SetActive(false);
	}
	
}
