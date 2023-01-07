using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton <T>: MonoBehaviour where T : MonoBehaviourSingleton<T>
{

    private static T mInstance;
    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject ¹· = GameObject.Find("GameRoot");
                if (¹· == null)
                {
                    ¹· = new GameObject("GameRoot");
                    DontDestroyOnLoad(¹·);
                }
                mInstance = ¹·.GetComponent<T>();   
                if (mInstance == null)
                {
                    mInstance = ¹·.AddComponent<T>();
                }
            }
            return mInstance;
        }
    }

}
