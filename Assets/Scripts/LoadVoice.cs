using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVoice : MonoBehaviour {

    string basePath = "Voices/";
    string voicePath = "106_1";

	// Use this for initialization
	void Start () {
        
	}
	
    public void play(){
		string path = basePath + voicePath;
        Debug.Log(path);
		AudioClip audioClip = (AudioClip)Resources.Load(path, typeof(AudioClip));
        if(audioClip == null){
            Debug.Log("null");
        }
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = audioClip;
        audioSource.Play();
    }

	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 50), "play"))
		{
            play();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
