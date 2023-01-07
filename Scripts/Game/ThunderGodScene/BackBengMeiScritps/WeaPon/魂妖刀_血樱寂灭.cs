using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 魂妖刀_血樱寂灭 : MonoBehaviour
{
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HerrscherOfThunderMei"|| other.tag == "QiuQiuPopple")
        {
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.BackBengMeiLeftHandAtkAt, null));
        }
    }
}
