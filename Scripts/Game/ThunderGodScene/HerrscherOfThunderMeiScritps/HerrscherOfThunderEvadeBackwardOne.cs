using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderEvadeBackwardOne : ActionState
{
    bool canMove;
    RaycastHit hitinfo;
    int randCound;
    public HerrscherOfThunderEvadeBackwardOne(
        EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {

    }

    override public void Enter()
    {
        mAt.CrossFade("EvadeBackForwardOne", 0.01f);
        canMove = false;
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiEvadeBackForword");
        randCound = Random.Range(1, 3);
    }


    override public void Update()
    {
        if (canMove == false &&
            Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, -mMaster.transform.forward, out hitinfo, 0.5f) == false)
        {
            mMaster.transform.position += -mMaster.transform.forward *
            mMaster.GetComponent<HerrscherOfThunderMeiData>().HerrscherOfThunderEvadeBackwardSpeed * Time.deltaTime;
        }
        else if (randCound == 2)
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeBackwardTwo);
        }
        else if (randCound == 1 || randCound  == 3&& (BackBengMei.heroActor.position - mMaster.transform.position).sqrMagnitude <= 3)
        {
            mAm.ChangeAction(EActionType.SkillThreeJumpTurnSword);
        }
    }
    

    override public void Exit()
    {
        canMove = false;

    }


    override public void OnAnimationStart()
    {

    }


    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()   
    {
        mAm.ChangeAction(EActionType.Walk);
    }
    override public void AnimationEventOne()
    {
        canMove = true;
    }



}
