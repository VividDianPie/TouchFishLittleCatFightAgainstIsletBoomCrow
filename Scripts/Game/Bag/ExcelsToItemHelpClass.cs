using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using Newtonsoft.Json;

public class ExcelsToItemHelpClass
{
    static ExcelsToItemHelpClass sInstance = null;
    public static ExcelsToItemHelpClass Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new ExcelsToItemHelpClass();
            }
            return sInstance;
        }
    }


    //所有的配置   装备Id 与 装备信息
    Dictionary<int, ExcelsToOutFitInformation> dictionaryBagInfo;


    ExcelsToItemHelpClass()
    {
        //mAllCfg = new Dictionary<int, Item>();

        //加载配置
        TextAsset txt = Resources.Load<TextAsset>("Configs/ExcelsToOutFitInformation");

        //反序列化
        dictionaryBagInfo = JsonConvert.DeserializeObject<Dictionary<int, ExcelsToOutFitInformation>>(txt.text);
    }


    public ExcelsToOutFitInformation IdToFindOutfitInformation(int id)
    {
        ExcelsToOutFitInformation outFitInformation = null;
        dictionaryBagInfo.TryGetValue(id, out outFitInformation);
        return outFitInformation;
    }
}
