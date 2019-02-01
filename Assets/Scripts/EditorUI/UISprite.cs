using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISprite : Image
{
	private string m_SpriteAltas;
	private string m_Sprite;
	
	public CreateAssetExpression expression;

	public string spriteAltas
	{
		get { return m_SpriteAltas; }
		set { m_SpriteAltas = value; }
	}

	public string sprite
	{
		get { return m_Sprite; }
		set
		{
			
			m_Sprite = value;
		}
	}


	public static void loadSpriteAltas(string path)
	{
		
	}
	
}
