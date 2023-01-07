using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiIdle : ActionState
{
    int randCound;
    public HerrscherOfThunderMeiIdle(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        randCound = 0;
    }

    override public void Enter()
    {
        mAt.CrossFade("Idle", 0.05f);
    }


    override public void Update()
    {
        //randCound = Random.Range(0, 500);
        //if (randCound == 0)
        //{
        //    //雷律招架
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderWeaPonTurn);
        //}
        //else if (randCound == 1)
        //{
        //    mAm.ChangeAction(EActionType.SkillOneLandThorn);
        //}
        //else if (randCound == 2)
        //{
        //    //连击其一
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderAtkOne);
        //}
        //else if (randCound == 3)
        //{
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderMeiSkillTwoSwordTrack);
        //}
        //else if (randCound == 4)
        //{
        //    //闪避追击
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeForwardOne);
        //}
        //else if (randCound == 5)
        //{
        //    //雷律技能其三
        //    mAm.ChangeAction(EActionType.SkillThreeJumpTurnSword);
        //}
        //else if (randCound == 6 || randCound <= 20)
        //{ 
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeBackwardOne);
        //}
        Debug.Log("AnimationIsIdle");
        mAm.ChangeAction(EActionType.Walk);


    }

    override public void Exit()
    {


    }


    override public void OnAnimationStart()
    {
    }


    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }


}
