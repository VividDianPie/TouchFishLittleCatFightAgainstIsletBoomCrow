using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//二十四大设计模式之观测者模式
public class EventManager
{

    static EventManager sInstance;
    public static EventManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new EventManager();
            }
            return sInstance;
        }
    }


    //管理事件
    List<MyEvent> mEvents;

    //感兴趣的事件:  事件类型  List<委托>
    Dictionary<EEventType, List<Action<MyEvent>>> mListeners;


    EventManager()
    {
        mEvents = new List<MyEvent>();
        mListeners = new Dictionary<EEventType, List<Action<MyEvent>>>();
    }


    //派遣事件
    public void DispatchEvent(MyEvent evt)
    {
        mEvents.Add(evt);
    }


    //添加监听者
    public void AddListener(EEventType type, Action<MyEvent> call)
    {
        List<Action<MyEvent>> litns = null;
        if (mListeners.TryGetValue(type, out litns))
        {
            //没有找到再添加
            if (litns.IndexOf(call) < 0)
            {
                litns.Add(call);
            }
        }
        else
        {
            //创建一个列表
            litns = new List<Action<MyEvent>>();
            litns.Add(call);

            //添加到字典中
            mListeners.Add(type, litns);
        }
    }


    //删除监听者
    public void DeleteListener(EEventType type, Action<MyEvent> call)
    {
        List<Action<MyEvent>> litns = null;
        if (mListeners.TryGetValue(type, out litns))
        {
            litns.Remove(call);
        }
    }


    //遍历时间，调用相关监听的函数
    public void Update()
    {
        //遍历去访问哪些函数对这个事件感兴趣
        for (int i = 0; i < mEvents.Count; i++)
        {
            List<Action<MyEvent>> litns = null;
            if (mListeners.TryGetValue(mEvents[i].type, out litns))
            {
                for (int j = 0; j < litns.Count; j++)
                {
                    litns[j](mEvents[i]);
                }
            }
        }
        //感兴趣函数执行一次则 清理对应的事件
        mEvents.Clear();
    }
}
