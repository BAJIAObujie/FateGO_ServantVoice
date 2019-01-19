using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant
{
	public string servantEName;
	public string BGM;
	public string BGSprite;
	public string defaultExpression;
	public ServantVoiceData voiceData;

	public Servant(string servantEName, string BGM, string BGSprite, string defaultExpression)
	{
		this.servantEName = servantEName;
		this.BGM = BGM;
		this.BGSprite = BGSprite;
		this.defaultExpression = defaultExpression;
		voiceData = new ServantVoiceData(servantEName);
	}
}
