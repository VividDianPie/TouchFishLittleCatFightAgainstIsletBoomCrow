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
                GameObject �� = GameObject.Find("GameRoot");
                if (�� == null)
                {
                    �� = new GameObject("GameRoot");
                    DontDestroyOnLoad(��);
                }
                mInstance = ��.GetComponent<T>();   
                if (mInstance == null)
                {
                    mInstance = ��.AddComponent<T>();
                }
            }
            return mInstance;
        }
    }

}
