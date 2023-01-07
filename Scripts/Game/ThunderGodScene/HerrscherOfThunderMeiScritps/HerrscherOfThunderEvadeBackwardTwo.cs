using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderEvadeBackwardTwo : ActionState
{
    int randCound;
    bool canMove;
    RaycastHit hitinfo;
    public HerrscherOfThunderEvadeBackwardTwo(
        EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {

    }


    override public void Enter()
    {
        mAt.CrossFade("EvadeBackForwardTwo", 0.01f);
        canMove = false;
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiEvadeBackForword");
        randCound = Random.Range(0, 2);

    }


    override public void Update()
    {
        if (canMove == false &&
            Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, -mMaster.transform.forward, out hitinfo, 0.5f) == false)
        {
            mMaster.transform.position += -mMaster.transform.forward *
            mMaster.GetComponent<HerrscherOfThunderMeiData>().HerrscherOfThunderEvadeBackwardSpeed * Time.deltaTime;
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
        if (randCound == 1)
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderMeiSkillTwoSwordTrack);
        }
        if (randCound == 0)
        {
            mAm.ChangeAction(EActionType.SkillBezierCuves);
        }
        else
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderMeiIdle);
        }
    }
    override public void AnimationEventOne()
    {
        canMove = true;
    }



}
