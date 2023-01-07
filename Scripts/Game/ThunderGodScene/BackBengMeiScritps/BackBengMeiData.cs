using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiData : MonoBehaviour
{
    public float backBengMeiRunSpeed { get; set; }
    public float backBengMeiJumpMoveSpeed { get; set; }
    public float backBengMeiJumpPower { get; set; }
    public float backBengMeiEvadeForwardSpeed { get; set; }
    public float backBengMeiEvadeBackForwardSpeed { get; set; }
    public bool backBengMeiEvadeForwardTwoCooling { get; set; }
    public bool backBengMeiTimeScaleIsColling { get; set; }
    public float backBengMeiJumpTurnSwordJump { get; set; }
    public float backBengMeiJumpTurnSwordFword { get; set; }
    public float JumpTurnSwordMousLongHitTime { get; set; }
    BackBengMeiData()
    {
        backBengMeiRunSpeed = 5;
        backBengMeiJumpMoveSpeed = 5;
        backBengMeiJumpPower = 7;
        backBengMeiEvadeForwardSpeed = 10;
        backBengMeiEvadeBackForwardSpeed = 10;
        backBengMeiJumpTurnSwordJump = 7;
        backBengMeiJumpTurnSwordFword = 10;
        JumpTurnSwordMousLongHitTime = 0.45f;

        backBengMeiEvadeForwardTwoCooling = false;
        backBengMeiTimeScaleIsColling = false;
    }
}
