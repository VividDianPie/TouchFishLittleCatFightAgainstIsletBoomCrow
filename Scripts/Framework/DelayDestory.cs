using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestory : MonoBehaviour
{

    static DelayDestory sInstance = null;
    public static DelayDestory Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new DelayDestory();
            }
            return sInstance;
        }
    }

    public void SetDestroy(Object obj)
    {
        GameObject.Destroy(obj);
    }

    public void SetDestroy(Object obj,float _time)
    {
        GameObject.Destroy(obj, _time);
    }

}
