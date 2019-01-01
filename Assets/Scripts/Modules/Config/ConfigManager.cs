using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager : SingleTonManager<ConfigManager>, IManager
{

    private static string servantVoiceDataPath;
    
    public void Init()
    {
        
        servantVoiceDataPath = "configurationData/ServantVoiceData";
        TextAsset ta = Resources.Load<TextAsset>(servantVoiceDataPath);
        //servantVoiceDataPath = Application.dataPath + "/Resources/configurationData/ServantVoiceData.json";
        //string jsonData = File.ReadAllText(servantVoiceDataPath);
        string jsonData = ta.text;
        cfgServantVoiceData = JsonUtility.FromJson<CfgServantVoiceData>(jsonData);
    }

    /* ------- 唯一数据 ------- */

    //可以改用泛型？
    private CfgServantVoiceData cfgServantVoiceData;
    public CfgServantVoiceData getCfgServantVoiceData()
    {
        if (cfgServantVoiceData == null)
        {
            
        }
        return cfgServantVoiceData;
    }
}
