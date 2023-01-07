using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 天殛之境_裁决 : MonoBehaviour
{
    void Start()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.HerrscherOfThunderMeiRightHandWeaPonBeHit, null));
        }
        else if (other.tag == "BackBengMei")
        {
            SoundMgr.Instance.PlayEffect("Sound/Effects/BeHitAt");
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.HerrscherOfThunderMeiDoubleHandAtkAtEvent, null));
        }
    }
}
