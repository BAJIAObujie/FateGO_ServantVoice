using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantVoiceData
{
    public List<ServantVoice> listVoices = new List<ServantVoice>();
    public ServantVoiceData(string ServantEName)
    {
        CfgServantVoiceData cfg = ConfigManager.getInstace().getCfgServantVoiceData();
        foreach (CfgServantVoice cfgServantVoice in cfg.voices)
        {
            if (cfgServantVoice.servantEName == ServantEName)
            {
                ServantVoice data = new ServantVoice(cfgServantVoice);
                listVoices.Add(data);
            }
        }
    }

    public List<ServantVoice> getServantVoice()
    {
        return listVoices;
    }
    
    public List<ServantVoice> getVoicesByType(int type)
    {
        List<ServantVoice> ret = new List<ServantVoice>();
        foreach (var voice in listVoices)
        {
            if (voice.getVoiceType() == type)
            {
                ret.Add(voice);
            }
        }
        return ret;
    }
}
