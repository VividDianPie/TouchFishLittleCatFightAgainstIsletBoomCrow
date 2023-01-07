using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiEvadeForwardOne : ActionState
{
    public HerrscherOfThunderMeiEvadeForwardOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    }

    override public void Enter()
    {
        mAt.Play("EvadeForwardOne");
        mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y, BackBengMei.heroActor.position.z)
            - mMaster.transform.position).normalized;
        SoundMgr.Instance.PlayEffect("Sound/Effects/HerrscherOfThunderMeiEvadeForword");
    }
     

    override public void Update()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false)
        {
            mMaster.transform.position += mMaster.transform.forward *
            mMaster.GetComponent<HerrscherOfThunderMeiData>().herrscherOfThunderMeiEvadeSpeedForward * Time.deltaTime;
        }
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

    override public void AnimationEventOne()
    {
        if (Vector3.Distance(mMaster.transform.position, BackBengMei.heroActor.position) < 1.0f)
        {
            mAm.ChangeAction(EActionType.DoublieKnifeAttack);
        }
        else
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeForwardTwo);
        }
    }



}

