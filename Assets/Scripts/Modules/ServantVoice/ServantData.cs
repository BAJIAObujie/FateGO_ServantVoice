using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantData
{
    
    
    public List<Servant> nameToServant = new List<Servant>();
    public ServantData()
    {
        //Hard coding
        //Osakabehime
        //Ereshkigal
        //以后需要加上自动生成工具
        //string servantEName, string BGM, string BGSprite
        Servant Osakabehime = new Servant(
            "Osakabehime",
            "Halloween_ShopTheme",
            "HalloweenCity",
            new Vector2(-2.1f, 103f),
            new Vector2(-3.55f, 102.1f),
            new Vector2(-2.09f, 109.9f) 
            );
        Servant Ereshkigal = new Servant(
            "Ereshkigal",
            "Christmas_ShopTheme",
            "MerryChrismasTree",
            new Vector2(10f, 111.1f),
            new Vector2(11.1f, 111.9f),
            new Vector2(11.1f, 111f)
            );
        Servant CarenHortensia = new Servant(
            "CarenHortensia",
            "CarenTheme",
            "Church",
            new Vector2(10f, 111.1f),
            new Vector2(11.1f, 111.9f),
            new Vector2(11.1f, 111f)
        );
        nameToServant.Add(Osakabehime);
        nameToServant.Add(Ereshkigal);
        nameToServant.Add(CarenHortensia);
    }
    
    public Servant getServant(string servantEName)
    {
        foreach (var servant in nameToServant)
        {
            if (servant.servantEName == servantEName)
            {
                return servant;
            }
        }
        Debug.Log("not find servant");
        return null;
    }

    
}
