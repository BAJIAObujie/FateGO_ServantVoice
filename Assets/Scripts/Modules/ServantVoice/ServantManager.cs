using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//目前的想法 数据不是加载两份 让Data代码由程序生成，配合Json数据直接生成数据实例 方法提供由Manager来提供 
public class ServantManager : SingleTonManager<ServantManager>, IManager
{
    private static ServantData data;

    public ServantData getData()
    {
        return data;
    }
    
    public void Init()
    {
        data = new ServantData();
    }

    /* ------- 唯一数据 ------- */
    
}
