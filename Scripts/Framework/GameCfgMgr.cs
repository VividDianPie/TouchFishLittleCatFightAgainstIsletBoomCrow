using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Utils;


//游戏配置管理类
public class GameCfgMgr 
{

    static GameCfgMgr sInstance = null;
    public static GameCfgMgr Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameCfgMgr();
            }
            return sInstance;
        }
    }
    
    static string sFileName = "GameCfgMgr.Cfg";
    GameCfg mCfg;

    GameCfgMgr()
    {
        mCfg = null;
        Load();
    }

    //加载存档
    public void Load()
    {
        //判断存档文件是否存在
        if (FileUtils.IsFileExist(Application.temporaryCachePath + "/" + sFileName))
        {
            //读文件
            byte[] buf = null;
            FileUtils.ReadFromFile(Application.temporaryCachePath + "/" + sFileName, out buf);
            if (buf != null)
            {
                //转换数据
                string jsonStr = System.Text.Encoding.UTF8.GetString(buf);
                mCfg = JsonConvert.DeserializeObject<GameCfg>(jsonStr);
            }
        }
        else
        {
            mCfg = new GameCfg();
        }
    }

    //存储存档
    public void Save()
    {
        if (mCfg == null)
        {
            return;
        }

        //转json
        string jsonStr = JsonConvert.SerializeObject(mCfg);
        byte[] datas = System.Text.Encoding.UTF8.GetBytes(jsonStr);

        //写入文件
        Debug.Log("CfgPath:" + Application.temporaryCachePath + "/" + sFileName);
        FileUtils.WriteToFile(Application.temporaryCachePath + "/" + sFileName, datas);
    }


    //音乐开关
    public bool MusicOn
    {
        get
        {
            return mCfg.MusicOn == 1;
        }
        set
        {
            mCfg.MusicOn = value ? 1 : 0;
            Save();
        }
    }

    //音效开关
    public bool EffectOn
    {
        get
        {
            return mCfg.EffectOn == 1;
        }
        set
        {
            mCfg.EffectOn = value ? 1 : 0;
            Save();
        }
    }


    //保存背包物品
    public void SaveBag(Dictionary<int, int> bagItems)
    {
        mCfg.BagItems = bagItems;
        Save();
    }


    public Dictionary<int, int> BagItems
    {
        get
        {
            return mCfg.BagItems;
        }
    }

}
