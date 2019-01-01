using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingleTonManager<AudioManager> ,IManager
{
	public AudioPlayer audioPlayer;

	//不能在这里使用new 创建monobehavior必须在
	//You are trying to create a MonoBehaviour using the 'new' keyword.
	//This is not allowed.  MonoBehaviours can only be added using AddComponent().
	//Alternatively, your script can inherit from ScriptableObject or no base class at all
	public void Init()
	{
		GameObject camera = GameObject.Find("Main Camera");
		AudioPlayer audioPlayer = camera.AddComponent<AudioPlayer>();
		AudioSource audioSource = camera.GetComponent<AudioSource>();
		audioPlayer.setAudioSource(audioSource);
	}

	private AudioPlayer getAudioPlayer()
	{
		return GameObject.Find("Main Camera").GetComponent<AudioPlayer>();
	}
	
	public AudioPlayer Play2DSound(int voiceId,Action onStart = null, Action onEnd = null)
	{
		//var clip = Resources.Load<AudioClip>(voicePath);
		AudioClip audioClip = ResourceManager.getInstace().getAudioClip(voiceId);
		audioPlayer = getAudioPlayer();
		if (audioPlayer == null)
		{
			Debug.Log("audioPlayer == null");
			return null;
		}
		audioPlayer.reset();
		audioPlayer.init(audioClip,onStart,onEnd);
		audioPlayer.play();
		return audioPlayer;
	}
}
