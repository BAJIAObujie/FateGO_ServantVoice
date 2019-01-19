using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantData
{

    private Dictionary<string, Servant> nameToServant = new Dictionary<string, Servant>();
    public ServantData()
    {
        //Hard coding
        //Osakabehime
        //Ereshkigal
        //以后需要加上自动生成工具

        Servant Osakabehime = new Servant("Osakabehime","Halloween_ShopTheme","Halloween","Osakabe_3a");
        Servant Ereshkigal = new Servant("Ereshkigal","Christmas_ShopTheme","null","Ereshkigal_Stage1_Sheeta");
        nameToServant.Add("Osakabehime",Osakabehime);
        nameToServant.Add("Ereshkigal",Ereshkigal);
    }
    
    public Servant getServant(string servantEName)
    {
        Servant ret;
        if (nameToServant.TryGetValue(servantEName, out ret))
        {
            return ret;
        }
        return null;
    }

    
}
