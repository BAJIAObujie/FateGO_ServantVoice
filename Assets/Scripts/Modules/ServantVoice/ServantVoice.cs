using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//类的变量可以只保存一个CfgServantVoice 这里是把CfgServantVoice拆开保存就是了
public class ServantVoice{
    private string servantName;
    private string servantEName;
    private int servantId;
    private string voiceId;
    private string voiceTitle;
    private string voiceContent;
    private int voiceType;
    private string defaultExpression;
    private Dictionary<float, string> timeToExpression;
    
    public ServantVoice(CfgServantVoice cfg){
        servantName = cfg.servantName;
        servantEName = cfg.servantEName;
        servantId = cfg.servantId;
        voiceId = cfg.voiceId;
        voiceTitle = cfg.voiceTitle;
        voiceContent = cfg.voiceContent;
        voiceType = cfg.voiceType;
        defaultExpression = cfg.defaultExpression;
        timeToExpression = new Dictionary<float, string>();
        timeToExpression.Add(cfg.time1,cfg.expression1);
        timeToExpression.Add(cfg.time2,cfg.expression2);
        timeToExpression.Add(cfg.time3,cfg.expression3);
        timeToExpression.Add(cfg.time4,cfg.expression4);
        timeToExpression.Add(cfg.time5,cfg.expression5);
        timeToExpression.Add(cfg.time6,cfg.expression6);
    }
    public string getServantName()
    {
        return servantName;
    }
    public string getServantEName()
    {
        return servantEName;
    }
    public string getVoiceId()
    {
        return voiceId;
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
