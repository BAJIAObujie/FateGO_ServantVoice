using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DlgShop : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		transform.Find("Button_Back").GetComponent<Button>().onClick.AddListener
		(
			() => { OnHide();}
		);
	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}

    //data
	private Servant servant;
	private ServantVoiceData data;
	private float length;
	private float leftlength;
	private Sprite defaultExpression;
	
	//ui component
	private Transform Group_Bg;
	private AudioSource BGMPlayer;
	private AudioPlayer VoicePlayer;
	
	private Transform Group_Conversation;
	private Transform Group_ServantVoice;

	private Transform servantWord;
	private Text Text_Speaker;
	private Text Text_Content;
	private Image Image_ServantImage;
	
	//action
	private Action<ServantVoice> PlayVoice;
	
	public void OnShow(string servantEName)
	{
		//data
		servant = ServantManager.getInstace().getData().getServant(servantEName);
		data = servant.voiceData;
		List<ServantVoice> voices = data.getServantVoice();
		defaultExpression = ResourceManager.getInstace().getServantExpression(servantEName, servant.defaultExpression);
		
		//ui
		Group_Bg = transform.Find("Group_Bg");
		Group_Conversation = transform.Find("Group_Conversation");
		Group_ServantVoice = transform.Find("Group_ServantVoice");
		Image_ServantImage = Group_Conversation.Find("Image_ServantImage").GetComponent<Image>();
		
		Button Button_ServantImage = Group_Conversation.Find("Image_ServantImage").GetComponent<Button>();
		Image_ServantImage.sprite = defaultExpression;
		servantWord = Group_Conversation.Find("Group_ServantWord");
		servantWord.gameObject.SetActive(false);
		
		Text_Speaker = servantWord.transform.Find("Group_SpeakerBg/Text_Speaker").GetComponent<Text>();
		Text_Content = servantWord.transform.Find("Group_ContentBg/Text_Content").GetComponent<Text>();
	
		//bg and bgm
		Group_Bg.GetComponent<Image>().sprite = ResourceManager.getInstace().getBGSprite(servant.BGSprite);
		BGMPlayer = Group_Bg.GetComponent<AudioSource>();
		BGMPlayer.clip = ResourceManager.getInstace().getBGM(servant.BGM);
		BGMPlayer.Play();
		
		//action
		PlayVoice = voice =>
		{
//			if (VoicePlayer.audioSource.isPlaying)
//			{
//				
//			}
			StopAllCoroutines();
			Image_ServantImage.sprite = defaultExpression;
			VoicePlayer = AudioManager.getInstace().Play2DSound(servantEName,voice.getVoiceId(), 
				() =>
				{
					servantWord.gameObject.SetActive(true);
					Text_Speaker.text = voice.getServantName();
					Text_Content.text = voice.getVoiceContent();
				},
				() =>
				{
					Image_ServantImage.sprite = defaultExpression;
					servantWord.gameObject.SetActive(false);
				}
			);
			Dictionary<float, string> timeToExpression = voice.getTimeToExpression();
			foreach (KeyValuePair<float,string> entry in timeToExpression)
			{
				if(entry.Key >= VoicePlayer.length - 0.1f || entry.Key <= 0)
				{
					continue;
				}
				IEnumerator expressionCoroutine = ShowExpression(entry.Key, servantEName, entry.Value);
				StartCoroutine(expressionCoroutine);
			}
			//IEnumerator expressionEndCoroutine = ShowExpression(VoicePlayer.length, null, defaultExpression);
			//StartCoroutine(expressionEndCoroutine);
		};
	
		Button_ServantImage.onClick.AddListener(() =>
		{
			int index = UnityEngine.Random.Range(0, voices.Count);
			ServantVoice voice = voices[index];
			PlayVoice.Invoke(voice);
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
					});
				});
			});
		dropDown.value = 4;
		dropDown.value = 3;  
	}

	IEnumerator ShowExpression(float delayTime, string servantEName, string expression = null)
	{
		yield return new WaitForSeconds(delayTime);
		Image_ServantImage.sprite = ResourceManager.getInstace().getServantExpression(servantEName,expression);
	}

	public void OnHide()
	{
		BGMPlayer.Stop();
		if (VoicePlayer != null)
		{
			VoicePlayer.reset();
		}
		UIManager.getInstace().HideCurrentDialog();
	}
	
	
}
