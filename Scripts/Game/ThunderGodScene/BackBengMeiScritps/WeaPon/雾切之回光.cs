using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ŒÌ«–÷Æªÿπ‚ : MonoBehaviour
{
    void Start()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HerrscherOfThunderMei"||other.tag == "QiuQiuPopple")
        {
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.BackBengMeiRightHandAtkAt, null));
        }
    }
}
