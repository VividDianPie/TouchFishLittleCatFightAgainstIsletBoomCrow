using MagicaCloth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    GameObject mUIRoot;
    GameObject mGameRoot;
    void Awake()
    {
        mUIRoot = GameObject.Find("UI");
        mGameRoot = GameObject.Find("GameRoot");

        //初始化游戏管理者
        GameManager.Instance.init(mUIRoot, mGameRoot);

        //限制切换场景不销毁UI根节点
        GameObject.DontDestroyOnLoad(mUIRoot);

        //加载游戏配置
        GameCfgMgr ins = GameCfgMgr.Instance;

        //加载声音组件 初始化音量
        SoundMgr.Instance.Set();

        PlayMusicCtrl.Instance.ChangeMusic(0);

        //初始化物品配置
        try
        {
            ExcelsToItemHelpClass ih = ExcelsToItemHelpClass.Instance;
        }
        catch (Exception exp)
        {
            Debug.Log(exp);
        }

    }
    void Start()
    {
        GameObject.Find("MagicaPhysicsManager").GetComponent<MagicaPhysicsManager>().UpdateMode = UpdateTimeManager.UpdateMode.OncePerFrame;
    }


    void Update()
    {
        //二十四大设计模式之观测者模式开启观测
        EventManager.Instance.Update();
        //运行定时器
        TimerMgr.Instance.Update();
        UnScaleTimerManger.Instance.Update();
        PlayMusicCtrl.Instance.Run();
    }

}
