using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTonManager<ResourceManager>, IManager{
    //还得看一下resources.load之后，系统内部是如何释放资源的
	private static Dictionary<string, Object> pathToObject;

	public void Init()
	{
		pathToObject = new Dictionary<string, Object>();

	}
	
	/* ------- 唯一数据 ------- */

	public void preLoadResource()
	{
		
	}
	
	public GameObject getUI(string uiName)
	{
		Object obj;
		string path = "UIPrefabs/" + uiName;
		if (pathToObject.TryGetValue(path, out obj))
		{
			return (GameObject) obj;
		}
		GameObject res = Resources.Load<GameObject>(path);
		if (res == null)
		{
			Debug.Log(string.Format("UIPrefab {0} not exist",path));
		}
		pathToObject.Add(path,res);
		return res;
	}
	
	public AudioClip getAudioClip(int audioId)
	{
		//Resources.UnloadUnusedAssets()
		Object obj;
		string path = "Voices/" + audioId;
		if (pathToObject.TryGetValue(path, out obj))
		{
			return (AudioClip) obj;
		}
		AudioClip res = Resources.Load<AudioClip>(path);
		if (res == null)
		{
			Debug.Log(string.Format("AudioClip {0} not exist",path));
		}
		pathToObject.Add(path,res);
		return res;
	}

	public Sprite getSprite(string spriteName)
	{
		string path = string.Format("ServantImage/刑部姬/{0}", spriteName);
		return Resources.Load<Sprite>(path);
	}
	
}
