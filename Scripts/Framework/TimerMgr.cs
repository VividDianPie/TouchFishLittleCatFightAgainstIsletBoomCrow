using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ETimerType
{
    OneShot,
    Repeated,
}


public class Timer
{

    //当前这个定时器对象的唯一标识符
    public string key;


    //超时时间
    public float time;


    //当前数到的时间
    public float curTime;


    //定时器的类型 
    public ETimerType type;


    //回调函数
    public Action action;
    //瞬狱影杀阵
    public bool isDead;


    public Timer(string key, float time, ETimerType type, Action action)
    {
        curTime = 0;
        this.key = key;
        this.time = time;
        this.type = type;
        this.action = action;
        isDead = false;
    }
}



public class TimerMgr
{

    static TimerMgr sInstance = null;
    public static TimerMgr Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new TimerMgr();
            }
            return sInstance;
        }
    }

    //管理所有的定时器
    Dictionary<string, Timer> mAllTimerManagerMap;


    TimerMgr()
    {
        mAllTimerManagerMap = new Dictionary<string, Timer>();
    }


    //增
    public bool AddTimer(Timer timer)
    {
        if (mAllTimerManagerMap.ContainsKey(timer.key))
        {
            return false;
        }

        mAllTimerManagerMap.Add(timer.key, timer);

        return true;
    }


    //删
    public bool DeleteTimer(string key)
    {
        Timer timer = null;
        if (mAllTimerManagerMap.TryGetValue(key, out timer) == false)
        {
            return false;
        }
        timer.isDead = true;
        return true;
    }

    public bool DeleteAllTimer()
    {
        if (mAllTimerManagerMap.Count == 0) { return false; }
        string[] key = new string[mAllTimerManagerMap.Count];
        //将字典中的 Key 值从零开始拷贝至 mIds中
        mAllTimerManagerMap.Keys.CopyTo(key, 0);
        int count = mAllTimerManagerMap.Count;
        for (int i = 0; i < count; i++)
        {
            mAllTimerManagerMap[key[i]].isDead = true;
        }
        return true;
    }

    //OneShot
    public bool OneShot(string key, float time, Action action)
    {
        return AddTimer(new Timer(key, time, ETimerType.OneShot, action));
    }


    //Repeated
    public bool Repeated(string key, float time, Action action)
    {
        return AddTimer(new Timer(key, time, ETimerType.Repeated, action));
    }

    //运行定时器
    public void Update()
    {
        float dt = Time.deltaTime;
        List<string> needDel = new List<string>();
        foreach (var kv in mAllTimerManagerMap)
        {
            if (kv.Value.isDead)
            {
                needDel.Add(kv.Key);
                continue;
            }
            kv.Value.curTime += dt;
            if (kv.Value.curTime >= kv.Value.time)
            {
                kv.Value.curTime = 0f;
                kv.Value.action();
                if (kv.Value.type == ETimerType.OneShot)
                {
                    kv.Value.isDead = true;
                }
            }
            if (kv.Value.isDead)
            {
                needDel.Add(kv.Key);
            }
        }

        for (int i = 0; i < needDel.Count; i++)
        {
            mAllTimerManagerMap.Remove(needDel[i]);
        }
    }
}
