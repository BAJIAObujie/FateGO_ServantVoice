using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//类的变量可以只保存一个CfgServantVoice 这里是把CfgServantVoice拆开保存就是了
public class ServantVoice{
    private int servantId;
    private string servantName;
    private int voiceId;
    private string voiceTitle;
    private string voiceContent;
    private int voiceType;
    private string defaultExpression;
    private Dictionary<float, string> timeToExpression;
    
    
    public ServantVoice(CfgServantVoice cfg){
        servantId = cfg.servantId;
        servantName = cfg.servantName;
        voiceId = cfg.voiceId;
        voiceTitle = cfg.voiceTitle;
        voiceContent = cfg.voiceContent;
        voiceType = cfg.voiceType;
        defaultExpression = cfg.defaultExpression;
        timeToExpression = new Dictionary<float, string>();
        timeToExpression.Add(cfg.time0,cfg.expression0);
        timeToExpression.Add(cfg.time1,cfg.expression1);
        timeToExpression.Add(cfg.time2,cfg.expression2);
    }

    
    public int getVoiceId()
    {
        return voiceId;
    }

    public string getServantName()
    {
        return servantName;
    }

    public string getVoiceTitle()
    {
        return voiceTitle;
    }
    
    public string getVoiceContent()
    {
        return voiceContent;
    }

    public int getVoiceType()
    {
        return voiceType;
    }

    public string getDefaultExpression()
    {
        return defaultExpression;
    }

    public Dictionary<float, string> getTimeToExpression()
    {
        return timeToExpression;
    }
    

}
