using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTonManager<ResourceManager>, IManager
{

	public static Sprite transparentPNG;
	
	public void Init()
	{
		transparentPNG = Resources.Load<Sprite>("Sprites/TransparentPNG");
		Debug.Log(transparentPNG);
	}
	
	/* ------- 唯一数据 ------- */
	
	public GameObject getUI(string uiName)
	{
		string path = "UIPrefabs/" + uiName;
		GameObject res = Resources.Load<GameObject>(path);
		if (res == null)
		{
			Debug.Log(string.Format("UIPrefab {0} not exist",path));
		}
		return res;
	}

	public AudioClip getBGM(string bgm)
	{
		string path = string.Format("BGM/{0}", bgm);
		AudioClip ret = Resources.Load<AudioClip>(path);
		if (ret == null)
		{
			Debug.Log(string.Format("bgm {0} not exist",path));
		}
		return ret;
	}
	
	public AudioClip getServantVoice(string servantName,string voiceId)
	{
		//Resources.UnloadUnusedAssets()
		string path = string.Format("ServantData/{0}/VoiceData/{1}",servantName, voiceId);
		AudioClip ret = Resources.Load<AudioClip>(path);
		if (ret == null)
		{
			Debug.Log(string.Format("AudioClip {0} not exist",path));
		}
		return ret;
	}

	public Sprite getBGSprite(string bg)
	{
		string path = string.Format("BG/{0}",bg);
		Sprite ret = Resources.Load<Sprite>(path);
		if (ret == null)
		{
			Debug.Log(string.Format("BGSprite {0} not exist",path));
		}
		return ret;
	}
	
	public Sprite getServantExpression(string servantName,string expression)
	{
		string path = string.Format("ServantData/{0}/ExpressionData/{1}",servantName, expression);
		Sprite ret = Resources.Load<Sprite>(path);
		if (ret == null)
		{
			Debug.Log(string.Format("ServantExpression sprite {0} not exist",path));
		}
		return ret;
	}

	public CreateAssetExpression getAssetExpression(string servantEName, string fileName)
	{
		string path = string.Format("ServantData/{0}/ExpressionData/{1}{2}", servantEName, servantEName, fileName); 
		return Resources.Load<CreateAssetExpression>(path);
	}
	
}
