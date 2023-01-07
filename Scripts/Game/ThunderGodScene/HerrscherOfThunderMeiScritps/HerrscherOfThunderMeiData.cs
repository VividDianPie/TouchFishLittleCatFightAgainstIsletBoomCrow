using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HerrscherOfThunderMeiData : MonoBehaviour
{
    public float herrscherOfThunderMeiEvadeSpeedForward { get; set; }
    public float herrscherOfThunderMeiSkillThreeJumpTurnSwordJump { get; set; }
    public float herrscherOfThunderMeiSkillThreeJumpTurnSwordFword { get; set; }
    public float HerrscherOfThunderEvadeBackwardSpeed { get; set; }
    public float WalkSpeed { get; set; }
    public float WalkSpeed_Android { get; set; }
    public string walkState { get; set; }//      LeftRightWalk     BackWalk     ForwardWalk
    HerrscherOfThunderMeiData()
    {
        herrscherOfThunderMeiEvadeSpeedForward = 10;
        herrscherOfThunderMeiSkillThreeJumpTurnSwordJump = 7;
        herrscherOfThunderMeiSkillThreeJumpTurnSwordFword = 10;
        HerrscherOfThunderEvadeBackwardSpeed = 10;
        WalkSpeed = 3;
        walkState = "LeftRightWalk";
    }

}
