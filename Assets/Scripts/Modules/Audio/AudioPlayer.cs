using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	public AudioSource audioSource;
	public float length;
	
	private Action onStart;
	private Action onEnd;
	private IEnumerator endIEnumerator;
	private Coroutine endCoroutine;

	public void setAudioSource(AudioSource audioSource)
	{
		this.audioSource = audioSource;
	}
	
	public void init(AudioClip audioClip, Action onStart = null, Action onEnd = null)
	{
		audioSource.clip = audioClip;
		length = audioClip.length;
		this.onStart = onStart;
		this.onEnd = onEnd;
		if (onEnd != null)
		{
			endIEnumerator = afterPlayFinish(length, onEnd);
		}
	}

	IEnumerator afterPlayFinish(float delayTime, Action onEnd)
	{
		yield return new WaitForSeconds(delayTime);
		onEnd.Invoke();
	}
	
	public void reset()
	{
		if (audioSource.clip == null)
		{
			return;
		}
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		audioSource.clip = null;
		length = 0;
		onStart = null;
		if (onEnd != null)
		{
			StopCoroutine(endCoroutine);
			onEnd = null;
			endIEnumerator = null;
			endCoroutine = null;
		}
	}

	public void play()
	{
		if (onStart != null)
		{
			onStart.Invoke();
		}
		audioSource.Play();
		if (onEnd != null)
		{
			endCoroutine = StartCoroutine(endIEnumerator);
		}
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
