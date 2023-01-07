using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleData : MonoBehaviour
{
    public float qiuQiuPoppleWalkSpeed { get; set; }
    public float qiuQiuPoppleRunSpeed { get; set; }
    public float qiuQiuPoppleJumpSpeed { get; set; }
    public float qiuQiuPoppleJumpPower { get; set; }
    public bool qiuQiuPoppIsAnger { get; set; }

    QiuQiuPoppleData()
    {
        qiuQiuPoppleWalkSpeed = 2;
        qiuQiuPoppleJumpSpeed = 8;
        qiuQiuPoppleJumpPower = 5;
        qiuQiuPoppleRunSpeed = 5;
        qiuQiuPoppIsAnger = false;
    }
}

