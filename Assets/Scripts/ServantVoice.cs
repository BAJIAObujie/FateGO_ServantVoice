using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantVoice{

    private int servantId;
    private string resourcePath;
    private string voiceWord;

    public ServantVoice(int servantId, string resourcePath, string voiceWord){
        this.servantId = servantId;
        this.resourcePath = resourcePath;
        this.voiceWord = voiceWord;
    }

    public string getVoicePath(){
        return string.Format("{0}_{1}", servantId, resourcePath);
    }

}
