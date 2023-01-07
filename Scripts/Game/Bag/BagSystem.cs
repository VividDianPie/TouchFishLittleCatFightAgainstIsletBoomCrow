using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;


public class BagSystem 
{
    //key:物品id value:数量
    Dictionary<int, int> dicOutfitIdAndCount;

    public Dictionary<int, int> dicOutfitIdAndCounts
    {
        get
        {
            return dicOutfitIdAndCount;
        }
    }


    static BagSystem sInstance = null;
    public static BagSystem Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new BagSystem();
            }
            return sInstance;
        }
    }


    BagSystem()
    {
        //数据加载
        dicOutfitIdAndCount = GameCfgMgr.Instance.BagItems;
        if (dicOutfitIdAndCount == null)
        {
            dicOutfitIdAndCount = new Dictionary<int, int>();
        }
    }


    //增加物品
    public void AddItem(int id, int count)          //  1001      蒂蒂的虎鲸抱枕                //     1002      雾切之回光                   //     1003     魂妖刀_血樱寂灭
    {
        //得需检查有没有相应物品的配置
        ExcelsToOutFitInformation outfitInfo = ExcelsToItemHelpClass.Instance.IdToFindOutfitInformation(id);
        if (outfitInfo != null)
        {
            if (dicOutfitIdAndCount.ContainsKey(id))
            {
                dicOutfitIdAndCount[id] += count;
            }
            else
            {
                dicOutfitIdAndCount.Add(id, count);
            }

            if (dicOutfitIdAndCount[id] <= 0)
            {
                dicOutfitIdAndCount.Remove(id);
            }
        }
        else
        {
            throw new System.Exception($"没有id: {id} 的配置!");
        }
        SaveCfg();
    }


    //存储数据
    public void SaveCfg()
    {
        GameCfgMgr.Instance.SaveBag(dicOutfitIdAndCount);
    }

}
