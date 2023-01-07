using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTurn天殛之境_裁决 : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 6);
    }

    void Update()
    {
            this.transform.Rotate(Vector3.up, -20);
    }
}
