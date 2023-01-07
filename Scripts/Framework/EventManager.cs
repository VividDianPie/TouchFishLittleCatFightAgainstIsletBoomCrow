using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//��ʮ�Ĵ����ģʽ֮�۲���ģʽ
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


    //�����¼�
    List<MyEvent> mEvents;

    //����Ȥ���¼�:  �¼�����  List<ί��>
    Dictionary<EEventType, List<Action<MyEvent>>> mListeners;


    EventManager()
    {
        mEvents = new List<MyEvent>();
        mListeners = new Dictionary<EEventType, List<Action<MyEvent>>>();
    }


    //��ǲ�¼�
    public void DispatchEvent(MyEvent evt)
    {
        mEvents.Add(evt);
    }


    //��Ӽ�����
    public void AddListener(EEventType type, Action<MyEvent> call)
    {
        List<Action<MyEvent>> litns = null;
        if (mListeners.TryGetValue(type, out litns))
        {
            //û���ҵ������
            if (litns.IndexOf(call) < 0)
            {
                litns.Add(call);
            }
        }
        else
        {
            //����һ���б�
            litns = new List<Action<MyEvent>>();
            litns.Add(call);

            //��ӵ��ֵ���
            mListeners.Add(type, litns);
        }
    }


    //ɾ��������
    public void DeleteListener(EEventType type, Action<MyEvent> call)
    {
        List<Action<MyEvent>> litns = null;
        if (mListeners.TryGetValue(type, out litns))
        {
            litns.Remove(call);
        }
    }


    //����ʱ�䣬������ؼ����ĺ���
    public void Update()
    {
        //����ȥ������Щ����������¼�����Ȥ
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
        //����Ȥ����ִ��һ���� �����Ӧ���¼�
        mEvents.Clear();
    }
}
