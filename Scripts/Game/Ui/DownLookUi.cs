using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLookUi : MonoBehaviour
{
    void Start()
    {
        
    }


    public void DownLookButton()
    { 
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.DownLook, null));
    }

}
