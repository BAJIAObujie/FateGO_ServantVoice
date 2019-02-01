using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant
{
	public string servantEName;
	public string BGM;
	public string BGSprite;
	public ServantVoiceData voiceData;

	private CreateAssetExpression stage_one;
	private CreateAssetExpression stage_second;
	private CreateAssetExpression stage_third;
	private Dictionary<string, Sprite> sprite_one = new Dictionary<string, Sprite>();
	private Dictionary<string, Sprite> sprite_second = new Dictionary<string, Sprite>();
	private Dictionary<string, Sprite> sprite_third = new Dictionary<string, Sprite>();
	public Vector2 expressionLocation1;
	public Vector2 expressionLocation2;
	public Vector2 expressionLocation3;
	
	
	public Servant(string servantEName, string BGM, string BGSprite, Vector2 one, Vector2 two, Vector2 three)
	{
		Debug.Log("new Servant " + servantEName);
		this.servantEName = servantEName;
		this.BGM = BGM;
		this.BGSprite = BGSprite;
		voiceData = new ServantVoiceData(servantEName);
		expressionLocation1 = one;
		expressionLocation2 = two;
		expressionLocation3 = three;
		initExpression();
	}

	private void initExpression()
	{
		stage_one    = ResourceManager.getInstace().getAssetExpression(servantEName, "_Expression_Stage_01");
		stage_second = ResourceManager.getInstace().getAssetExpression(servantEName, "_Expression_Stage_02");
		stage_third  = ResourceManager.getInstace().getAssetExpression(servantEName, "_Expression_Stage_03");
		if (stage_one != null)
		{
			foreach (Sprite sprite in stage_one.expression)
			{
				sprite_one.Add(sprite.name,sprite);
			}
		}
		if (stage_second != null)
		{
			foreach (Sprite sprite in stage_second.expression)
			{
				sprite_second.Add(sprite.name,sprite);
			}
		}
		if (stage_third != null)
		{
			foreach (Sprite sprite in stage_third.expression)
			{
				sprite_third.Add(sprite.name,sprite);
			}
		}
	}

	public Sprite getDefaultExpression(int stage)
	{
		return getExpression(stage, "0");
	}
	
	public Sprite getExpression(int stage, string expression)
	{
		Dictionary<string, Sprite> temp;
		if (stage == 1)
		{
			temp = sprite_one;
		}
		else if (stage == 2)
		{
			temp = sprite_second;
		}
		else if (stage == 3)
		{
			temp = sprite_third;
		}
		else
		{
			Debug.Log("wrong stage");
			return null;
		}
		if (temp == null)
		{
			Debug.Log("Dictionary<string, Sprite> temp == null");
		}
		
		Sprite sprite;
		string spritePath = string.Format("{0}_Stage{1}_{2}", servantEName,stage,expression);
		Debug.Log("spritePath " + spritePath);
		if (temp.TryGetValue(spritePath, out sprite))
		{
			return sprite;
		}
		Debug.Log(string.Format("stage {0}, expression {1}, sprite is null",stage,expression));
		return ResourceManager.transparentPNG;
	}
}
