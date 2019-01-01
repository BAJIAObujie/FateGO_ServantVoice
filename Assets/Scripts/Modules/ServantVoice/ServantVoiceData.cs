using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantVoiceData{
    private List<ServantVoice> allVoice = new List<ServantVoice>();
    private Dictionary<int, List<ServantVoice>> typeToVoices;
    public ServantVoiceData()
    {
        CfgServantVoiceData cfg = ConfigManager.getInstace().getCfgServantVoiceData();
        foreach (CfgServantVoice voice in cfg.voices)
        {
            allVoice.Add(new ServantVoice(voice));
        }
        typeToVoices = new Dictionary<int, List<ServantVoice>>();
        foreach (int type in Enum.GetValues(typeof(CommonEnum.VoiceType)))
        {
            typeToVoices[type] = new List<ServantVoice>();
        }
        foreach (var voice in allVoice)
        {
            int type = voice.getVoiceType();
            typeToVoices[type].Add(voice);
        }
    }

    public List<ServantVoice> getServantVoice()
    {
        return allVoice;
    }

    public List<ServantVoice> getVoicesByType(int type)
    {
        List<ServantVoice> ret;
        if (typeToVoices.TryGetValue(type, out ret))
        {
            return ret;
        }
        return null;
    }

    

}
