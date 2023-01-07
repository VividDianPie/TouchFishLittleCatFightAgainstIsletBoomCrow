using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiBeHitTwo : ActionState
{
    SkinnedMeshRenderer facialExpression;

    public BackBengMeiBeHitTwo(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        facialExpression = mMaster.transform.Find("Body").transform.GetComponent<SkinnedMeshRenderer>();
    }

    override public void Enter()
    {
        mAt.Play("BeHitTwo");
        facialExpression.SetBlendShapeWeight(62, 100);
    }


    override public void Update()
    {


    }

    override public void Exit()
    {
        facialExpression.SetBlendShapeWeight(62, 0); ;

    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }

    override public void AnimationEventOne()
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/不要打我");
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "天殛之境_裁决")
        {
            if (HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoublieKnifeAttack") ||
                HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AtkFive"))
            {
                mAm.ChangeAction(EActionType.BeHitRetreat);
            }
            else
            {
                mAm.ChangeAction(EActionType.BeHitOne);
            }
        }
        else if (other.tag == "QQRWeapon")
        {
            mAm.ChangeAction(EActionType.BeHitOne);
        }
    }

}
