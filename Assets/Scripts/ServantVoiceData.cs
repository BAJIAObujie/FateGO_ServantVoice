using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantVoiceData{
    private Dictionary<int, ServantVoice> idToVoice;
    private char[] separator = { ',' };
    ServantVoiceData(){
        
    }

    private ServantVoice resolveCfgToVoice(string cfgString){
        string[] stringArray = cfgString.Split(separator);
        int ServantId = int.Parse(stringArray[0]);
        string resourcePath = stringArray[1];
        string voiceWord = stringArray[2];
        return new ServantVoice(ServantId,resourcePath,voiceWord);
    }


}
