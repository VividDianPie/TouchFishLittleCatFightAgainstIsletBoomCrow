using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EUnScaleTimerType
{
    OneShot,
    Repeated,
}

//�ӵ�ʱ��ʱ�����
public class UnScaleTimer
{

    //��ǰ�����ʱ�������Ψһ��ʶ��
    public string key;


    //��ʱʱ��
    public float time;


    //��ǰ������ʱ��
    public float curTime;


    //��ʱ�������� 
    public ETimerType type;


    //�ص�����
    public Action action;
    //˲��Ӱɱ��
    public bool isDead;


    public UnScaleTimer(string key, float time, ETimerType type, Action action)
    {
        curTime = 0;
        this.key = key;
        this.time = time;
        this.type = type;
        this.action = action;
        isDead = false;
    }
}



public class UnScaleTimerManger
{

    static UnScaleTimerManger sInstance = null;
    public static UnScaleTimerManger Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new UnScaleTimerManger();
            }
            return sInstance;
        }
    }

    //�������еĶ�ʱ��
    Dictionary<string, UnScaleTimer> mAllTimerManagerMap;


    UnScaleTimerManger()
    {
        mAllTimerManagerMap = new Dictionary<string, UnScaleTimer>();
    }


    //��
    public bool AddTimer(UnScaleTimer timer)
    {
        if (mAllTimerManagerMap.ContainsKey(timer.key))
        {
            return false;
        }

        mAllTimerManagerMap.Add(timer.key, timer);

        return true;
    }


    //ɾ
    public bool DeleteTimer(string key)
    {
        UnScaleTimer timer = null;
        if (mAllTimerManagerMap.TryGetValue(key, out timer) == false)
        {
            return false;
        }
        timer.isDead = true;
        return true;
    }


    //OneShot
    public bool OneShot(string key, float time, Action action)
    {
        return AddTimer(new UnScaleTimer(key, time, ETimerType.OneShot, action));
    }


    //Repeated
    public bool Repeated(string key, float time, Action action)
    {
        return AddTimer(new UnScaleTimer(key, time, ETimerType.Repeated, action));
    }

    //���ж�ʱ��
    public void Update()
    {
        float dt = Time.unscaledDeltaTime;
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
